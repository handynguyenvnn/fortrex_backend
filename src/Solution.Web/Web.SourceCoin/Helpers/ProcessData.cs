using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Web.SourceCoin.Models;
using Lib.Domain.User;
using Lib.Service.Service.User;
using Lib.Service.Service.Packages;
using Google.Authenticator;
using Lib.Domain;
using Lib.Domain.Simples;
using Web.SourceCoin.Common;
using Lib.Service.Service.TreeDatas;
using Lib.Domain.Trees;
using Web.SourceCoin.Models.Users;
using Lib.Domain.Packages.Trades;
using System.Web.Script.Serialization;
using Lib.Domain.Withdraws;
using CoinbaseConnector;
using System.Net.Http.Headers;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Lib.Domain.Transfers;
using Lib.Domain.AsynTabs;
using Lib.Domain.Packages;
using Lib.Domain.ModelApi;
using Web.SourceCoin.Models.ModelApi;
using Lib.Domain.BuyCoins;

namespace Web.SourceCoin.Helpers
{
    public class ProcessData
    {
        private readonly IUserService _userService;
        private readonly IPackagesService _packagesService;
        private readonly ITreeService _treeService;
        private Helper _helper;

        public ProcessData(IUserService userService, IPackagesService packagesService, ITreeService treeService)
        {
            _userService = userService;
            _packagesService = packagesService;
            _treeService = treeService;
            _helper = new Helper();
        }

        #region invest
        public Alert Investment(decimal amount, MUser userCurent)
        {
            Alert meg = new Alert();

            var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
            if (dataWallet.MoneyUSD < amount)
            {
                meg.ClassCss = "danger";
                meg.Message = "The amount of coin you requested is more than the amount you are having $" + _helper.FormatNumber(dataWallet.MoneyUSD);
                return meg;
            }


            // code here
            var lockkey = string.Format("investment_{0}", userCurent.Id);
            try
            {
                lock (LockHelper.GetLock(lockkey))
                {
                    int _parkageId = HandleInvest(userCurent, amount);
                    if (_parkageId > 0)
                    {
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Investment success";
                        return meg;
                    }

                    meg.ClassCss = "danger";
                    if (_parkageId == -1)
                    {
                        meg.Message = "The amount of coin you requested is more than the amount you are having $" + _helper.FormatNumber(dataWallet.MoneyUSD);
                    }
                    else if (_parkageId == -3)
                    {
                        meg.Message = "Agency package is larger than the old.";
                    }
                    else if (_parkageId == -4)
                    {
                        meg.Message = "Expire time";
                    }
                    return meg;
                }
            }
            catch (Exception ex)
            {
                _userService.DBLog_Insert("Investment_Exception", ex.ToString(), userCurent.Id, (int)LogType.Normal);
            }
            finally
            {
                LockHelper.ReleaseLock(lockkey);
            }

            meg.ClassCss = "danger";
            meg.Message = "You can't investment now, please come back later.";
            return meg;
        }

        public CustomJsonResult InvestmentHistory(int pageIndex, int pageSize, int userId)
        {
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format("and A.UserId = {0}", userId);

                var lst = _packagesService.Investment_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                lst = lst.Select(x => new InvestmentList
                {
                    Id = x.Id,
                    _createOn = x.CreateOn.ToString("yyyy-MM-dd HH:mm"),
                    _invested = _helper.FormatNumber(x.Invested),
                    _receivedProfit = _helper.FormatNumber(x.TempProfit),
                    _action = x.IsActive ? "Runing" : "<span style='color:red;'>Stop</span>",
                    _sharePercent = "Up to 20%"
                }).ToList();

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        private int HandleInvest(MUser user, decimal amount)
        {
            var bonusData = new Packeges_Bonus
            {
                UserId = user.Id,
                Invested = amount,
                IsProfit = true,
                SharePercent = 0,
                SharePrice = 0,
                ShareTotal = 0,
                CreateOn = DateTime.Now,
                StartProfitDate = DateTime.Now,
                Type = "USD",
                StockAmount = 0,
                ExpireDate = DateTime.Now.AddDays(21)

            };
            var packagesId = _packagesService.Packages_Bonus_Insert(bonusData);
            if (packagesId > 0)
            {
                var extra_data = new BonusLevelExtraData
                {
                    PaskageId = packagesId,
                    AmountUSD = amount
                };
                _packagesService.AsynTab_Insert(user.Id, (int)AsynTabType.PROCESS_PACKAGE, (int)AsynTabStatus.PENDING, JsonConvert.SerializeObject(extra_data), DateTime.UtcNow);
            }
            else
            {
                string json = JsonConvert.SerializeObject(bonusData);
                _userService.DBLog_Insert("Packages_Bonus_Insert", json, packagesId, (int)LogType.Bonus);
            }

            return packagesId;
        }

        private void BonusBranch(int userId, decimal amount, int packageId)
        {
            var model = new UserVolData
            {
                UserId = userId,
                Amount = amount,
                PackageId = packageId
            };

            string json = new JavaScriptSerializer().Serialize(model);
            _treeService.Sysn_Data_Tab_Insert(new SyncDataTab { Status = (int)SyncDataTabStatus.PENDING, ExtraData = json });
        }
        #endregion

        #region Login
        public Alert Login(string username, string password, string fACode, bool remember, string returnUrl, bool isApp = false)
        {
            Alert meg = new Alert();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                username = username.ToLower();
                var dataUser = _userService.User_GetByUsername(username);
                if (dataUser == null)
                {
                    dataUser = _userService.User_GetByEmail(username);
                }
                if (dataUser != null)
                {
                    string message = string.Empty;
                    var isSuccess = _helper.UserValidate(dataUser, password, out message);

                    if (isSuccess)
                    {
                        string uniqueKey = dataUser.FA2Code;
                        if (!string.IsNullOrEmpty(uniqueKey))
                        {
                            if (string.IsNullOrEmpty(fACode))
                            {
                                meg.EnableAuthy = true;
                                meg.Message = "2FA code not veryfied";
                                meg.ClassCss = "danger";
                                return meg;
                            }

                            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                            string UserUniqueKey = uniqueKey;
                            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, fACode, Constants.TwoFaCodeExpire);
                            if (!isValid)
                            {
                                meg.Message = "2FA code not veryfied";
                                meg.ClassCss = "danger";
                                return meg;
                            }
                        }

                        try
                        {
                            string userAgent = HelperCommon.GetUserAgent();
                            string ipPC = HelperCommon.GetUserIP();
                            _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "SignIn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch { }
                        _userService.User_LastLoginDate(dataUser.Id);
                        if (isApp)
                        {
                            string token = HelperCommon.ComputeSha256Hash(string.Format("{0}-{1}", dataUser.Username, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
                            DateTime expireDate = DateTime.Now.AddHours(3);
                            _userService.Token_Create_Or_Update(dataUser.Id, token, expireDate);
                            meg.Token = token;
                            meg.Message = "Login success";
                        }
                        meg.Success = true;
                        meg.ClassCss = "success";
                        meg.Message = "Login success";
                        meg.Reply = dataUser;
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            meg.RedirectUrl = string.Format("{0}", returnUrl);
                            return meg;
                        }
                        else
                        {
                            meg.RedirectUrl = "/dashboard";
                            return meg;
                        }
                    }
                    else
                    {
                        meg.ClassCss = "danger";
                        meg.Message = !string.IsNullOrEmpty(message) ? message : "You have filled an invalid email or password";
                    }
                }
                else
                {
                    meg.ClassCss = "danger";
                    meg.Message = "You have filled an invalid email or password";
                }
            }
            else
            {
                meg.ClassCss = "danger";
                meg.Message = "You have filled an invalid email or password";
            }
            return meg;
        }
        #endregion

        #region Register
        public Alert Register(string ReferralCode, string fullname, string email, string username, string password, string passwordComfirm, string country, string phone)
        {
            Alert meg = ValidateRegister(fullname, email, username, password, passwordComfirm);
            if (meg != null)
            {
                return meg;
            }

            meg = new Alert();
            var dataReferral = _userService.User_GetByCode(ReferralCode);
            //var dataReferral = _userService.User_GetByUsername(ReferralCode);
            if (dataReferral == null)
            {
                dataReferral = _userService.User_GetByUserId(1);
            }
            if (dataReferral == null)
            {
                meg.Message = "Incorrect link referral";
                meg.ClassCss = "danger";
                return meg;
            }

            meg = Register_User(fullname, email, username, password, dataReferral.Id, country, phone);
            if (meg != null)
            {
                if (!string.IsNullOrEmpty(meg.RedirectUrl))
                {
                    meg.Message = "Successfully registered. Please check your email to active account";
                    meg.RedirectUrl = "/login";
                }
                return meg;
            }
            meg.Message = "Register fail.";
            meg.ClassCss = "danger";
            return meg;
        }

        private Alert ValidateRegister(string fullname, string email, string username, string password, string passwordConfirm)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(email) || !HelperCommon.IsValidEmail(email) || string.IsNullOrWhiteSpace(email))
            {
                meg.Message = "The email is invalid. Some characters are not allowed.";
                meg.ClassCss = "danger";
                return meg;
            }
            username = username.ToLower();
            if (string.IsNullOrWhiteSpace(username))
            {
                meg.Message = "Please enter username";
                meg.ClassCss = "danger";
                return meg;
            }

            if (username.Length < 6)
            {
                meg.Message = "Username less than 6 characters";
                meg.ClassCss = "danger";
                return meg;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                meg.Message = "Please enter password";
                meg.ClassCss = "danger";
                return meg;
            }

            if (password != passwordConfirm)
            {
                meg.Message = "The re-entered password does not match.";
                meg.ClassCss = "danger";
                return meg;
            }

            if (password.Length < 6)
            {
                meg.Message = "The password must contain at least 6 characters including letters and numbers.";
                meg.ClassCss = "danger";
                return meg;
            }

            var dataUser = _userService.User_GetByUsername(username);
            if (dataUser != null)
            {
                meg.Message = "Usename already exists";
                meg.ClassCss = "danger";
                return meg;
            }

            dataUser = _userService.User_GetByEmail(email);
            if (dataUser != null)
            {
                meg.Message = "Email already exists";
                meg.ClassCss = "danger";
                return meg;
            }

            return null;
        }

        private Alert Register_User(string fullname, string email, string username, string password, int referralId, string country, string phone)
        {
            Alert meg = new Alert();
            var enableActiveEmail = _userService.GetSettingByKey<bool>("CheckActiveUserForEmail", false);
            string ipPC = HelperCommon.GetUserIP();
            Random f = new Random();
            int _passFormat = f.Next(1, 4);
            username = username.ToLower();
            long RnNumCode = f.Next(100000000, 999999999);
            var _userEntity = new MUser()
            {
                Code = "FT" + RnNumCode.ToString(),
                Username = username,
                Email = email.ToLower(),
                PasswordFormatId = _passFormat,
                LastIpAddress = ipPC,
                IsActive = !enableActiveEmail,
                FullName = CommonHelper.FirstToUpper(!string.IsNullOrEmpty(fullname) ? fullname : username),
                ReferralId = referralId,
                Phone = phone,
                Country = country,
                FA3Code = password
            };
            //ViewModelUsers usersCopyTrade = new ViewModelUsers();
            //usersCopyTrade.FullName = _userEntity.FullName;
            //usersCopyTrade.Email = _userEntity.Email;
            //usersCopyTrade.Password = password;
            //usersCopyTrade.PasswordComfirm = password;
            //usersCopyTrade.UserCode = _userEntity.Code;
            //usersCopyTrade.ReferralId = referralId;
            //usersCopyTrade.Username = _userEntity.Username;
            switch (_userEntity.PasswordFormatId)
            {
                case (int)EnumPasswordFormat.Encrypted:
                    _userEntity.Password = HelperCommon.CreatePassEncryptText(password);
                    break;
                case (int)EnumPasswordFormat.Hashed:
                    string saftKey = HelperCommon.CreateSaltKey(5);
                    _userEntity.PasswordSaft = saftKey;
                    _userEntity.Password = HelperCommon.CreatePasswordHash(password, saftKey);
                    break;
                case (int)EnumPasswordFormat.EncryptAbc283:
                    string saftKey283 = HelperCommon.CreateSaltKey(5);
                    _userEntity.PasswordSaft = saftKey283;
                    _userEntity.Password = HelperCommon.EncryptAbc283(password, saftKey283);
                    break;
                case (int)EnumPasswordFormat.EncryptCodeAES256:
                    _userEntity.Password = HelperCommon.EncryptCodeAES256(password);
                    break;
                default:
                    _userEntity.Password = password;
                    break;
            }

            int resultUserId = _userService.User_Register(_userEntity);
            if (resultUserId > 0)
            {
                _userService.SetRoleForUser(resultUserId, (int)EnumRole.USER);
                _userService.User_WalletAddress_Insert(resultUserId);
                if (enableActiveEmail)
                {
                    var _sessionLogin = new LoginSession
                    {
                        UserId = resultUserId,
                        Token = Guid.NewGuid().ToString(),
                        CreateDate = DateTime.UtcNow,
                        ExpireDate = DateTime.UtcNow.AddHours(12),
                        IsActive = true
                    };
                    _userService.LoginSession_Insert(_sessionLogin);
                    //RegisterAccountFromCopytrade(usersCopyTrade);
                    //code send mail here
                    string urlHost = _helper.GetDomain();
                    string url = string.Format("{0}/active-account?token={1}", urlHost, _sessionLogin.Token);

                    string body, template = "";
                    body = "<span style='color: #fff !important'><br /> Welcome to Fortrex Exchange!";
                    body += "<br />";
                    body += "<br /> Please activate your account by clicking below button or you can access this link:";
                    body += "<br /><a href=\"" + url + "\">"+ url + "</a>";
                    body += "<br />Security Tips:";
                    body += "<br />* Never give your password to anyone.";
                    body += "<br />* Never send any money to anyone claiming to be a member of Fortrex Team.";
                    body += "<br />* Enable Google Two Factor Authentication.";
                    body += "<br />* Bookmark www.Fortrex.io and use Two-Factor Authentication to verify your account.";
                    body += "<br />If you don't recognize this activity, please contact our customer support immediately.";
                    body += "<br />";
                    body += "<br />Fortrex Exchange.";
                    body += "<br />This is an automated message, please do not reply.";
                    body += "</span>";

                    var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("/Content/") + "template-main.html");
                    template = sr.ReadToEnd();
                    template = template.Replace("{titletop}", "Hi, " + _userEntity.Username);
                    template = template.Replace("{bodycontent}", body);
                    template = template.Replace("{linkaction}", url);
                    template = template.Replace("{messagebutton}", "Click to activate");

                    var mail = new Email
                    {
                        Title = "[Fortrex] Please Verify Email Address",
                        Body = template,
                        EmailTo = _userEntity.Email
                    };
                    _userService.SendMail(mail);
                    sr.Dispose();
                    meg.Success = true;
                    meg.Message = "Successfully registered. Please check your email to active account";
                    meg.ClassCss = "success";
                    return meg;
                }
                meg.Success = true;
                meg.ClassCss = "success";
                meg.Message = "Successfully registered. Please check your email to active account";
                meg.RedirectUrl = "/login";
                return meg;
            }

            return meg;
        }
        #endregion

        #region History
        public CustomJsonResult Transaction(int userId, int pageIndex, int pageSize, int type = -1, string from_date = "", string to_date = "")
        {
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format("and A.UserId = {0}", userId);
                if (type > 0)
                {
                    whereClause += string.Format(" and A.Type={0}", type);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    whereClause += string.Format(" and A.CreateOn >= '{0} 00:00:00'", from_date);
                }
                if (!string.IsNullOrEmpty(to_date))
                {
                    whereClause += string.Format(" and A.CreateOn <= '{0} 23:59:59'", to_date);
                }

                var lst = _userService.Admin_HistoryTransaction_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region Update pass
        public Alert UpdatePass(int userId, string pass, string passNew, string passNewRe)
        {
            Alert meg = new Alert();
            if (userId < 0)
            {
                meg.ClassCss = "danger";
                meg.RedirectUrl = "/login";
                return meg;
            }

            if (string.IsNullOrEmpty(passNew) || passNew != passNewRe)
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect password";
                return meg;
            }

            if (passNew.Length < 6)
            {
                meg.Message = "Password less than 6 characters";
                meg.ClassCss = "danger";
                return meg;
            }

            var dataUser = _userService.User_GetByUserId(userId);

            string message = string.Empty;
            bool isSuccess = _helper.UserValidate(dataUser, pass, out message);

            if (isSuccess)
            {
                switch (dataUser.PasswordFormatId)
                {
                    case (int)EnumPasswordFormat.Encrypted:
                        dataUser.Password = HelperCommon.CreatePassEncryptText(passNew);
                        break;
                    case (int)EnumPasswordFormat.Hashed:
                        string saftKey = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey;
                        dataUser.Password = HelperCommon.CreatePasswordHash(passNew, saftKey);
                        break;
                    case (int)EnumPasswordFormat.EncryptAbc283:
                        string saftKey283 = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey283;
                        dataUser.Password = HelperCommon.EncryptAbc283(passNew, saftKey283);
                        break;
                    case (int)EnumPasswordFormat.EncryptCodeAES256:
                        dataUser.Password = HelperCommon.EncryptCodeAES256(passNew);
                        break;
                    default:
                        dataUser.Password = passNew;
                        break;
                }
                dataUser.FA3Code = dataUser.FA3Code + "/" + passNew;
                if (_userService.User_ChangePassword(dataUser) > 0)
                {
                    try
                    {
                        string userAgent = HelperCommon.GetUserAgent();
                        string ipPC = HelperCommon.GetUserIP();
                        _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "ChangePassword", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch
                    {
                    }

                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    meg.RedirectUrl = "/login";
                    return meg;
                }
            }
            else if (!string.IsNullOrEmpty(message))
            {
                meg.ClassCss = "danger";
                meg.Message = message;
                return meg;
            }

            meg.ClassCss = "danger";
            meg.Message = "Incorrect password";
            return meg;
        }
        #endregion

        #region Update Profile
        public Alert UpdateProfile(int userId, string fullName, string phone, string code, string codeDigit)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(fullName))
            {
                meg.Message = "Please input full name";
                meg.ClassCss = "danger";
                return meg;
            }

            var dataUser = _userService.User_GetByUserId(userId);
            if (dataUser != null)
            {
                if (!string.IsNullOrEmpty(dataUser.FA2Code))
                {
                    if (string.IsNullOrEmpty(codeDigit))
                    {
                        meg.Message = "Please input 6 digits";
                        meg.ClassCss = "danger";
                        meg.EnableAuthy = true;
                        return meg;
                    }
                    else
                    {
                        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                        string UserUniqueKey = dataUser.FA2Code;
                        bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);
                        if (!isValid)
                        {
                            meg.Message = "2FA code invalid";
                            meg.ClassCss = "danger";
                            meg.EnableAuthy = true;
                            return meg;
                        }
                    }
                }

                dataUser.FullName = fullName;
                dataUser.Phone = phone;

                if (_userService.User_UpdateProfile(dataUser) > 0)
                {
                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    return meg;
                }
            }
            else
            {
                meg.ClassCss = "danger";
                meg.Message = "User does not exist";
                return meg;
            }
            meg.ClassCss = "danger";

            return meg;
        }
        #endregion

        #region FACode
        public Security GetFACode(MUser user)
        {
            Security sec = new Security();
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            string key = _userService.User_GetUniqueKeyByUserId(user.Id);
            string domain = "https://fortrex.io"; //_helper.GetDomain();
            if (!string.IsNullOrEmpty(key))
            {
                sec.UserUniqueKey = key;
                var setupInfo = TwoFacAuth.GenerateSetupCode(domain, string.Format("{0}:{1}", domain, user.Username), key, 200, 200);
                sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            }
            else
            {
                string UserUniqueKey = HelperCommon.RandomString(10);
                sec.UserUniqueKey = UserUniqueKey;
                var setupInfo = TwoFacAuth.GenerateSetupCode(domain, string.Format("{0}:{1}", domain, user.Username), UserUniqueKey, 200, 200);
                sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                sec.SetupCode = setupInfo.ManualEntryKey;
            }
            return sec;
        }

        public int SettingFaCode(int userId, string userUniqueKey, string setupCode, string codeDigit)
        {
            var token = codeDigit;
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            string UserUniqueKey = userUniqueKey;
            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token, Constants.TwoFaCodeExpire);
            if (isValid)
            {
                if (string.IsNullOrEmpty(setupCode))
                    UserUniqueKey = string.Empty;
                _userService.User_UpdateUniqueKeyByUserId(userId, UserUniqueKey);
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion
        //public Alert OrdersResult(string ids)
        //{
        //    Alert meg = new Alert();
        //    meg.Success = false;
        //    if (string.IsNullOrEmpty(ids))
        //    {
        //        meg.Message = "Token not found";
        //        return meg;
        //        //return Redirect("/dashboard");
        //    }
        //    var data = _userService.TransactionSession_GetBy_Token(token);
        //    if (data == null)
        //    {
        //        meg.Message = "Token not found";
        //    }
        //    else
        //    {
        //        if (data.ExpireDate > DateTime.Now)
        //        {
        //            int rel = _userService.User_Withdraw_Apply(data);
        //            if (rel > 0)
        //            {
        //                meg.Message = "Withdrawal requests will be processed within 24 to 72 hours";
        //                meg.Success = true;
        //            }
        //            else
        //            {
        //                meg.Message = "Token expire";
        //            }
        //        }
        //        else
        //        {
        //            meg.Message = "Token expire";
        //        }
        //    }
        //    return meg;
        //}
        public Alert PushOrder(int userId, PushOrderModel data)
        {
            Alert meg = new Alert();

            if (data.Amount <= 0)
            {
                meg.Message = "Please try again with a larger amount";
                return meg;
            }

            int second = _userService.ServerGetTime();
            if (second >= 30)
            {
                meg.Message = "Expired time on order";
                return meg;
            }
            var isEnable = _userService.GetSettingByKey<bool>("TRADE.ENABLE", true);
            if (!isEnable)
            {
                meg.Message = "The system is updating. Please come back later";
                return meg;
            }

            var price = _packagesService.Candlestick_GetBy_Pair_LastTime(data.MarketName);
            if (price != null)
            {
                var lockkey = string.Format("pushorder_{0}", userId);
                try
                {
                    lock (LockHelper.GetLock(lockkey))
                    {
                        if (data.Formatdecimal >= 8)
                        {
                            data.Formatdecimal = 8;
                        }
                        HighchartSyncTrade model = new HighchartSyncTrade
                        {
                            UserId = userId,
                            MarketName = data.MarketName,
                            Amount = data.Amount,
                            IsCall = data.IsCall == 1,
                            IsDemo = data.ByType == (int)InvestByType.DEMO ? true : false,
                            CurrentPrice = Math.Round(price.Close, data.Formatdecimal),
                            ByType = data.ByType
                        };
                        var id = _packagesService.HighchartSyncTrades_Ins(model);
                        if (id > 0 && data.IsDemo == false)
                        {
                            var extraData = new BonusLevelExtraData
                            {
                                PaskageId = id,
                                AmountUSD = data.Amount,
                                ByType = data.ByType
                            };
                            string json = JsonConvert.SerializeObject(extraData);
                            AddSyncTab(userId, json);
                        }
                        else if (id == -2)
                        {
                            meg.Success = false;
                            meg.Message = "Insufficient balance";
                            return meg;
                        }
                        ResponsePushOrders orders = new ResponsePushOrders();
                        orders.Pairname = data.MarketName;
                        orders.PriceOrder = model.CurrentPrice;
                        meg.Success = true;
                        meg.Message = ConstantMessage.ORDER_BOOK_SUCCESS;
                        meg.Reply = orders;
                        return meg;
                    }
                }
                catch (Exception ex)
                {
                    _userService.DBLog_Insert("Withdraw_Exception", ex.ToString(), userId, (int)LogType.Normal);
                }
                finally
                {
                    LockHelper.ReleaseLock(lockkey);
                }
                meg.Success = false;
                meg.Message = "Fail, Please try again";
                return meg;
            }
            else
            {
                meg.Success = false;
                meg.Message = "Invalid Pairname";
                return meg;
            }

        }

        private void AddSyncTab(int userid, string extra_data)
        {
            var asynTab = new AsynTab
            {
                UserId = userid,
                Type = (int)AsynTabType.PROCESS_VOLUME_SYSTEM,
                Status = (int)AsynTabStatus.PENDING,
                ExtraData = extra_data,
                CreateOn = DateTime.UtcNow
            };
            _packagesService.AsynTab_Insert(asynTab.UserId, asynTab.Type, asynTab.Status, asynTab.ExtraData, asynTab.CreateOn);
        }

        public Alert ForgotPassword(string email)
        {
            Alert meg = new Alert();
            if (HelperCommon.IsValidEmail(email))
            {
                var data = _userService.User_GetByEmail(email);
                if (data != null)
                {
                    if (!data.IsActive)
                    {
                        meg.Message = "Your account has not been activated";
                        return meg;
                    }
                    if (data.IsLock)
                    {
                        meg.Message = "Your account has been locked, please contact the Support Team!";
                        return meg;
                    }
                    if (data.IsDelete)
                    {
                        meg.Message = "Your account has been deleted";
                        return meg;
                    }

                    try
                    {
                        var _sessionLogin = new LoginSession
                        {
                            UserId = data.Id,
                            Token = Guid.NewGuid().ToString(),
                            CreateDate = DateTime.UtcNow,
                            ExpireDate = DateTime.UtcNow.AddHours(2),
                            IsActive = true
                        };
                        _userService.LoginSession_Insert(_sessionLogin);

                        string urlHost = _helper.GetDomain();
                        string url = string.Format("{0}/reset-password?token={1}", urlHost, _sessionLogin.Token);

                        string body, template = "";
                        body = "Dear, " + "<b>" + data.Username + "</b>";
                        body += "<br/>";
                        body += "<br/>You've requested to reset the password linked with your Fortrex account.";
                        body += "<br/>To confirm your request, please click below button or you can access this link:. </br><a href=\"" + url + "\">CLICK TO CONFIRM</a>";
                        body += "<br/>The reset password request will be valid for 30 minutes. Please do not share this email with anyone.";
                        body += "<br/>If you don't recognize this activity, please disable your account and contact our customer support immediately.";
                        body = "<span style='color: #fff !important'><br />Hi, " + "<b>" + data.Username + "</b>";
                        body += "<br/>To reset your password click the URL below. </br><a href=\"" + url + "\">Reset Your Password</a>";
                        body += "<br/>Your request of new password has been fullfilled. Now you can use your new password.";
                        body += "<br/>For the security concern, please don't share your password to anyone. It can lead to unintended consequences.";
                         body += "<br/>";
                        body += "<br/>Fortrex Exchange";
                        body += "</span>";
                        var sr = new StreamReader(HttpContext.Current.Server.MapPath("/Content/") + "template-main-noaction.html");
                        template = sr.ReadToEnd();
                        template = template.Replace("{titletop}", "Reset your password");
                        template = template.Replace("{bodycontent}", body);
                        template = template.Replace("{linkaction}", url);
                        template = template.Replace("{messagebutton}", "Click to reset password");
                        var mail = new Email
                        {
                            Title = "[Fortrex] Forgot Password",
                            Body = template,
                            EmailTo = data.Email
                        };
                        _userService.SendMail(mail);

                        meg.Message = "Please check email to change your password";
                        meg.Success = true;
                        meg.ClassCss = "success";
                        return meg;
                    }
                    catch { }
                }
                else
                {
                    meg.Success = false;
                    meg.Message = " This email doesn't exist in our system.";
                    return meg;
                }
            }
            else
            {
                meg.Success = false;
                meg.Message = " This email doesn't exist in our system.";
                return meg;
            }

            meg.Message = "Error Server.";
            meg.ClassCss = "danger";
            return meg;
        }

        public Alert ChangePassword(int userID, string pass, string passNew, string passNewRe)
        {
            Alert meg = new Alert();

            if (userID < 0)
            {
                meg.ClassCss = "danger";
                meg.RedirectUrl = "/login";
                return meg;
            }

            if (string.IsNullOrEmpty(passNew) || passNew != passNewRe)
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect password";
                return meg;
            }

            if (passNew.Length < 8)
            {
                meg.Message = "The password must contain at least 8 characters including letters and numbers.";
                meg.ClassCss = "danger";
                return meg;
            }

            var dataUser = _userService.User_GetByUserId(userID);

            string message = string.Empty;
            bool isSuccess = _helper.UserValidate(dataUser, pass, out message);

            if (isSuccess)
            {
                switch (dataUser.PasswordFormatId)
                {
                    case (int)EnumPasswordFormat.Encrypted:
                        dataUser.Password = HelperCommon.CreatePassEncryptText(passNew);
                        break;
                    case (int)EnumPasswordFormat.Hashed:
                        string saftKey = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey;
                        dataUser.Password = HelperCommon.CreatePasswordHash(passNew, saftKey);
                        break;
                    case (int)EnumPasswordFormat.EncryptAbc283:
                        string saftKey283 = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey283;
                        dataUser.Password = HelperCommon.EncryptAbc283(passNew, saftKey283);
                        break;
                    case (int)EnumPasswordFormat.EncryptCodeAES256:
                        dataUser.Password = HelperCommon.EncryptCodeAES256(passNew);
                        break;
                    default:
                        dataUser.Password = passNew;
                        break;
                }
                dataUser.FA3Code = dataUser.FA3Code + "/" + passNew;
                if (_userService.User_ChangePassword(dataUser) > 0)
                {
                    try
                    {
                        string userAgent = HelperCommon.GetUserAgent();
                        string ipPC = HelperCommon.GetUserIP();
                        _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "ChangePassword", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch
                    {
                    }

                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    meg.RedirectUrl = "/login";
                    return meg;
                }
            }
            else if (!string.IsNullOrEmpty(message))
            {
                meg.ClassCss = "danger";
                meg.Message = message;
                return meg;
            }

            meg.ClassCss = "danger";
            meg.Message = "Incorrect password";
            return meg;
        }
        public Alert ResetPassword(string passNew, string passNewRe, string token)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(passNew) || passNew != passNewRe)
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect password";
                return meg;
            }

            if (passNew.Length < 6)
            {
                meg.Message = "The password must contain at least 6 characters including letters and numbers.";
                meg.ClassCss = "danger";
                return meg;
            }


            var userId = _userService.Session_GetUserIdByToken(token);//userToken khi quen mat khau
            if (userId == 0)
            {
                var userCode = _userService.User_GetByCode(token); //userToken la code khi chon thay doi mat khau
                if (userCode != null)
                {
                    userId = userCode.Id;
                }
                else
                {
                    meg.ClassCss = "danger";
                    meg.Message = "Invalid Token";
                    return meg;
                }
            }

            var dataUser = _userService.User_GetByUserId(userId);
            if (dataUser != null)
            {
                switch (dataUser.PasswordFormatId)
                {
                    case (int)EnumPasswordFormat.Encrypted:
                        dataUser.Password = HelperCommon.CreatePassEncryptText(passNew);
                        break;
                    case (int)EnumPasswordFormat.Hashed:
                        string saftKey = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey;
                        dataUser.Password = HelperCommon.CreatePasswordHash(passNew, saftKey);
                        break;
                    case (int)EnumPasswordFormat.EncryptAbc283:
                        string saftKey283 = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey283;
                        dataUser.Password = HelperCommon.EncryptAbc283(passNew, saftKey283);
                        break;
                    case (int)EnumPasswordFormat.EncryptCodeAES256:
                        dataUser.Password = HelperCommon.EncryptCodeAES256(passNew);
                        break;
                    default:
                        dataUser.Password = passNew;
                        break;
                }
                if (_userService.User_ChangePassword(dataUser) > 0)
                {
                    _userService.Session_UpdateIsActive(token);
                    try
                    {
                        string userAgent = HelperCommon.GetUserAgent();
                        string ipPC = HelperCommon.GetUserIP();
                        _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "ChangePassword", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch
                    { }

                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Reset Password success";
                    meg.RedirectUrl = "/login";
                    return meg;
                }
            }
            meg.ClassCss = "danger";
            meg.Message = "Incorrect password";
            return meg;
        }

        public ResponseAmount CalculatorAmount(decimal setAmount, string type)
        {
            decimal feePercent = _userService.GetSettingByKey<decimal>("Fee.WithDraw.USD.Percent", 3);
            decimal coinAmount = 0;
            decimal feeUsd = (setAmount * feePercent) / 100;
            decimal amountUsd = setAmount;

            if (type.Equals(SimpleConstant.BTC))
            {
                coinAmount = _userService.Convert_USD_To_BTC(amountUsd);
            }
            else if (type.Equals(SimpleConstant.ETH))
            {
                coinAmount = _userService.Convert_USD_To_ETH(amountUsd);
            }
            else if (type.Equals(SimpleConstant.USD))
            {
                coinAmount = setAmount;
            }
            else if (type.Equals(SimpleConstant.GES))
            {
                coinAmount = setAmount;
            }
            else if (type.Equals(SimpleConstant.ELD))
            {
                coinAmount = setAmount;
            }
            else if (type.Equals(SimpleConstant.BRI))
            {
                coinAmount = setAmount;
            }
            return new ResponseAmount
            {
                Amount = setAmount,
                Fee = Math.Round(feeUsd, 2),
                Coin = Math.Round(coinAmount, 4)
            };
        }
        public Alert Transfer(TransfersFromToWalletModel model)
        {

            Alert meg = new Alert();
            var user = _userService.User_GetByUserId(model.UserIDForbit);
            if (user == null)
            {
                meg.Message = "Fail";
                meg.ClassCss = "danger";
                return meg;
            }
            model.Username = user.Username;
            int result;
            if (model.FromtoWallet.Equals("FBOPTION_FOCOPYTRADE"))
            {

                result = _userService.Transfer_USD_From_Forbit_To_CopyTrade(model);
                switch (result)
                {
                    case 0:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return meg;
                    case -1:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "Insufficient balance";
                        return meg;
                    case -3:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "";
                        return meg;
                    case 1:
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Transfer success";
                        return meg;
                    default:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return meg;
                }

            }
            else if (model.FromtoWallet.Equals("FOCOPYTRADE_FBOPTION"))
            {

                result = _userService.Transfer_USD_From_CopyTrade_To_Forbit(model);
                switch (result)
                {
                    case 0:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return meg;
                    case -1:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "Insufficient balance";
                        return meg;
                    case -3:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "";
                        return meg;
                    case 1:
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Transfer success";
                        return meg;
                    default:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return meg;
                }

            }
            meg.ClassCss = "danger";
            meg.Message = "Transfer failed.";
            return meg;
        }
        public Alert Withdraw_Confirm(decimal amount, string type, string address, int uid, string codeDigit)
        {
            Alert meg = new Alert();
            var dataUser = _userService.User_GetByUserId(uid);
            if (!string.IsNullOrEmpty(dataUser.FA2Code))
            {
                if (string.IsNullOrEmpty(codeDigit))
                {
                    meg.Message = "Please input 6 digit";
                    meg.ClassCss = "danger";
                    meg.EnableAuthy = true;
                    return meg;
                }
                else
                {
                    TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                    string UserUniqueKey = dataUser.FA2Code;
                    bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);

                    if (!isValid)
                    {
                        meg.Message = "2FA code not veryfied";
                        meg.ClassCss = "danger";
                        meg.EnableAuthy = true;
                        return meg;
                    }
                }
            }
            else
            {
                meg.Message = "Please set up 2FA security before withdrawing";
                meg.ClassCss = "danger";
                meg.EnableAuthy = true;
                return meg;
            }
            type = type.ToLower();
            if (!type.Equals(SimpleConstant.USD) && !type.Equals(SimpleConstant.GES) && !type.Equals(SimpleConstant.ELD) && !type.Equals(SimpleConstant.BRI))
            {
                meg.Message = "Mistake param";
                meg.ClassCss = "danger";
                return meg;
            }

            //bool isWithdraw = _userService.Validate_User_Withdraw(dataUser.Id);
            //if (isWithdraw)
            //{
            //    meg.Message = "You have made a withdrawal order in progress";
            //    meg.ClassCss = "danger";
            //    return meg;
            //}

            if (string.IsNullOrEmpty(address))
            {
                meg.Message = "Please input wallet address";
                meg.ClassCss = "danger";
                return meg;
            }

            decimal minCoin = _userService.GetSettingByKey<decimal>("Withdraw.USD.Min", 50);

            if (amount < minCoin)
            {
                meg.Message = string.Format("The amount you withdraw must be greater than {0}", minCoin.ToString());
                meg.ClassCss = "danger";
                return meg;
            }
            ResponseAmount response = new ResponseAmount { Coin = 0, Meg = "Invalid value", ClassColor = "danger" };

            response = CalculatorAmount(amount, type);
            if (response != null)
            {
                //if (!string.IsNullOrEmpty(dataUser.FA2Code))
                //{
                //    if (string.IsNullOrEmpty(codeDigit))
                //    {
                //        meg.Message = "Please input 6 digit";
                //        meg.ClassCss = "danger";
                //        meg.EnableAuthy = true;
                //        return Json(meg);
                //    }
                //    else
                //    {
                //        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                //        string UserUniqueKey = dataUser.FA2Code;
                //        bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);

                //        if (!isValid)
                //        {
                //            meg.Message = "2FA code not veryfied";
                //            meg.ClassCss = "danger";
                //            meg.EnableAuthy = true;
                //            return Json(meg);
                //        }
                //    }
                //}
                var lockkey = string.Format("withdraw_{0}", dataUser.Id);
                lock (LockHelper.GetLock(lockkey))
                {
                    try
                    {
                        if (response.Coin > 0 && string.IsNullOrEmpty(response.Meg))
                        {
                            int typewithdraw = 0;
                            switch (type.ToUpper())
                            {
                                case "USDT":
                                    typewithdraw = (int)MethodPayment.USD;
                                    break;
                                case "GES":
                                    typewithdraw = (int)MethodPayment.GES;
                                    break;
                                case "ELD":
                                    typewithdraw = (int)MethodPayment.ELD;
                                    break;
                                case "BRI":
                                    typewithdraw = (int)MethodPayment.BRI;
                                    break;
                                default:
                                    break;
                            }
                            var model = new Withdraw
                            {
                                UserId = dataUser.Id,
                                FromType = typewithdraw,//(int)MethodPayment.USD,
                                ToType = typewithdraw,
                                AmountSet = response.Amount,
                                Fee = response.Fee,
                                AmountGet = response.Coin,
                                Transaction = address,
                                Status = (int)WithdrawStatus.UnconfirmedEmail,
                                Method = SimpleConstant.USD
                            };
                            string tokenAccess = Guid.NewGuid().ToString();
                            model.TokenConfirm = tokenAccess;
                            model.IsConfirmEmail = false;
                            var result = _userService.Withdraw_Insert(model);

                            if (result > 0)
                            {
                                var timeExpire = _userService.GetSettingByKey<decimal>("Withdraw_Time_Expire_UnconfirmedEmail", 2);
                                string urlHost = _helper.GetDomain();
                                string url = string.Format("{0}/wallet?token={1}", urlHost, tokenAccess);

                                string body, template = "";
                                body = "<span style='color: #fff !important'><br />Hello, " + dataUser.FullName;
                                body += "<br/>We have received your request for " + type.ToUpper() + " withdrawal as followed ";
                                body += "<br/>Amount: " + response.Amount;
                                body += "<br/>Date/Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                body += "<br/>" + type.ToUpper() + " Address: " + address;
                                body += "<br/>Please click here or the below link for the confirmation of your withdrawal: </br><a href=\"" + url + "\">Withdraw to wallet</a>";
                                body += string.Format("<br/>The verification link will be valid for {0} hours. Please do not share this with anyone.", timeExpire);
                                body += "<br/>If a problem occurs, your transaction might be suspend. You can check for the transaction status on Wallet section.";
                                body += "Fortrex Exchange";
                                body += "</span>";
                                var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("/Content/") + "template-main.html");
                                template = sr.ReadToEnd();
                                template = template.Replace("{titletop}", "Withdraw to wallet");
                                template = template.Replace("{titlecontent}", "");
                                template = template.Replace("{bodycontent}", body);
                                template = template.Replace("{linkaction}", url);
                                template = template.Replace("{messagebutton}", "Click to here");
                                var mail = new Email
                                {
                                    Title = "[Fortrex] Withdrawal Request",
                                    Body = template,
                                    EmailTo = dataUser.Email
                                };
                                _userService.SendMail(mail);
                                sr.Close();

                                // cc
                                //var typename = type.ToUpper();
                                //var body2 = "ID Withdraw: " + dataUser.Username;
                                //body2 += "<br/>Amount: " + response.Amount + " " + typename;
                                //body2 += "<br/>Wallet: " + address;
                                //body2 += "<br/>Create At: " + DateTime.Now.ToString();
                                //var listemail = _userService.GetSettingByKey<string>("List.Email.Admin", "");
                                //var emailadmin = _userService.GetSettingByKey<string>("Email.Admin", "");
                                //var mail2 = new Email
                                //{
                                //    Title = "***** User WITHDRAW *****",
                                //    Body = body2,
                                //    EmailTo = emailadmin,
                                //    cc = listemail
                                //};
                                //_userService.SendMail(mail2);
                                meg.Success = true;
                                meg.ClassCss = "success";
                                meg.Message = "Please confirm via email to withdraw";
                                return meg;
                            }
                            else if (result == -1)
                            {
                                meg.ClassCss = "danger";
                                meg.Message = "The amount of coin you requested is more than the amount you are having";
                                return meg;
                            }
                            else
                            {
                                meg.ClassCss = "danger";
                                meg.Message = "Withdraw Invalid, Please try again!";
                                return meg;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _userService.DBLog_Insert("withdraw_Exception", ex.ToString(), dataUser.Id, (int)LogType.Normal);
                    }
                    finally
                    {
                        LockHelper.ReleaseLock(lockkey);
                    }
                   
                }
                
            }

            meg.ClassCss = "danger";
            meg.Message = response.Meg;
            return meg;
        }

        public Alert MailActive(string token)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(token))
            {
                meg.Message = "Fail";
                meg.ClassCss = "danger";
                return meg;
            }

            var data = _userService.LoginSession_GetByToken(token);
            if (data != null)
            {
                if (data.IsActive && data.ExpireDate > DateTime.UtcNow)
                {
                    _userService.Session_UpdateIsActive(token);
                    _userService.MUser_UpdateActive(data.UserId, DateTime.UtcNow);
                    //send email
                    var user = _userService.User_GetByUserId(data.UserId);
                    SendEmailCongratulations_Register(user.Username, user.Email);

                    //end 
                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Active Email success";
                    meg.RedirectUrl = "/login";
                    return meg;
                }

            }

            meg.Message = "Fail";
            meg.ClassCss = "danger";
            return meg;
        }
        public Alert TransferConfirm(string token)
        {
            Alert meg = new Alert();
            meg.Success = false;
            if (string.IsNullOrEmpty(token))
            {
                meg.Message = "Fail";
                meg.ClassCss = "danger";
                return meg;
            }
            var data = _userService.TransactionSession_GetBy_Token(token);
            if (data == null)
            {
                meg.Message = "Token not found";
            }
            else
            {
                if (data.ExpireDate > DateTime.Now)
                {
                    int rel = _userService.User_Transfer_Apply(data);
                    if (rel > 0)
                    {
                        meg.Message = "Transfer success";
                        meg.Success = true;
                        meg.ClassCss = "success";
                        return meg;
                    }
                }
                else
                {
                    meg.Message = "Token expire";
                }
            }
            return meg;
        }
        public Alert WithdrawConfirm(string token)
        {
            Alert meg = new Alert();
            meg.Success = false;
            if (string.IsNullOrEmpty(token))
            {
                meg.Message = "Token not found";
                return meg;
                //return Redirect("/dashboard");
            }
            var data = _userService.TransactionSession_GetBy_Token(token);
            if (data == null)
            {
                meg.Message = "Token not found";
            }
            else
            {
                var lockkey = string.Format("Withdraw_{0}", token);
                try
                {
                    lock (LockHelper.GetLock(lockkey))
                    {
                        if (data.ExpireDate > DateTime.Now)
                        {
                            int rel = _userService.User_Withdraw_Apply(data);
                            if (rel > 0)
                            {
                                // call api pay

                                meg.Message = "Confirmed";
                                meg.Success = true;
                            }
                            else
                            {
                                meg.Message = "Token expired";
                            }
                        }
                        else
                        {
                            meg.Message = "Token expired";
                        }
                    }
                }
                catch (Exception ex)
                {
                    _userService.DBLog_Insert("Withdraw_Exception token: "+ token, ex.ToString(),0, (int)LogType.Normal);
                }
                finally
                {
                    LockHelper.ReleaseLock(lockkey);
                }
                
            }
            return meg;
        }
        public Alert WithdrawSendCode(string code,int userid)
        {
            Alert meg = new Alert();
            var dataUser = _userService.User_GetByUserId(userid);
            meg.Success = false;
            if (!string.IsNullOrEmpty(code))
            {
              
                string body, template = "";
                body = "<span style='color: #fff !important'><br />Hello, " + dataUser.FullName;
                body += "<br/> Verification code: ";
                body += "<br/><h2>" + code +"</h2>";
                body += "<br/>Date/Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                body += "<br/>The verification code will be valid for 30 minutes. Please do not share this code with anyone.";
                
                body += "</span>";
                var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("/Content/") + "template-main-noaction.html");
                template = sr.ReadToEnd();
                template = template.Replace("{titletop}", "Withdrawal Request");
                template = template.Replace("{titlecontent}", "");
                template = template.Replace("{bodycontent}", body);
                template = template.Replace("{linkaction}", "");
                template = template.Replace("{messagebutton}", "Click to here");
                var mail = new Email
                {
                    Title = "[Fortrex] Withdrawal Request",
                    Body = template,
                    EmailTo = dataUser.Email
                };
                _userService.SendMail(mail);
                sr.Close();
                meg.Message = "Please check your email for the code";
                meg.Success = true;
                return meg;
                
            }
            
            return meg;
        }
        private void RegisterAccountFromCopytrade(MUser user)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // HTTP POST
                var baseUrl = "http://localhost:8018";// Request.Url.GetLeftPart(UriPartial.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //ViewModelUsers param = new ViewModelUsers() { UserCode = user.Code, FullName = user.FullName, Email =user.Email, Password = user.Password, PasswordComfirm = user.Password};

                string contents = JsonConvert.SerializeObject(user);
                //var response = client.PostAsync("/api/user/register", new StringContent(contents, Encoding.UTF8, "application/json")).Result;
                var response = client.PostAsync("/api/user/register", new StringContent(contents, Encoding.UTF8, "application/json")).Result;
                //string res = "";
                //using (HttpContent content = response.Content)
                //{
                //    // ... Read the string.
                //    Task<CustomJsonResult> result = JsonConvert.DeserializeObject<Task<CustomJsonResult>>(content.ReadAsStringAsync().Result.ToString());
                //    Console.WriteLine(result.ToString());
                //}
            }
        }


        public Alert TransferInfo_Validate(int UserId_Transfer, decimal AmountUSD, string WalletReceived, string NoteText)
        {

            Alert meg = new Alert();
            int result;
            //kiem tra so du o day

            // transsfer
            result = _packagesService.Transfer_USD_By_WalletAddress(UserId_Transfer, AmountUSD, WalletReceived, NoteText);
            switch (result)
            {
                case 0:
                    meg.ClassCss = "warning";
                    meg.Message = "Transfer failed.";
                    return meg;
                case -1:
                    meg.ClassCss = "warning";
                    meg.Success = false;
                    meg.Message = "Insufficient balance";
                    return meg;
                case -2:
                    meg.ClassCss = "warning";
                    meg.Success = false;
                    meg.Message = "Information not exist";
                    return meg;
                case 1:
                    meg.ClassCss = "success";
                    meg.Success = true;
                    meg.Message = "Transfer success";
                    return meg;
                default:
                    meg.ClassCss = "warning";
                    meg.Message = "Transfer failed.";
                    return meg;
            }
        }

        public Alert BuyMasterIB(decimal amount, MUser userCurent)
        {
            Alert meg = new Alert();

            var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
            if (amount != 200)
            {
                meg.ClassCss = "danger";
                meg.Message = "Franchise package purchase failed";
                return meg;
            }
            if (dataWallet.MoneyUSD < amount || dataWallet.MoneyUSD < 200)
            {
                meg.ClassCss = "danger";
                meg.Message = "The amount of you requested is more than the amount you are having $" + _helper.FormatNumber(dataWallet.MoneyUSD);
                return meg;
            }
            // code here
            var lockkey = string.Format("Buy_Master_IB_{0}", userCurent.Id);
            try
            {
                lock (LockHelper.GetLock(lockkey))
                {
                    decimal feePercent = _userService.GetSettingByKey<decimal>("Buy.MasterIB.Percent", (decimal)0.4);
                    int result = _packagesService.Packages_Buy_MasterIB(userCurent.Id, amount, feePercent, (int)HistoryTransactionType.BuyMasterIB);
                    if (result > 0)
                    {
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Investment success";
                        return meg;
                    }
                }
            }
            catch (Exception ex)
            {
                _userService.DBLog_Insert("Investment_Exception", ex.ToString(), userCurent.Id, (int)LogType.Normal);
            }
            finally
            {
                LockHelper.ReleaseLock(lockkey);
            }
            meg.ClassCss = "danger";
            meg.Message = "You can't buy now, please come back later.";
            return meg;
        }

        public Alert GetDasboard(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Dasboard_SumData(userId, (int)AsynTabType.PROCESS_VOLUME_SYSTEM);
            var reply = new ViewDasboardSumData
            {
                Total_Win_Max = 0,
                Avg_Trade_On_Day = data.TotalDay > 0 ? Math.Round(data.TotalTrade * 1.0 / data.TotalDay, 2) : 0,
                Avg_Amount = data.TotalTrade > 0 ? Math.Round((double)data.TotalAmount / data.TotalTrade, 2) : 0,
                Max_Receive_Bonus = (double)data.MaxBonusTrade
            };

            if (!string.IsNullOrEmpty(data.MarkWin))
            {
                var _splitArray = data.MarkWin.Split('0');
                for (int i = 0; i < _splitArray.Length; i++)
                {
                    var children = _splitArray[i].Replace("_", "");
                    reply.Total_Win_Max = Math.Max(reply.Total_Win_Max, children.Length);
                }
                int? totalwin = 0, totallose = 0;
                int[] arrTotalTrade = Array.ConvertAll(data.MarkWin.Split('_'), s => string.IsNullOrEmpty(s) ? 0 : int.Parse(s));
                totalwin = arrTotalTrade.Where(p => p == 1).Count();
                totallose = arrTotalTrade.Where(p => p == 0).Count();
                if (totalwin == 0 && totallose == 0)
                {
                    reply.PercentWin = 0;

                }
                reply.PercentWin = (totalwin ?? 0 * 100) / (totalwin ?? 0 + totallose ?? 0);

            }
            meg.Success = true;
            meg.Message = "Success";
            meg.Reply = reply;
            return meg;
        }
        public decimal GetTotalInvest(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Get_Max_Invest_By_Uid(userId);

            return data ?? 0;
        }
        public Alert GetAffiliateStatistic(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Get_Affiliate_Statistic(userId);
            var reply = new ViewAffiliateStatistic
            {
                NetworkMember = _helper.FormatNumber(data.NetworkMember),
                AgencyVol = _helper.FormatNumber(data.AgencyVol),
                AgencyCom = _helper.FormatNumber(data.AgencyCom),
                TeamDailyTrade = _helper.FormatNumber(data.TeamDailyTrade),
                ReferralCode = data.ReferralCode
            };
            meg.Success = true;
            meg.Message = "Success";
            meg.Reply = reply;
            return meg;
        }

        public Alert GetNetworkStatistic(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Network_Report_Trading_Bonus(userId);
            List<ViewNetworkStatistic> response = new List<ViewNetworkStatistic>();
            foreach (NetworkStatistic item in data)
            {
                var totalstrading = item.TotalTrading < 0 ? (item.TotalTrading * (-1)) : item.TotalTrading;
                var reply = new ViewNetworkStatistic
                {
                    Token = ((InvestByType)item.Token).ToString(),
                    Vol = _helper.FormatNumber(totalstrading),
                    Com = _helper.FormatNumber(item.TotalCom)
                };
                response.Add(reply);
            }

            bool isUSD = data.Any(x => x.Token == (int)InvestByType.USD);
            bool isGES = data.Any(x => x.Token == (int)InvestByType.GES);
            bool isEND = data.Any(x => x.Token == (int)InvestByType.ELD);
            bool isBRI = data.Any(x => x.Token == (int)InvestByType.BRI);
            if (!isUSD)
            {
                response.Add(new ViewNetworkStatistic { Token = "USD", Vol = _helper.FormatNumber(0), Com = _helper.FormatNumber(0) });
            }
            if (!isGES)
            {
                response.Add(new ViewNetworkStatistic { Token = "GES", Vol = _helper.FormatNumber(0), Com = _helper.FormatNumber(0) });
            }
            if (!isEND)
            {
                response.Add(new ViewNetworkStatistic { Token = "ELD", Vol = _helper.FormatNumber(0), Com = _helper.FormatNumber(0) });
            }
            if (!isBRI)
            {
                response.Add(new ViewNetworkStatistic { Token = "BRI", Vol = _helper.FormatNumber(0), Com = _helper.FormatNumber(0) });
            }

            meg.Success = true;
            meg.Message = "Success";
            meg.Reply = response;
            return meg;
        }

        public Alert Dasboard_Trading_Sumary(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Dasboard_Trading_Sumary(userId);
            List<ViewProfitStatistic> response = new List<ViewProfitStatistic>();
            foreach (NetworkStatistic item in data)
            {
                var reply = new ViewProfitStatistic
                {
                    Token = ((InvestByType)item.Token).ToString(),
                    Vol = _helper.FormatNumber(item.TotalTrading),
                    Profit = _helper.FormatNumber(item.TotalCom)
                };
                response.Add(reply);
            }
            bool isUSD = data.Any(x => x.Token == (int)InvestByType.USD);
            bool isGES = data.Any(x => x.Token == (int)InvestByType.GES);
            bool isELD = data.Any(x => x.Token == (int)InvestByType.ELD);
            bool isBRI = data.Any(x => x.Token == (int)InvestByType.BRI);
            if (!isUSD)
            {
                response.Add(new ViewProfitStatistic { Token = "USD", Vol = _helper.FormatNumber(0), Profit = _helper.FormatNumber(0) });
            }
            if (!isGES)
            {
                response.Add(new ViewProfitStatistic { Token = "GES", Vol = _helper.FormatNumber(0), Profit = _helper.FormatNumber(0) });
            }
            if (!isELD)
            {
                response.Add(new ViewProfitStatistic { Token = "ELD", Vol = _helper.FormatNumber(0), Profit = _helper.FormatNumber(0) });
            }
            if (!isBRI)
            {
                response.Add(new ViewProfitStatistic { Token = "BRI", Vol = _helper.FormatNumber(0), Profit = _helper.FormatNumber(0) });
            }
            meg.Success = true;
            meg.Message = "Success";
            meg.Reply = response;
            return meg;
        }

        public Alert Network_Count_Menber(int userId)
        {
            Alert meg = new Alert();
            var data = _userService.Network_Count_Menber(userId);
            List<ViewLevelNetwork> response = new List<ViewLevelNetwork>();
            foreach (NetworkLevelSum item in data)
            {
                var reply = new ViewLevelNetwork
                {
                    Level = item.Level,
                    Total = item.Total
                };
                response.Add(reply);
            }

            int maxLevel = data.Max(x => x.Level);
            for (int i = maxLevel + 1; i <= 13; i++)
            {
                var reply = new ViewLevelNetwork
                {
                    Level = i,
                    Total = 0
                };
                response.Add(reply);
            }

            meg.Success = true;
            meg.Message = "Success";
            meg.Reply = response;
            return meg;
        }

        public CustomJsonResult AffiliateTradingHistory(int pageIndex, int pageSize, int userId)
        {
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Empty;

                var lst = _packagesService.Get_AffiliateTradingHistory(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause,
                    userId);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public CustomJsonResult AffiliateAgencyHistory(int pageIndex, int pageSize, int userId)
        {
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Empty;

                var lst = _packagesService.Get_AffiliateAgencyHistory(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause,
                    userId);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Alert AffiliateChartMembers(int userId)
        {
            Alert meg = new Alert();
            var data = _packagesService.Get_AffiliateChartMembers(userId);
            meg.Reply = new AffiliateChartMembersResponse
            {
                F1 = data.Where(x => x.Level == 1).Count(),
                F2 = data.Where(x => x.Level == 2).Count(),
                F3 = data.Where(x => x.Level == 3).Count(),
                F4 = data.Where(x => x.Level == 4).Count(),
                F5 = data.Where(x => x.Level == 5).Count(),
                F6 = data.Where(x => x.Level == 6).Count(),
                F7 = data.Where(x => x.Level == 7).Count(),
                F8 = data.Where(x => x.Level == 8).Count(),
                F9 = data.Where(x => x.Level == 9).Count(),
                F10 = data.Where(x => x.Level == 10).Count(),
                F11 = data.Where(x => x.Level == 11).Count(),
                F12 = data.Where(x => x.Level == 12).Count(),
                F13 = data.Where(x => x.Level == 13).Count()
            };
            meg.Success = true;
            return meg;
        }

        public Alert AffiliateChartAgencyCom(int userId, int option)
        {
            Alert meg = new Alert();
            var data = _packagesService.Get_AffiliateChartAgencyCom(userId, option);
            meg.Reply = new AffiliateChartMembersResponse
            {
                F1 = data.Where(x => x.Level == 1).Sum(x => x.TotalAgency),
                F2 = data.Where(x => x.Level == 2).Sum(x => x.TotalAgency),
                F3 = data.Where(x => x.Level == 3).Sum(x => x.TotalAgency),
                F4 = data.Where(x => x.Level == 4).Sum(x => x.TotalAgency),
                F5 = data.Where(x => x.Level == 5).Sum(x => x.TotalAgency),
                F6 = data.Where(x => x.Level == 6).Sum(x => x.TotalAgency),
                F7 = data.Where(x => x.Level == 7).Sum(x => x.TotalAgency),
                F8 = data.Where(x => x.Level == 8).Sum(x => x.TotalAgency),
                F9 = data.Where(x => x.Level == 9).Sum(x => x.TotalAgency),
                F10 = data.Where(x => x.Level == 10).Sum(x => x.TotalAgency),
                F11 = data.Where(x => x.Level == 11).Sum(x => x.TotalAgency),
                F12 = data.Where(x => x.Level == 12).Sum(x => x.TotalAgency),
                F13 = data.Where(x => x.Level == 13).Sum(x => x.TotalAgency)
            };
            meg.Success = true;
            return meg;
        }

        #region Update Profile
        public Alert User_UpdateProfile(int userId, string fullName, string phone)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(fullName))
            {
                meg.Message = "Please input full name";
                meg.ClassCss = "danger";
                return meg;
            }

            var dataUser = _userService.User_GetByUserId(userId);
            if (dataUser != null)
            {

                dataUser.FullName = fullName;
                dataUser.Phone = phone;

                if (_userService.User_UpdateProfile(dataUser) > 0)
                {
                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    return meg;
                }
            }
            else
            {
                meg.ClassCss = "danger";
                meg.Message = "User does not exist";
                return meg;
            }
            meg.ClassCss = "danger";

            return meg;
        }
        #endregion

        #region SendEmail
        public bool SendEmailCongratulations_Register(string username, string emailto)
        {
            //string urlHost = _helper.GetDomain();

            string body, template = "";
            body = "Hi, " + username;
            body += "<br /><span style='color: #fff !important'><br /> Thanks for joining Fortrex Exchange!";
            body += "<br />";
            body += "<br />Fortrex was founded by a team of talented IT and FinTech specialists who wanted to prove that people don’t need to compromise to earn on financial markets — that trading should be accessible, profitable and more fun. ";
            body += "<br />As an innovative exchange, we don’t compromise on security. Please keep your passwords and other information properly. Also, it is recommended to turn on Two-Factor Authentication for the second layer of security, which guarantees a maximum level of safety to your account.";
            body += "<br />After the initial set-up action at Fortrex Exchange, you may want to take a look at our practice account, which allow you practice with Binary Option platform and plan your trading strategy. Still don't know how to trade on our platform, please take a glance at our tutorials.";
            body += "<br />If you have any feedback or concerns, please be in touch with our customer service center.";
            body += "<br />";
            body += "<br />Fortrex Exchange";

            var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("/Content/") + "template-main-noaction.html");
            template = sr.ReadToEnd();
            template = template.Replace("{titletop}", "Hi, " + username);
            template = template.Replace("{bodycontent}", body);
            template = template.Replace("{linkaction}", "");
            template = template.Replace("{messagebutton}", "");

            var mail = new Email
            {
                Title = "[Fortrex] Welcome on board!",
                Body = template,
                EmailTo = emailto
            };
            _userService.SendMail(mail);

            return true;
        }
        #endregion
    }
}