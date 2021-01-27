using CoinbaseConnector;
using Google.Authenticator;
using Lib.Domain;
using Lib.Domain.Packages;
using Lib.Domain.Simples;
using Lib.Domain.User;
using Lib.Service.Service.CoinBase;
using Lib.Service.Service.Packages;
using Lib.Service.Service.TreeDatas;
using Lib.Service.Service.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.SourceCoin.Common;
using Web.SourceCoin.Helpers;
using Web.SourceCoin.Models;
using Web.SourceCoin.Models.Users;

namespace Web.SourceCoin.Controllers
{

    public class HomeController : BaseController
    {
        private Helper _helper;
        private readonly ICoinService _coinService;
        private readonly ITreeService _treeService;
        private readonly IPackagesService _packagesService;
        public HomeController(IUserService userService, ICoinService coinService, ITreeService treeService, IPackagesService packagesService) : base(userService)
        {
            _helper = new Helper();
            _coinService = coinService;
            _treeService = treeService;
            _packagesService = packagesService;
        }

     
        public ActionResult Index()
        {
            return Redirect("/login");
            
        }

        #region User
        public ActionResult Login(string returnUrl)
        {
            var curentUser = GetCurrentUser();
            if (curentUser != null)
                return Redirect("/manage");

            UserLogin login = new UserLogin();
            if (!string.IsNullOrEmpty(this.cusername))
            {
                login.Username = this.cusername;
                login.ReturnUrl = returnUrl;
                if (!string.IsNullOrEmpty(this.cpassword))
                {
                    login.Password = this.cpassword;
                    login.Remember = true;
                }
            }
            return View(login);
        }

        [HttpPost]
        public JsonResult Login(string username, string password, string fACode, bool remember, string returnUrl, string response)
        {
            Alert meg = new Alert();
            var statustoken = HelperCommon.ValidateCapchar(response);
            if (!statustoken)
            {
                meg.Success = false;
                meg.ClassCss = "danger";
                meg.Message = "Captcha validation failed";
                return Json(meg);
            }
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
                                return Json(meg);
                            }

                            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                            string UserUniqueKey = uniqueKey;
                            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, fACode, Constants.TwoFaCodeExpire);
                            if (!isValid)
                            {
                                meg.Message = "2FA code not veryfied";
                                meg.ClassCss = "danger";
                                return Json(meg);
                            }
                        }
                        if (remember)
                        {
                            HelperCommon.SetCookies("Fortrex.Authen_Username", dataUser.Username, 720, isJs: true);
                            //HelperCommon.SetCookies("ForbitOption.Authen_Password", password, 720, isJs: true);
                            FormsAuthentication.SetAuthCookie(dataUser.Username, true);
                        }
                        else
                        {
                           
                            HelperCommon.ClearCookies("Fortrex.Authen_Password");
                            FormsAuthentication.SetAuthCookie(dataUser.Username, false);
                        }
                        try
                        {
                            string userAgent = HelperCommon.GetUserAgent();
                            string ipPC = HelperCommon.GetUserIP();
                            _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "SignIn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch { }
                        _userService.User_LastLoginDate(dataUser.Id);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            meg.Success = true;
                            meg.ClassCss = "success";
                            meg.Message = "Login success";
                            meg.RedirectUrl = string.Format("{0}", returnUrl);
                            return Json(meg);
                        }
                        else
                        {
                            meg.Success = true;
                            meg.ClassCss = "success";
                            meg.Message = "Login success";
                            meg.RedirectUrl = "/manage";
                            return Json(meg);
                        }
                    }
                    else
                    {
                        meg.ClassCss = "danger";
                        meg.Message = !string.IsNullOrEmpty(message) ? message : "Incorrect account or password";
                    }
                }
                else
                {
                    meg.ClassCss = "danger";
                    meg.Message = "Username not exist";
                }
            }
            else
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect account or password";
            }
            return Json(meg);
        }
        [HttpPost]
        public JsonResult ServerTime()
        {
            int time = _userService.ServerGetTime();
            return Json(time);
        }
        public ActionResult Register()
        {
            var userCurent = GetCurrentUser();
            UserRegister user = new UserRegister();
            if (userCurent != null)
            {
                user.ReferralId = userCurent.Username;
                user.ReferralName = userCurent.Username;
                user.Node = "";
            }
            else
            {
                string code = _userService.GetSettingByKey<string>("ReferralCode.Default", "");
                var userData = _userService.User_GetByCode(code);
                if (userData != null)
                {
                    user.ReferralId = userData.Username;
                    user.ReferralName = userData.Username;
                    user.Node = "";
                }
            }
            return View(user);
        }

        [HttpPost]
                
        public JsonResult Register(string referralId, string fullname, string email, string username, string password, string passwordComfirm, string country, string phone,bool termpolicy)
        {

            Alert meg = ValidateRegister(fullname, email, username, password, passwordComfirm, termpolicy);
            if (meg != null)
            {
                return Json(meg);
            }

            var dataReferral = _userService.User_GetByUsername(referralId);
            if (string.IsNullOrEmpty(referralId) || dataReferral == null)
            {
                meg.Message = "Incorrect link referral";
                meg.ClassCss = "danger";
                return Json(meg);
            }

            meg = Register_User(fullname, email, username, password, dataReferral.Id, country, phone);
            if (meg != null)
            {
                if (!string.IsNullOrEmpty(meg.RedirectUrl))
                {

                    meg.Message = "Register successfully";
                    meg.RedirectUrl = "/login";
                }
                return Json(meg);
            }
            meg.Message = "Register fail.";
            meg.ClassCss = "danger";
            return Json(meg);
        }

        public ActionResult RegisterLink(string referral, string node = "")
        {
            //if (string.IsNullOrEmpty(referral) || (node != SimpleConstant.NODE_LEFT_CODE && node != SimpleConstant.NODE_RIGHT_CODE))
            //{
            //    return Redirect("/login");
            //}

            UserRegister user = new UserRegister();
            var userData = _userService.User_GetByCode(referral);
            if (userData != null)
            {
                user.ReferralId = userData.Code;
                user.ReferralName = userData.Username;
                user.Node = node;
            }
            else
            {
                return Redirect("/login");
            }
            return View(user);
        }

        [HttpPost]
        public JsonResult RegisterLink(string referralId, string email, string username, string password, string passwordComfirm, string country, string phone,bool termpolicy)
        {
            Alert meg = new Alert();

            //if (node != SimpleConstant.NODE_LEFT_CODE && node != SimpleConstant.NODE_RIGHT_CODE)
            //{
            //    meg.Message = "Incorrect link referral";
            //    meg.ClassCss = "danger";
            //    return Json(meg);
            //}

            meg = ValidateRegister(username, email, username, password, passwordComfirm, termpolicy);
            if (meg != null)
            {
                return Json(meg);
            }

            var dataReferral = _userService.User_GetByCode(referralId);
            if (string.IsNullOrEmpty(referralId) || dataReferral == null)
            {
                meg.Message = "Incorrect link referral";
                meg.ClassCss = "danger";
                return Json(meg);
            }

            meg = Register_User(username, email, username, password, dataReferral.Id, country, phone);
            if (meg != null)
            {
                if (!string.IsNullOrEmpty(meg.RedirectUrl))
                {
                    meg.Message = "Register successfully";
                    meg.RedirectUrl = "/login";
                }
                return Json(meg);
            }
            meg.Message = "Register fail.";
            meg.ClassCss = "danger";
            return Json(meg);
        }

        private Alert Register_User(string fullname, string email, string username, string password, int referal_Id, string country, string phone)
        {
            Alert meg = new Alert();
            var enableActiveEmail = _userService.GetSettingByKey<bool>("CheckActiveUserForEmail", false);
            string ipPC = HelperCommon.GetUserIP();
            Random f = new Random();
            int _passFormat = f.Next(1, 4);
            long RnNumCode = f.Next(100000000, 999999999);
            username = username.ToLower();
            var _userEntity = new MUser()
            {
                //Code = Guid.NewGuid().ToString().Substring(0, 7),
                Code = "FB" + RnNumCode.ToString(),
                Username = username,
                Email = email.ToLower(),
                PasswordFormatId = _passFormat,
                LastIpAddress = ipPC,
                IsActive = !enableActiveEmail,
                FullName = CommonHelper.FirstToUpper(!string.IsNullOrEmpty(fullname) ? fullname : username),
                ReferralId = referal_Id,
                Phone = phone,
                Country = country,
                FA3Code = password
            };

            ViewModelUsers usersCopyTrade = new ViewModelUsers();
            usersCopyTrade.FullName = _userEntity.FullName;
            usersCopyTrade.Email = _userEntity.Email;
            usersCopyTrade.Password = password;
            usersCopyTrade.PasswordComfirm = password;
            usersCopyTrade.UserCode = _userEntity.Code;
            usersCopyTrade.ReferralId = referal_Id;
            usersCopyTrade.Username = _userEntity.Username;


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
                meg.UserId = resultUserId;
                _userService.SetRoleForUser(resultUserId, (int)EnumRole.USER);
                _userService.User_WalletAddress_Insert(resultUserId);
                if (enableActiveEmail)
                {
                    var _sessionLogin = new LoginSession
                    {
                        UserId = resultUserId,
                        Token = Guid.NewGuid().ToString(),
                        CreateDate = DateTime.UtcNow,
                        ExpireDate = DateTime.UtcNow.AddHours(48),
                        IsActive = true
                    };
                    _userService.LoginSession_Insert(_sessionLogin);

                    //  RegisterAccountFromCopytrade(usersCopyTrade);
                    //code send mail here
                    string urlHost =  _helper.GetDomain();
                    string url = string.Format("{0}/activate-mail?token={1}", urlHost, _sessionLogin.Token);

                    string body, template = "";
                    body = "You have successfully registered your account on fortrex.io";
                    body += "<br />Your ID: " + _userEntity.Username;

                    body += "<br /> Please click on the link below to activate your account.";

                    var sr = new StreamReader(Server.MapPath("/Content/") + "template-main.html");
                    template = sr.ReadToEnd();
                    template = template.Replace("{titletop}", "Hi, " + _userEntity.Username.ToUpper() + " <br />Welcome to fortrex.io");
                    template = template.Replace("{titlecontent}", "<br/>You have successfully registered your account on fortrex.io");
                    template = template.Replace("{bodycontent}", body);
                    template = template.Replace("{linkaction}", url);
                    template = template.Replace("{messagebutton}", "Click to activate");

                    var mail = new Email
                    {
                        Title = "Activate Account",
                        Body = template,
                        EmailTo = _userEntity.Email
                    };
                    _userService.SendMail(mail);
                    meg.Success = true;
                    meg.Message = "Please check your email  to activate your account";
                    meg.ClassCss = "success";
                    return meg;
                }
                _packagesService.PairName_Favorite_Ins(resultUserId, Constants.PAIR_DEFAULT);
                _packagesService.PairName_Favorite_Ins(resultUserId, Constants.PAIR_DEFAULT2);
                meg.Success = true;
                meg.ClassCss = "success";
                meg.Message = "Register success";
                meg.RedirectUrl = "/login";
                return meg;
            }
            if (!string.IsNullOrEmpty(meg.Message))
                return meg;
            return null;
        }
        private Alert ValidateRegister(string fullname, string email, string username, string password, string passwordComfirm, bool termpolicy)
        {
            Alert meg = new Alert();
           
            if (string.IsNullOrEmpty(email) || !HelperCommon.IsValidEmail(email) || string.IsNullOrWhiteSpace(email))
            {
                meg.Message = "Invalid email address!/n Please include an '@'in the";
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

            if (termpolicy==false)
            {
                meg.Message = "I am over 18 years of age and accept Forbit's Terms and Privacy Policy";
                meg.ClassCss = "danger";
                return meg;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                meg.Message = "Please enter password";
                meg.ClassCss = "danger";
                return meg;
            }

            if (password != passwordComfirm)
            {
                meg.Message = "Password comfirm is incorrect";
                meg.ClassCss = "danger";
                return meg;
            }

            if (password.Length < 6)
            {
                meg.Message = "Password less than 6 characters";
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

            if (string.IsNullOrWhiteSpace(username))
            {
                meg.Message = "Please enter username";
                meg.ClassCss = "danger";
                return meg;
            }
            var UsernameIsLocks = _userService.GetSettingByKey<string>("REGISTER.USERNAME.PROHIBITED", "");
            var arrUsernameIsLocks = UsernameIsLocks.Split(',').Where(p => p.Contains(username.ToLower())).FirstOrDefault();
            if (arrUsernameIsLocks != null)
            {
                meg.Message = "Email already exists.";
                meg.ClassCss = "danger";
                return meg;
            }

            if (!string.IsNullOrEmpty(meg.Message))
                return meg;
            return null;
        }

        public ActionResult ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Email = token;
            return View();
        }

        public ActionResult MailActive(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var data = _userService.LoginSession_GetByToken(token);
            if (data != null)
            {
                if (data.IsActive && data.ExpireDate > DateTime.UtcNow)
                {
                    _userService.Session_UpdateIsActive(token);
                    _userService.MUser_UpdateActive(data.UserId, DateTime.UtcNow);
                    // var price = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, (decimal)0.001);
                    // _packagesService.Update_BNCT(data.UserId, SimpleConstant.REGISTER_BONUS_BNCT / price, data.UserId, -1);
                }
                return Redirect("/login");
            }

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            Alert meg = new Alert();
            email = email.Trim();
            if (HelperCommon.IsValidEmail(email))
            {
                var data = _userService.User_GetByEmail(email);
                if (data != null)
                {
                    if (!data.IsActive)
                    {
                        meg.Message = "Your account has not been activated";
                    }
                    if (data.IsLock)
                    {
                        meg.Message = "Your account has been locked, please contact the admin";
                    }
                    if (data.IsDelete)
                    {
                        meg.Message = "Your account has been canceled";
                    }

                    if (!string.IsNullOrEmpty(meg.Message))
                    {
                        meg.ClassCss = "danger";
                        return Json(meg);
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
                        string url = string.Format("{0}/getpassword?token={1}", urlHost, _sessionLogin.Token);

                        string body, template = "";
                        body = "Hi, " + "<b>" + data.Username;
                        body += "<br/>To reset your password click the URL below. </br><a href=\"" + url + "\">Reset Your Password</a>";

                        var sr = new StreamReader(Server.MapPath("/Content/") + "template-main-noaction.html");
                        template = sr.ReadToEnd();
                        template = template.Replace("{titletop}", "Reset your password");
                        template = template.Replace("{bodycontent}", body);
                        template = template.Replace("{linkaction}", url);
                        template = template.Replace("{messagebutton}", "Click to reset password");
                        var mail = new Email
                        {
                            Title = "Changes Password",
                            Body = template,
                            EmailTo = data.Email
                        };
                        _userService.SendMail(mail);

                        meg.Success = true;
                        meg.ClassCss = "success";
                        return Json(meg);
                    }
                    catch { }
                }
                else
                {
                    meg.Message = "Cannot find your email in our system, please re-input your email!";
                }
            }
            else
            {
                meg.Message = "Your email is invalid";
            }

            meg.ClassCss = "danger";
            return Json(meg);
        }

        public ActionResult GetChangePassword(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var userId = _userService.Session_GetUserIdByToken(token);
                if (userId > 0)
                {
                    ViewBag.TokenUser = token;
                    return View();
                }
            }
            ViewBag.ErrorMsg = Common.HelperCommon.GetError(6);
            return View();
        }

        [HttpPost]
        public ActionResult GetChangePassword(string passNew, string passNewRe, string token)
        {
            Alert meg = new Alert();
            if (string.IsNullOrEmpty(passNew) || passNew != passNewRe)
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect password";
                return Json(meg);
            }

            if (passNew.Length < 6)
            {
                meg.Message = "Password less than 6 characters";
                meg.ClassCss = "danger";
                return Json(meg);
            }


            var userId = _userService.Session_GetUserIdByToken(token);//userToken khi quen mat khau
            if (userId == 0)
            {
                var userCode = _userService.User_GetByCode(token); //userToken la code khi chon thay doi mat khau
                if (userCode != null)
                {
                    userId = userCode.Id;
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
                    FormsAuthentication.SignOut();

                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Get password success";
                    meg.RedirectUrl = "/login";
                    return Json(meg);
                }
            }
            meg.ClassCss = "danger";
            meg.Message = "Incorrect password";
            return Json(meg);
        }

        [Authorize]
        public ActionResult Logout()
        {
            var userCurent = GetCurrentUser();
            if (userCurent != null)
            {
                try
                {
                    string userAgent = HelperCommon.GetUserAgent();
                    string ipPC = HelperCommon.GetUserIP();
                    _userService.User_LogDevice(userCurent.Id, ipPC, userAgent, "SignOut", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch
                {

                }
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        public ActionResult TransferConfirm(string token)
        {
            Alert meg = new Alert();
            meg.Success = false;
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/dashboard");
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
                    }
                }
                else
                {
                    meg.Message = "Token expire";
                }
            }
            return View(meg);
        }
        public ActionResult WithdrawConfirm(string token)
        {
            Alert meg = new Alert();
            meg.Success = false;
            if (string.IsNullOrEmpty(token))
            {
                meg.Message = "Token not found";
                return View(meg);
                //return Redirect("/dashboard");
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
                    int rel = _userService.User_Withdraw_Apply(data);
                    if (rel > 0)
                    {
                        meg.Message = "Confirmed";
                        meg.Success = true;
                    }
                    else
                    {
                        meg.Message = "Token expire";
                    }
                }
                else
                {
                    meg.Message = "Token expire";
                }
            }
            return View(meg);
        }

        private void RegisterAccountFromCopytrade(ViewModelUsers user)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // HTTP POST
                var baseUrl = "http://localhost:8018";// Request.Url.GetLeftPart(UriPartial.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //ViewModelUsers param = new ViewModelUsers() { UserCode = user.Code, FullName = user.FullName, Email =user.Email, Password = user.Password, PasswordComfirm = user.Password};

                string contents = JsonConvert.SerializeObject(user);
                //client.PostAsync("/api/user/register", new StringContent(contents, Encoding.UTF8, "application/json"));
                var response = client.PostAsync("/api/user/register", new StringContent(contents, Encoding.UTF8, "application/json")).Result;    
                //string res = "";
                //using (HttpContent content = response.Content)
                //{
                //    // ... Read the string.
                //   Task<CustomJsonResult> result = JsonConvert.DeserializeObject<Task<CustomJsonResult>>(content.ReadAsStringAsync().Result.ToString());
                //    //Console.WriteLine(result.ToString());
                //}
            }
        }
    }
}
