using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Lib.Cache;
using Lib.Data.Repository.User;
using Lib.Domain.User;
using RestSharp;
using System.Web.Script.Serialization;
using System.Collections;
using System.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Lib.Domain.Simples;
using Lib.Domain.ContentStatics;
using Lib.Domain.CoinTransactions;
using Lib.Domain.BuyCoins;
using Lib.Domain.Withdraws;
using Lib.Domain.TransactionHistorys;
using System.ComponentModel;
using Lib.Domain.Packages;
using System.IO;
using System.Runtime.Serialization.Json;
using Lib.Domain.Coins;
using Lib.Domain;
using System.Net.Http.Headers;
using Lib.Domain.Packages.Trades;
using Lib.Domain.Transfers;

namespace Lib.Service.Service.User
{
    public interface IUserService
    {
        string GetValueSetting(string key);
        T GetSettingByKey<T>(string key, T defaultValue = default(T));

        MUser User_GetByEmail(string email);
        MUser User_GetByUsername(string username);
        MUser User_GetByUserId(int userId);
        MUser User_GetByCode(string code);
        int User_UpdateProfile(MUser user);
        int User_UpdateEmail(MUser user);
        int User_Register(MUser user);
        int User_LastLoginDate(int userId);
        int LoginSession_Insert(LoginSession login);
        int LoginSession_Update(string token);
        LoginSession LoginSession_GetByToken(string token);
        UserSocialMap UserSocialMap_GetSocialId(int type, string socialId);
        int UserSocialMap_Insert(int userId, string socialId, int type);
        void SetRoleForUser(int userId, int role);
        List<string> User_GetRoleByUserId(int userId);
        List<string> User_GetRoleByUsername(string username);

        void DBLog_Insert(string name, string body, int? referentId, int type = 1);

        Task SendMail(Email mail);
        int Session_GetUserIdByToken(string token);
        int User_ChangePassword(MUser user);
        int Session_UpdateIsActive(string token);
        int MUser_UpdateActive(int id, DateTime date);
        int User_WalletAddress_Insert(int userId);
        decimal BitcoinPrice();
        decimal EthereumPrice();
        decimal RipplePrice();
        decimal StellaPrice();
        decimal GetCoinPrice(string coinname = "");
        MarketsExchange ExchangeMarkets();
        CoinPairPrice ExchangeGetPriceCoin(string exchangename,string pair);
        Country CountryFromIP(string ip);
        ContentStatic ContentStatic_GetById(int userId);
        List<SettingEntity> Manage_Setting_GetAll(int pageIndex, int pageSize, out int total, string whereClause);
        SettingEntity Manage_Setting_GetById(int id);
        int Manage_Setting_Insert(SettingEntity model);
        int Manage_Setting_Update(SettingEntity model);
        string User_GetUniqueKeyByUserId(int id);
        void User_UpdateUniqueKeyByUserId(int userId, string uniqueKey);
        User_WalletAddress User_WalletAddress_GetByUserId(int userId);
        User_WalletAddress User_WalletAddress_CopyTrade_GetByUserName(string Username);
        int User_WalletAddress_Update(User_WalletAddress model);
        List<TransactionCoin> Admin_CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<TransactionCoin> CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<TransactionCoin> Deposit_Last_Get(int type);
        int BuyCoin_Insert(BuyCoin model);
        int BuyCoinWithETH_Insert(BuyCoin model);
        List<BuyCoinList> Admin_BuyCoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause);
        int Withdraw_Insert(Withdraw model);
        List<HistoryTransaction> Admin_HistoryTransaction_List(int pageIndex, int pageSize, out int total, string whereClause);
       
        List<Withdraw> Withdraw_History(int pageIndex, int pageSize, out int total, string whereClause);
        List<BuyCoinList> Admin_BuyCoinManage_List(int pageIndex, int pageSize, out int total, string whereClause);
        int BuyCoin_UpdateStatus(int id, int status, int userId, DateTime approveDate);
        List<WithdrawList> Admin_WithdrawManage_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<UserData> UserData_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<UserData> UserData_List_KYC(int pageIndex, int pageSize, out int total, string whereClause);
        int Withdraw_UpdateStatus(int id, int status, int userId, DateTime approveDate, string hash);
        decimal Total_CoinBuyByUserId(int userId, DateTime day);
        string MailTemplate_GetByName(string name);
        List<Dblog> Manage_DBLog_GetAll(int pageIndex, int pageSize, out int total, string whereClause);
        Dblog Manage_DBLog_GetById(int id);
        int Manage_Delete_LogById(int[] ids);
        int UserCountAll();
        decimal TotalCoinSold();
        BuyCoinEntity BuyCoin_GetUserIdById(int id);
        int GetReferralIdByUserId(int userId);
        int BuyCoin_BonusForUser(int userId, int fromUser, decimal coin);
        int Address_CheckExists(string address);
        int SendCoin_SendToAddress(int userId, int toUserId, string address, decimal coin, string tranc);
        List<TotalCoinChildren> TotalCoinChildrenOfUser(int userId);
        List<User_WalletAddress> Lending_ListUserNotLending();
        List<Users_Marketing_Bonus> Users_Marketing_Bonus_GetBy_Type(string type);
        //List<Referral> Admin_Referral_List(int pageIndex, int pageSize, out int total, string whereClause, int userId, int child);
        BonusCoin GetBonusById(int id);
        string User_Tron_Create(TronCoin tron);
        int User_Extension_Insert(User_Extension model);
        User_Extension User_Extension_GetDetail(int userId);
        int User_Extension_Delete(int userId);
        int User_Extension_UpdateStatus(int userId);
        int User_Tron_Refund(int userId);
        ManageDasboard ManageDasboard_Detail();
        int UnLock_When_Not_Reinvestment(int userId);
        int ServerGetTime();
        List<SystemSchedule> System_GetTool();
        List<QANote> Manage_QANote_GetAll(int pageIndex, int pageSize, out int total, string whereClause);
        QANote Manage_QANote_GetById(int id);
        int Manage_QANote_Insert(QANote model);
        QA_Total AQ_GetTotal();
        int LastActivityUpdate(int userId);
        string Get_Address_By_UserId(int userId);
        decimal Convert_BTC_To_USD(decimal btc);
        decimal Convert_USD_To_ETH(decimal usd);
        decimal Convert_XRP_To_USD(decimal xrp);
        decimal Convert_USD_To_BTC(decimal usd);
        decimal Convert_USD_To_XRP(decimal usd);
        decimal Convert_USD_To_XGT(decimal usd);
        int User_WalletAddress_Bonus_Lucky(int userId, decimal bonus, int byUser, int packageId);
        int TransactionSession_Insert(TransactionSession tran);
        TransactionSession TransactionSession_GetBy_Token(string token);
        int User_Transfer_Apply(TransactionSession tran);
        string GetUsernameByWallet(string wallet, string type);
        decimal? Get_Max_Invest_By_Uid(int uid);
        int User_DepositBy_USDT_Insert(UserDepositByUSDT tran);
        List<UserDepositByUSDT> User_DepositBy_USDT_Lst(int pageIndex, int pageSize, out int total, string whereClause);
        int User_DepositBy_USDT_ApproveOrCancel(UserDepositByUSDT tran, int type);
        int User_LogDevice(int userId, string ip, string userAgent, string status, string createOn);
        List<LogDerviceList> LogDervice_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<UsersAffiliates> Account_Referal_List(int userId, int getlevel, int pageIndex, int pageSize, out int total);
        List<decimal> User_Get_List_Amount(int userId, string method);
        UserTooltip UserTooltip_ById(int id);
        List<CoinTransactionList> Admin_CoinTransactionList(int pageIndex, int pageSize, out int total, string whereClause);
        int Admin_CoinApprove(string addressWallet, string transactionId, int methodPayment);
        UserPending User_LockPending_Get(int userId);
        int User_Withdraw_Apply(TransactionSession tran);
        int ArbittrageTransaction_Ins(TradeHistoryTransaction model);
        List<ArbittrageTransaction_Lst> ArbittrageTransaction_Lst(int pageIndex, int pageSize, out int total, string whereClause);
        bool Validate_User_Withdraw(int userId);
        int Ticket_Ins(TicketEntity ticket);
        int Ticket_Update(int id, string ReplyBy, string ReplyMessages);
        List<TicketEntity> Ticket_Lst(int Userid);
        List<WithdrawETH> PayProfitDaily_List(int pageIndex, int pageSize, out int total, string whereClause);
        int Withdraw_Update_Tranfer_Status(int id, int status);
        WithdrawETH PayProfitDaily_Get(int id);
        List<HighchartSyncTrade> Admin_Trading_List(int pageIndex, int pageSize, out int total, string whereClause);
        List<AccountBalance> AccountBalance(int userId,string formatCommas="N1");
        int Token_Create_Or_Update(int uid, string token, DateTime expire);
        int Token_GetUserIdByToken(string token);
        int Transfer_USD_From_Forbit_To_CopyTrade(TransfersFromToWalletModel model);
        int Transfer_USD_From_CopyTrade_To_Forbit(TransfersFromToWalletModel model);
        //public List<SystemSchedule> OrdersResult()
        //{
        //    return _userRepository.System_GetTool();
        //}

        List<TransferHistoryModel> Transfer_History(int pageIndex, int pageSize, out int total, string whereClause);
        List<Totalvolumebuysell> Totalvolumebuysells();
        DasboardSumData Dasboard_SumData(int userId, int type);
        AffiliateStatistic Get_Affiliate_Statistic(int userId);
        List<NetworkStatistic> Network_Report_Trading_Bonus(int userId);
        List<NetworkStatistic> Dasboard_Trading_Sumary(int userId);
        List<NetworkLevelSum> Network_Count_Menber(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<NetworkLevelSum> Network_Count_Menber(int userId)
        {
            return _userRepository.Network_Count_Menber(userId);
        }

        public List<NetworkStatistic> Dasboard_Trading_Sumary(int userId)
        {
            return _userRepository.Dasboard_Trading_Sumary(userId);
        }

        public List<NetworkStatistic> Network_Report_Trading_Bonus(int userId)
        {
            return _userRepository.Network_Report_Trading_Bonus(userId);
        }

        public AffiliateStatistic Get_Affiliate_Statistic(int userId)
        {
            return _userRepository.Get_Affiliate_Statistic(userId);
        }

        public DasboardSumData Dasboard_SumData(int userId, int type)
        {
            return _userRepository.Dasboard_SumData(userId, type);
        }

        public int Token_GetUserIdByToken(string token)
        {
            return _userRepository.Token_GetUserIdByToken(token);
        }

        public int Token_Create_Or_Update(int uid, string token, DateTime expire)
        {
            return _userRepository.Token_Create_Or_Update(uid, token, expire);
        }

        public WithdrawETH PayProfitDaily_Get(int id)
        {
            return _userRepository.PayProfitDaily_Get(id);
        }

        public int Withdraw_Update_Tranfer_Status(int id, int status)
        {
            return _userRepository.Withdraw_Update_Tranfer_Status(id, status);
        }

        public bool Validate_User_Withdraw(int userId)
        {
            return _userRepository.Validate_User_Withdraw(userId);
        }

        public UserPending User_LockPending_Get(int userId)
        {
            return _userRepository.User_LockPending_Get(userId);
        }

        public int Admin_CoinApprove(string addressWallet, string transactionId, int methodPayment)
        {
            return _userRepository.Admin_CoinApprove(addressWallet, transactionId, methodPayment);
        }

        public UserTooltip UserTooltip_ById(int id)
        {
            return _userRepository.UserTooltip_ById(id);
        }

        public List<decimal> User_Get_List_Amount(int userId, string method)
        {
            return _userRepository.User_Get_List_Amount(userId, method);
        }
        public int User_LogDevice(int userId, string ip, string userAgent, string status, string createOn)
        {
            return _userRepository.User_LogDevice(userId, ip, userAgent, status, createOn);
        }
        public decimal? Get_Max_Invest_By_Uid(int uid)
        {
            return _userRepository.Get_Max_Invest_By_Uid(uid);
        }
        public string GetUsernameByWallet(string wallet, string type)
        {
            return _userRepository.GetUsernameByWallet(wallet, type);
        }
        public int User_Transfer_Apply(TransactionSession tran)
        {
            return _userRepository.User_Transfer_Apply(tran);
        }
        public TransactionSession TransactionSession_GetBy_Token(string token)
        {
            return _userRepository.TransactionSession_GetBy_Token(token);
        }
        public int TransactionSession_Insert(TransactionSession tran)
        {
            return _userRepository.TransactionSession_Insert(tran);
        }
        public int User_WalletAddress_Bonus_Lucky(int userId, decimal bonus, int byUser, int packageId)
        {
            return _userRepository.User_WalletAddress_Bonus_Lucky(userId, bonus, byUser, packageId);
        }
        public decimal Convert_USD_To_BTC(decimal usd)
        {
            try
            {
                var oneBtc = BitcoinPrice();
                return Math.Round(usd / oneBtc, 8);
            }
            catch
            {
                return 0;
            }
        }
        public decimal Convert_USD_To_ETH(decimal usd)
        {
            try
            {
                var oneEth = EthereumPrice();
                return Math.Round(usd / oneEth, 8);
            }
            catch
            {
                return 0;
            }
        }
        public decimal Convert_USD_To_XRP(decimal usd)
        {
            try
            {
                var oneXrp = RipplePrice();
                return Math.Round(usd / oneXrp, 8);
            }
            catch
            {
                return 0;
            }
        }
        public decimal Convert_USD_To_XGT(decimal usd)
        {
            try
            {
                var oneXgt = GetSettingByKey<decimal>("Price_XGT", (decimal)0.005);
                return Math.Round(usd / oneXgt, 8);
            }
            catch
            {
                return 0;
            }
        }
        public decimal Convert_BTC_To_USD(decimal btc)
        {
            var oneBtc = BitcoinPrice();
            return Math.Round(btc * oneBtc, 8);
        }
        public decimal Convert_XRP_To_USD(decimal xrp)
        {
            var oneXrp = RipplePrice();
            return Math.Round(xrp * oneXrp, 8);
        }
        public string Get_Address_By_UserId(int userId)
        {
            return _userRepository.Get_Address_By_UserId(userId);
        }
        public int LastActivityUpdate(int userId)
        {
            return _userRepository.LastActivityUpdate(userId);
        }
        public QA_Total AQ_GetTotal()
        {
            return _userRepository.AQ_GetTotal();
        }
        public int Manage_QANote_Insert(QANote model)
        {
            return _userRepository.Manage_QANote_Insert(model);
        }
        public QANote Manage_QANote_GetById(int id)
        {
            return _userRepository.Manage_QANote_GetById(id);
        }
        public List<QANote> Manage_QANote_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Manage_QANote_GetAll(pageIndex, pageSize, out total, whereClause);
        }

        public List<SystemSchedule> System_GetTool()
        {
            return _userRepository.System_GetTool();
        }

        public int UnLock_When_Not_Reinvestment(int userId)
        {
            return _userRepository.UnLock_When_Not_Reinvestment(userId);
        }
        public int User_Tron_Refund(int userId)
        {
            return _userRepository.User_Tron_Refund(userId);
        }
        public int User_Extension_UpdateStatus(int userId)
        {
            return _userRepository.User_Extension_UpdateStatus(userId);
        }
        public int User_Extension_Delete(int userId)
        {
            return _userRepository.User_Extension_Delete(userId);
        }
        public User_Extension User_Extension_GetDetail(int userId)
        {
            return _userRepository.User_Extension_GetDetail(userId);
        }
        public int User_Extension_Insert(User_Extension model)
        {
            return _userRepository.User_Extension_Insert(model);
        }
        public string User_Tron_Create(TronCoin tron)
        {
            return _userRepository.User_Tron_Create(tron);
        }

        public string GetValueSetting(string key)
        {
            var data = _userRepository.GetValueSetting();
            return data.Where(s => s.Name == key).Select(s => s.Value).FirstOrDefault();
        }

        public T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;

            key = key.Trim().ToLowerInvariant();

            var settings = GetAllSetting();
            if (settings.ContainsKey(key))
            {
                var setting = settings[key];
                return setting.As<T>();
            }
            else
            {
                //SetSetting(key, defaultValue, true);
            }
            return defaultValue;
        }

        private IDictionary<string, Setting> GetAllSetting()
        {
            var cacheKey = CacheKeyManager.SettingValueCache.Cache__GetAllSettingValue;
            return CacheExtensions.Get(cacheKey, TimeSpan.FromDays(180), () =>
            {
                var query = _userRepository.GetValueSetting();
                return query.ToDictionary(s => s.Name.ToLowerInvariant());
            });
        }

        public MUser User_GetByEmail(string email)
        {
            return _userRepository.User_GetByEmail(email);
        }
        public MUser User_GetByUsername(string username)
        {
            return _userRepository.User_GetByUsername(username);
        }
        public MUser User_GetByUserId(int userId)
        {
            return _userRepository.User_GetByUserId(userId);
        }
        public MUser User_GetByCode(string code)
        {
            return _userRepository.User_GetByCode(code);
        }
        public int User_UpdateProfile(MUser user)
        {
            return _userRepository.User_UpdateProfile(user);
        }
            public int User_UpdateEmail(MUser user)
        {
            return _userRepository.User_UpdateEmail(user);
        }
        public int User_Register(MUser user)
        {
            return _userRepository.User_Register(user);
        }
        public int User_LastLoginDate(int userId)
        {
            return _userRepository.User_LastLoginDate(userId);
        }
        public int LoginSession_Insert(LoginSession login)
        {
            return _userRepository.LoginSession_Insert(login);
        }
        public int LoginSession_Update(string token)
        {
            return _userRepository.LoginSession_Update(token);
        }
        public LoginSession LoginSession_GetByToken(string token)
        {
            return _userRepository.LoginSession_GetByToken(token);
        }
        public UserSocialMap UserSocialMap_GetSocialId(int type, string socialId)
        {
            return _userRepository.UserSocialMap_GetSocialId(type, socialId);
        }
        public int UserSocialMap_Insert(int userId, string socialId, int type)
        {
            return _userRepository.UserSocialMap_Insert(userId, socialId, type);
        }
        public void SetRoleForUser(int userId, int role)
        {
            _userRepository.SetRoleForUser(userId, role);
        }
        public List<string> User_GetRoleByUserId(int userId)
        {
            return _userRepository.User_GetRoleByUserId(userId);
        }
        public List<string> User_GetRoleByUsername(string username)
        {
            return _userRepository.User_GetRoleByUsername(username);
        }

        public void DBLog_Insert(string name, string body, int? referentId, int type = 1)
        {
            _userRepository.DBLog_Insert(name, body, referentId, type);
        }

        public async Task SendMail(Email mail)
        {
            await SendNotificationAsync(mail.EmailTo, mail.Title, mail.Body);
        }

        private Task<bool> SendNotificationAsync(string emailTo, string title, string body, string ccemail = "")
        {
            var sent = false;
            try
            {
                string addMail = GetSettingByKey<string>("Mail.Address");
                string smtpHost = GetSettingByKey<string>("Mail.Smtp.Host");
                int smtpPost = GetSettingByKey<int>("Mail.Smtp.Port");
                bool smtpEnableSsl = GetSettingByKey<bool>("Mail.Smtp.EnableSsl");
                string smtpUserName = GetSettingByKey<string>("Mail.SmtpUserName");
                string smtpPassword = GetSettingByKey<string>("Mail.SmtpPassword");
                string displayName = GetSettingByKey<string>("Mail.DisplayName");

                MailAddress ma = new MailAddress(addMail, displayName);
                MailAddress maTo = new MailAddress(emailTo);
                using (MailMessage mm = new MailMessage(ma, maTo))
                {
                    mm.Subject = title;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    // cc email
                    if (!string.IsNullOrEmpty(ccemail))
                    {
                        var arrcc = ccemail.Split(',');
                        foreach (var add in arrcc)
                        {
                            if (!string.IsNullOrEmpty(add))
                            {
                                mm.CC.Add(add);
                            }
                        }
                    }
                    // end cc email
                    NetworkCredential NetworkCred = new NetworkCredential(smtpUserName, smtpPassword);
                    SmtpClient smtp = new SmtpClient(smtpHost);
                    smtp.Host = smtpHost;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = smtpPost;
                    smtp.EnableSsl = smtpEnableSsl;
                    smtp.SendCompleted += SendCompletedCallback;
                    smtp.Send(mm);
                    sent = true;
                }
            }
            catch (Exception ex)
            {
                DBLog_Insert("Send mail to: " + emailTo, ex.Message, null);
            }
            return Task.FromResult(sent);
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            string token = (string)e.UserState;

            if (e.Cancelled)
            {
                DBLog_Insert(string.Format("{0} Send canceled.", token), "", null);
            }
            if (e.Error != null)
            {
                DBLog_Insert(string.Format("{0} {1}", token, e.Error.ToString()), "", null);
            }
        }

        public int Session_GetUserIdByToken(string token)
        {
            return _userRepository.Session_GetUserIdByToken(token);
        }
        public int User_ChangePassword(MUser user)
        {
            return _userRepository.User_ChangePassword(user);
        }
        public int Session_UpdateIsActive(string token)
        {
            return _userRepository.Session_UpdateIsActive(token);
        }
        public int MUser_UpdateActive(int id, DateTime date)
        {
            return _userRepository.MUser_UpdateActive(id, date);
        }
        public int User_WalletAddress_Insert(int userId)
        {
            return _userRepository.User_WalletAddress_Insert(userId);
        }

        public decimal BitcoinPrice()
        {
            //try
            //{
            //    string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinmarketcap.com/v1/ticker") + "/bitcoin/";
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
            //                                       SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //    var client = new WebClient();
            //    //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
            //    var resultToken = client.DownloadString(urlApi);
            //    var obj = JArray.Parse(resultToken);
            //    return (decimal)obj[0]["price_usd"];
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
            try
            {
                string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinbase.com/v2/prices/BTC-USD/spot");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new WebClient();
                //client.Headers.Add
                client.Headers.Add("Bearer", "abd90df5f27a7b170cd775abf89d632b350b7c1c9d53e08b340cd9832ce52c2c");
                var resultToken = client.DownloadString(urlApi);
                var obj = JsonConvert.DeserializeObject<CoinBaseGetPriceCoinData>(resultToken);
                return (decimal)obj.data.amount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal EthereumPrice()
        {

            //try
            //{
            //    string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinmarketcap.com/v1/ticker") + "/ethereum/";
            //    ServicePointManager.Expect100Continue = true;
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
            //                                       SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

            //    var client = new WebClient();
            //    //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
            //    var resultToken = client.DownloadString(urlApi);
            //    var obj = JArray.Parse(resultToken);
            //    return (decimal)obj[0]["price_usd"];
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
            try
            {
                string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinbase.com/v2/prices/ETH-USD/spot");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new WebClient();
                //client.Headers.Add
                client.Headers.Add("Bearer", "abd90df5f27a7b170cd775abf89d632b350b7c1c9d53e08b340cd9832ce52c2c");
                var resultToken = client.DownloadString(urlApi);
                var obj = JsonConvert.DeserializeObject<CoinBaseGetPriceCoinData>(resultToken);
                return (decimal)obj.data.amount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal StellaPrice()
        {

            try
            {
                string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinmarketcap.com/v1/ticker") + "/stellar/";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

                var client = new WebClient();
                //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
                var resultToken = client.DownloadString(urlApi);
                var obj = JArray.Parse(resultToken);
                return (decimal)obj[0]["price_usd"];
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public decimal RipplePrice()
        {
            try
            {
                string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinmarketcap.com/v1/ticker") + "/ripple/";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new WebClient();
                client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
                var resultToken = client.DownloadString(urlApi);
                var obj = JArray.Parse(resultToken);
                return (decimal)obj[0]["price_usd"];
            }
            catch
            {
                return 0;
            }
        }
        public decimal GetCoinPrice(string coinname = "")
        {
            var cacheKey = CacheKeyManager.SettingValueCache.Cache__GetUSDByBTC;
            return CacheExtensions.Get(cacheKey, TimeSpan.FromMinutes(5), () =>
            {
                try
                {
                    string urlApi = GetSettingByKey<string>("CoinMarketCapUrl", "https://api.coinmarketcap.com/v1/ticker") + "/" + coinname + "/";
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                    //                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                    var client = new WebClient();
                    client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
                    var resultToken = client.DownloadString(urlApi);
                    var obj = JArray.Parse(resultToken);
                    return (decimal)obj[0]["price_usd"];
                }
                catch
                {
                    return 0;
                }
            });
        }
        public MarketsExchange ExchangeMarkets()
        {
            try
            {
                string urlApi = GetSettingByKey<string>("cryptowatUrl", "https://api.cryptowat.ch/markets");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new WebClient();
                //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
                var result = new CustomJsonResult();
                result.Result = client.DownloadString(urlApi);
                //var obj = JArray.Parse(resultToken);
                MarketsExchange markets = new MarketsExchange();
                if (result!=null && result.Result!=null)
                {
                    markets = JsonConvert.DeserializeObject<MarketsExchange>(result.Result.ToString());
                }
                return markets;
            }
            catch(Exception ex)
            {
                return new MarketsExchange();
            }
        }
        public CoinPairPrice ExchangeGetPriceCoin(string exchangename, string pair)
        {
            try
            {
                string urlApi = GetSettingByKey<string>("cryptowatUrl",string.Format("https://api.cryptowat.ch/markets/{0}/{1}/price", exchangename, pair));
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new WebClient();
                //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
                var result = new CustomJsonResult();
                result.Result = client.DownloadString(urlApi);
                //var obj = JArray.Parse(resultToken);
                CoinPairPrice markets = new CoinPairPrice();
                if (result != null && result.Result != null)
                {
                    markets = JsonConvert.DeserializeObject<CoinPairPrice>(result.Result.ToString());
                }

                return markets;
            }
            catch (Exception ex)
            {
                return new CoinPairPrice();
            }
        }
        
        public Country CountryFromIP(string ip)
        {
            try
            {
                string urlApi = string.Format("http://ip-api.com/json/{0}", ip);
                var client = new WebClient();
                var resultToken = client.DownloadString(urlApi);
                var obj = JArray.Parse(resultToken);
                return new Country {
                    Name = obj["country"].ToString(),
                    City = obj["city"].ToString()
                };
            }
            catch
            {
                return null;
            }
        }
        public ContentStatic ContentStatic_GetById(int userId)
        {
            return _userRepository.ContentStatic_GetById(userId);
        }

        public List<SettingEntity> Manage_Setting_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Manage_Setting_GetAll(pageIndex, pageSize, out total, whereClause);
        }
        public SettingEntity Manage_Setting_GetById(int id)
        {
            return _userRepository.Manage_Setting_GetById(id);
        }
        public int Manage_Setting_Insert(SettingEntity model)
        {
            return _userRepository.Manage_Setting_Insert(model);
        }
        public int Manage_Setting_Update(SettingEntity model)
        {
            return _userRepository.Manage_Setting_Update(model);
        }
        public string User_GetUniqueKeyByUserId(int id)
        {
            return _userRepository.User_GetUniqueKeyByUserId(id);
        }
        public void User_UpdateUniqueKeyByUserId(int userId, string uniqueKey)
        {
            _userRepository.User_UpdateUniqueKeyByUserId(userId, uniqueKey);
        }
        public User_WalletAddress User_WalletAddress_GetByUserId(int userId)
        {
            return _userRepository.User_WalletAddress_GetByUserId(userId);
        }
        // Get Data Wallet COPYTRADE
        public User_WalletAddress User_WalletAddress_CopyTrade_GetByUserName(string Username)
        {
            return _userRepository.User_WalletAddress_CopyTrade_GetByUserName(Username);
        }
        public int User_WalletAddress_Update(User_WalletAddress model)
        {
            return _userRepository.User_WalletAddress_Update(model);
        }
        public List<TransactionCoin> Admin_CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_CoinTransaction_List(pageIndex, pageSize, out total, whereClause);
        }
        public List<TransactionCoin> CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.CoinTransaction_List(pageIndex, pageSize, out total, whereClause);
        }
        public List<LogDerviceList> LogDervice_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.LogDervice_List(pageIndex, pageSize, out total, whereClause);
        }
        public List<TransactionCoin> Deposit_Last_Get(int type)
        {
            return _userRepository.Deposit_Last_Get(type);
        }
        public int BuyCoin_Insert(BuyCoin model)
        {
            return _userRepository.BuyCoin_Insert(model);
        }
        public int BuyCoinWithETH_Insert(BuyCoin model)
        {
            return _userRepository.BuyCoinWithETH_Insert(model);
        }
        public List<BuyCoinList> Admin_BuyCoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_BuyCoinTransaction_List(pageIndex, pageSize, out total, whereClause);
        }
        public int Withdraw_Insert(Withdraw model)
        {
            return _userRepository.Withdraw_Insert(model);
        }
        public List<HistoryTransaction> Admin_HistoryTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_HistoryTransaction_List(pageIndex, pageSize, out total, whereClause);
        }


        public List<HighchartSyncTrade> Admin_Trading_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_Trading_List(pageIndex, pageSize, out total, whereClause);
        }

        public List<UsersAffiliates> Account_Referal_List(int userId, int getlevel, int pageIndex, int pageSize, out int total)
        {
            return _userRepository.Account_Referal_List(userId, getlevel,pageIndex, pageSize, out total);
        }
        public List<Withdraw> Withdraw_History(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Withdraw_History(pageIndex, pageSize, out total, whereClause);
        }
        public List<BuyCoinList> Admin_BuyCoinManage_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_BuyCoinManage_List(pageIndex, pageSize, out total, whereClause);
        }
        public int BuyCoin_UpdateStatus(int id, int status, int userId, DateTime approveDate)
        {
            int result = _userRepository.BuyCoin_UpdateStatus(id, status, userId, approveDate);
            if (result == 1)
            {
                var dataCoin = _userRepository.BuyCoin_GetUserIdById(id);
                if (dataCoin != null && dataCoin.Status == (int)BuycoinStatus.Approve)
                {
                    int referralId = _userRepository.GetReferralIdByUserId(dataCoin.UserId);
                    var bonus = 5; //5%
                    decimal coin = (dataCoin.NumberCoin * bonus) / 100;
                    _userRepository.BuyCoin_BonusForUser(referralId, dataCoin.UserId, coin);
                }
            }
            return result;
        }

        public List<WithdrawETH> PayProfitDaily_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.PayProfitDaily_List(pageIndex, pageSize, out total, whereClause);
        }

        public List<WithdrawList> Admin_WithdrawManage_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_WithdrawManage_List(pageIndex, pageSize, out total, whereClause);
        }

        public List<CoinTransactionList> Admin_CoinTransactionList(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Admin_CoinTransactionList(pageIndex, pageSize, out total, whereClause);
        }

        public List<UserData> UserData_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.UserData_List(pageIndex, pageSize, out total, whereClause);
        }
        public List<UserData> UserData_List_KYC(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.UserData_List_KYC(pageIndex, pageSize, out total, whereClause);
        }
        public int Withdraw_UpdateStatus(int id, int status, int userId, DateTime approveDate, string hash)
        {
            return _userRepository.Withdraw_UpdateStatus(id, status, userId, approveDate, hash);
        }
        public decimal Total_CoinBuyByUserId(int userId, DateTime day)
        {
            return _userRepository.Total_CoinBuyByUserId(userId, day);
        }
        public string MailTemplate_GetByName(string name)
        {
            return _userRepository.MailTemplate_GetByName(name);
        }
        public List<Dblog> Manage_DBLog_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Manage_DBLog_GetAll(pageIndex, pageSize, out total, whereClause);
        }
        public Dblog Manage_DBLog_GetById(int id)
        {
            return _userRepository.Manage_DBLog_GetById(id);
        }
        public int Manage_Delete_LogById(int[] ids)
        {
            return _userRepository.Manage_Delete_LogById(string.Join(",", ids));
        }
        public int UserCountAll()
        {
            var cacheKey = CacheKeyManager.SettingValueCache.Cache__GetTotalUser;
            return CacheExtensions.Get(cacheKey, TimeSpan.FromMinutes(15), () =>
            {
                return _userRepository.UserCountAll();
            });
        }
        public decimal TotalCoinSold()
        {
            return _userRepository.TotalCoinSold();
        }
        public BuyCoinEntity BuyCoin_GetUserIdById(int id)
        {
            return _userRepository.BuyCoin_GetUserIdById(id);
        }
        public int GetReferralIdByUserId(int userId)
        {
            return _userRepository.GetReferralIdByUserId(userId);
        }
        public int BuyCoin_BonusForUser(int userId, int fromUser, decimal coin)
        {
            return _userRepository.BuyCoin_BonusForUser(userId, fromUser, coin);
        }
        public int Address_CheckExists(string address)
        {
            return _userRepository.Address_CheckExists(address);
        }
        public int SendCoin_SendToAddress(int userId, int toUserId, string address, decimal coin, string tranc)
        {
            return _userRepository.SendCoin_SendToAddress(userId, toUserId, address, coin, tranc);
        }
        public List<TotalCoinChildren> TotalCoinChildrenOfUser(int userId)
        {
            return _userRepository.TotalCoinChildrenOfUser(userId);
        }
        public List<User_WalletAddress> Lending_ListUserNotLending()
        {
            return _userRepository.Lending_ListUserNotLending();
        }
        public List<Users_Marketing_Bonus> Users_Marketing_Bonus_GetBy_Type(string type)
        {
            return _userRepository.Users_Marketing_Bonus_GetBy_Type(type);
        }
        //public List<Referral> Admin_Referral_List(int pageIndex, int pageSize, out int total, string whereClause, int userId, int child)
        //{
        //    return _userRepository.Admin_Referral_List(pageIndex, pageSize, out total, whereClause, userId, child);
        //}
        public BonusCoin GetBonusById(int id)
        {
            return _userRepository.GetBonusById(id);
        }
        public ManageDasboard ManageDasboard_Detail()
        {
            return _userRepository.ManageDasboard_Detail();
        }
        public int ServerGetTime()
        {
            return _userRepository.ServerGetTime();
        }
        public int User_DepositBy_USDT_Insert(UserDepositByUSDT tran)
        {
            return _userRepository.User_DepositBy_USDT_Insert(tran);
        }
        public List<UserDepositByUSDT> User_DepositBy_USDT_Lst(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.User_DepositBy_USDT_Lst( pageIndex,  pageSize, out total,  whereClause);
        }
        public int User_DepositBy_USDT_ApproveOrCancel(UserDepositByUSDT tran, int type)
        {
            return _userRepository.User_DepositBy_USDT_ApproveOrCancel(tran,type);
        }
        public int User_Withdraw_Apply(TransactionSession tran)
        {
            return _userRepository.User_Withdraw_Apply(tran);
        }
        public int ArbittrageTransaction_Ins(TradeHistoryTransaction model)
        {
            try
            {
                return _userRepository.ArbittrageTransaction_Ins(model);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public List<ArbittrageTransaction_Lst> ArbittrageTransaction_Lst(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.ArbittrageTransaction_Lst(pageIndex, pageSize, out total, whereClause);
        }
        public int Ticket_Ins(TicketEntity ticket)
        {
           return _userRepository.Ticket_Ins(ticket);
        }
        public int Ticket_Update(int id, string ReplyBy, string ReplyMessages)
        {
           return _userRepository.Ticket_Update(id,ReplyBy,ReplyMessages);
        }
        public List<TicketEntity> Ticket_Lst(int Userid)
        {
            return _userRepository.Ticket_Lst(Userid);
        }
        #region balance
        public List<AccountBalance> AccountBalance(int userId, string formatCommas)
        {
            return _userRepository.AccountBalance(userId, formatCommas);
        }
        #endregion
        public int Transfer_USD_From_CopyTrade_To_Forbit(TransfersFromToWalletModel model)
        {
            return _userRepository.Transfer_USD_From_CopyTrade_To_Forbit(model);
        }

        public int Transfer_USD_From_Forbit_To_CopyTrade(TransfersFromToWalletModel model)
        {
            return _userRepository.Transfer_USD_From_Forbit_To_CopyTrade(model);
        }

        public List<TransferHistoryModel> Transfer_History(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _userRepository.Transfer_History(pageIndex, pageSize, out total, whereClause);
        }

        public List<Totalvolumebuysell> Totalvolumebuysells()
        {
            return _userRepository.Totalvolumebuysells();
        }

    }
}