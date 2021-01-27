using Lib.Core.Data;
using Lib.Data.MapBuilder;
using Lib.Data.ResultSetMapper;
using Lib.Domain.BuyCoins;
using Lib.Domain.Coins;
using Lib.Domain.CoinTransactions;
using Lib.Domain.ContentStatics;
using Lib.Domain.Packages;
using Lib.Domain.Packages.Trades;
using Lib.Domain.Simples;
using Lib.Domain.TransactionHistorys;
using Lib.Domain.Transfers;
using Lib.Domain.User;
using Lib.Domain.Withdraws;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lib.Data.Repository.User
{
    public interface IUserRepository
    {
        List<Setting> GetValueSetting();
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

        int Session_GetUserIdByToken(string token);
        int User_ChangePassword(MUser user);
        int Session_UpdateIsActive(string token);
        int MUser_UpdateActive(int id, DateTime date);
        int User_WalletAddress_Insert(int userId);
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
        int Manage_Delete_LogById(string ids);
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
        List<AccountBalance> AccountBalance(int userId,string formatCommas);
        int Token_Create_Or_Update(int uid, string token, DateTime expire);
        int Token_GetUserIdByToken(string token);
        int Transfer_USD_From_Forbit_To_CopyTrade(TransfersFromToWalletModel model);
        int Transfer_USD_From_CopyTrade_To_Forbit(TransfersFromToWalletModel model);
        List<TransferHistoryModel>Transfer_History(int pageIndex, int pageSize, out int total, string whereClause);
        List<Totalvolumebuysell> Totalvolumebuysells();
        DasboardSumData Dasboard_SumData(int userId, int type);
        AffiliateStatistic Get_Affiliate_Statistic(int userId);
        List<NetworkStatistic> Network_Report_Trading_Bonus(int userId);
        List<NetworkStatistic> Dasboard_Trading_Sumary(int userId);
        List<NetworkLevelSum> Network_Count_Menber(int userId);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {

        public List<NetworkLevelSum> Network_Count_Menber(int userId)
        {
            var map = NewsMapBuilder<NetworkLevelSum>.BuildAllProperties();
            return _db.CreateSprocAccessor("Network_Count_Menber", map).Execute(userId).ToList();
        }

        public List<NetworkStatistic> Dasboard_Trading_Sumary(int userId)
        {
            var map = NewsMapBuilder<NetworkStatistic>.BuildAllProperties();
            return _db.CreateSprocAccessor("Dasboard_Trading_Sumary", map).Execute(userId).ToList();
        }

        public List<NetworkStatistic> Network_Report_Trading_Bonus(int userId)
        {
            var map = NewsMapBuilder<NetworkStatistic>.BuildAllProperties();
            return _db.CreateSprocAccessor("Network_Report_Trading_Bonus", map).Execute(userId).ToList();
        }

        public AffiliateStatistic Get_Affiliate_Statistic(int userId)
        {
            var map = NewsMapBuilder<AffiliateStatistic>.BuildAllProperties();
            return _db.CreateSprocAccessor("Get_Affiliate_Statistic", map).Execute(userId).FirstOrDefault();
        }

        public DasboardSumData Dasboard_SumData(int userId, int type)
        {
            var map = NewsMapBuilder<DasboardSumData>.BuildAllProperties();
            return _db.CreateSprocAccessor("Dasboard_SumData", map).Execute(userId, type).FirstOrDefault();
        }

        public int Token_GetUserIdByToken(string token)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Token_GetUserIdByToken", map).Execute(token).FirstOrDefault();
        }

        public int Token_Create_Or_Update(int uid, string token, DateTime expire)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Token_Create_Or_Update", map).Execute(uid,
                token, expire).FirstOrDefault();
        }

        public WithdrawETH PayProfitDaily_Get(int id)
        {
            var map = NewsMapBuilder<WithdrawETH>.BuildAllProperties();
            return _db.CreateSprocAccessor("PayProfitDaily_Get", map).Execute(id).FirstOrDefault();
        }

        public int Withdraw_Update_Tranfer_Status(int id, int status)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Withdraw_Update_Tranfer_Status", map).Execute(id,
                status).FirstOrDefault();
        }

        public bool Validate_User_Withdraw(int userId)
        {
            var map = new BooleanResultSetMapper();
            return _db.CreateSprocAccessor("Validate_User_Withdraw", map).Execute(userId).FirstOrDefault();
        }
        public UserPending User_LockPending_Get(int userId)
        {
            var map = NewsMapBuilder<UserPending>.BuildAllProperties();
            return _db.CreateSprocAccessor("User_LockPending_Get", map).Execute(userId).FirstOrDefault();
        }

        public int Admin_CoinApprove(string addressWallet, string transactionId, int methodPayment)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Admin_UpdateMoneyDeposit", map).Execute(addressWallet, transactionId, methodPayment).FirstOrDefault();
        }
        public UserTooltip UserTooltip_ById(int id)
        {
            var map = NewsMapBuilder<UserTooltip>.BuildAllProperties();
            return _db.CreateSprocAccessor("UserTooltip_ById", map).Execute(id).FirstOrDefault();
        }
        public List<decimal> User_Get_List_Amount(int userId, string method)
        {
            var map = new DecimalResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Get_List_Amount", map);
            return query.Execute(userId, method).ToList();
        }
        public List<UsersAffiliates> Account_Referal_List(int userId, int getlevel, int pageIndex, int pageSize, out int total)
        {
            var map = NewsMapBuilder<UsersAffiliates>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("UserId", userId, DbType.Int32),
                    _db.CreateParameter("Level", getlevel, DbType.Int32),
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output)
            };
            var data = _db.Execute("Account_Referal_List", map, parameters).ToList();
            total = parameters[4].Value != DBNull.Value ? Convert.ToInt32(parameters[4].Value) : 0;
            return data;
        }
        public decimal? Get_Max_Invest_By_Uid(int uid)
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("Get_Max_Invest_By_Uid", map).Execute(uid).FirstOrDefault();
        }
        public string GetUsernameByWallet(string wallet, string type)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("GetUsernameByWallet", map).Execute(wallet, type).FirstOrDefault();
        }
        public int User_Transfer_Apply(TransactionSession tran)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Transfer_Apply", map).Execute(tran.Id, tran.ReferentId).FirstOrDefault();
        }
        public TransactionSession TransactionSession_GetBy_Token(string token)
        {
            var map = NewsMapBuilder<TransactionSession>.BuildAllProperties();
            return _db.CreateSprocAccessor("TransactionSession_GetBy_Token", map).Execute(token).FirstOrDefault();
        }
        public int TransactionSession_Insert(TransactionSession tran)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("TransactionSession_Insert", map).Execute(tran.UserId,
                tran.Token,
                tran.CreateDate,
                tran.ExpireDate,
                tran.IsActive,
                tran.ReferentId,
                tran.Status).FirstOrDefault();
        }
        public int User_WalletAddress_Bonus_Lucky(int userId, decimal bonus, int byUser, int packageId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_WalletAddress_Bonus_Lucky", map).Execute(userId, bonus, byUser, packageId).FirstOrDefault();
        }
        public string Get_Address_By_UserId(int userId)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("Get_Address_By_UserId", map).Execute(userId).FirstOrDefault();
        }
        public int LastActivityUpdate(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("LastActivityUpdate", map).Execute(userId).FirstOrDefault();
        }
        public QA_Total AQ_GetTotal()
        {
            var map = NewsMapBuilder<QA_Total>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("AQ_GetTotal", map);
            return query.Execute().FirstOrDefault();
        }
        public int Manage_QANote_Insert(QANote model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Manage_QANote_Insert", map).Execute(model.Amount,
                model.Note,
                model.UserId).FirstOrDefault();
        }

        public QANote Manage_QANote_GetById(int id)
        {
            var map = NewsMapBuilder<QANote>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Manage_QANote_GetById", map);
            return query.Execute(id).FirstOrDefault();
        }

        public List<QANote> Manage_QANote_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<QANote>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Manage_QANote_GetAll", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<SystemSchedule> System_GetTool()
        {
            var map = NewsMapBuilder<SystemSchedule>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("System_GetTool", map);
            return query.Execute().ToList();
        }

        public int UnLock_When_Not_Reinvestment(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("UnLock_When_Not_Reinvestment", map).Execute(userId).FirstOrDefault();
        }
        public int User_Tron_Refund(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Tron_Refund", map).Execute(userId).FirstOrDefault();
        }
        public int User_Extension_UpdateStatus(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Extension_UpdateStatus", map).Execute(userId).FirstOrDefault();
        }
        public int User_Extension_Delete(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Extension_Delete", map).Execute(userId).FirstOrDefault();
        }
        public User_Extension User_Extension_GetDetail(int userId)
        {
            var map = NewsMapBuilder<User_Extension>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_Extension_GetDetail", map);
            return query.Execute(userId).FirstOrDefault();
        }
        public int User_Extension_Insert(User_Extension model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Extension_Insert", map).Execute(model.UserId,
                model.Firstname,
                model.Lastname,
                model.PhoneNatural,
                model.PhoneNumber,
                model.Country,
                model.IdentificationType,
                model.IdentificationNumber,
                model.FontSideUrl,
                model.BackSideUrl,
                model.SelfieUrl,
                model.Status,
                model.CreateOn).FirstOrDefault();
        }
        public string User_Tron_Create(TronCoin tron)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("User_Tron_Create", map).Execute(tron.UserId,
                tron.Key,
                tron.Address,
                tron.Balance).FirstOrDefault();
        }
        public List<Setting> GetValueSetting()
        {
            var map = NewsMapBuilder<Setting>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Setting_GetAll", map);
            return query.Execute().ToList();
        }

        public MUser User_GetByEmail(string email)
        {
            var map = NewsMapBuilder<MUser>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetByEmail", map);
            return query.Execute(email).FirstOrDefault();
        }
        public MUser User_GetByUsername(string username)
        {
            var map = NewsMapBuilder<MUser>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetByUsername", map);
            return query.Execute(username).FirstOrDefault();
        }
        public MUser User_GetByUserId(int userId)
        {
            var map = NewsMapBuilder<MUser>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetByUserId", map);
            return query.Execute(userId).FirstOrDefault();
        }
        public MUser User_GetByCode(string code)
        {
            var map = NewsMapBuilder<MUser>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetByCode", map);
            return query.Execute(code).FirstOrDefault();
        }
        public int User_UpdateProfile(MUser user)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_UpdateProfile", map).Execute(user.Id,
                user.FullName,
                user.Phone).FirstOrDefault();
        }
        public int User_UpdateEmail(MUser user)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_UpdateEmail", map).Execute(user.Id,
                user.Email).FirstOrDefault();
        }
        public int User_Register(MUser user)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Register", map).Execute(user.Code,
                user.Username,
                user.Email,
                user.Password,
                user.PasswordFormatId,
                user.PasswordSaft,
                user.LastIpAddress,
                user.IsActive,
                user.FullName,
                user.ReferralId,
                user.FA3Code,
                user.Country,
                user.Phone).FirstOrDefault();
        }
        public int User_LastLoginDate(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_LastLoginDate", map).Execute(userId).FirstOrDefault();
        }
        public int User_LogDevice(int userId, string ip, string userAgent, string status, string createOn)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_LogDevice", map).Execute(userId,
                ip,
                userAgent,
                status,
                createOn).FirstOrDefault();
        }
        public int LoginSession_Insert(LoginSession login)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("LoginSession_Insert", map).Execute(login.UserId,
                login.Token,
                login.CreateDate,
                login.ExpireDate,
                login.IsActive).FirstOrDefault();
        }
        public int LoginSession_Update(string token)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("LoginSession_Update", map).Execute(token).FirstOrDefault();
        }
        public LoginSession LoginSession_GetByToken(string token)
        {
            var map = NewsMapBuilder<LoginSession>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("LoginSession_GetByToken", map);
            return query.Execute(token).FirstOrDefault();
        }
        public UserSocialMap UserSocialMap_GetSocialId(int type, string socialId)
        {
            var map = NewsMapBuilder<UserSocialMap>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("UserSocialMap_GetSocialId", map);
            return query.Execute(type, socialId).FirstOrDefault();
        }
        public int UserSocialMap_Insert(int userId, string socialId, int type)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("UserSocialMap_Insert", map).Execute(userId,
                socialId,
                type).FirstOrDefault();
        }
        public void SetRoleForUser(int userId, int role)
        {
            _db.ExecuteNonQuery("User_Role_Mapping_Insert", userId, role);
        }
        public List<string> User_GetRoleByUserId(int userId)
        {
            var map = NewsMapBuilder<string>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetRoleByUserId", map);
            return query.Execute(userId).ToList();
        }

        public List<string> User_GetRoleByUsername(string username)
        {
            var map = NewsMapBuilder<string>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_GetRoleByUsername", map);
            return query.Execute(username).ToList();
        }

        public void DBLog_Insert(string name, string body, int? referentId, int type=1)
        {
            _db.ExecuteNonQuery("DBLog_Insert", name, body, referentId, type);
        }

        public int Session_GetUserIdByToken(string token)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Session_GetUserIdByToken", map).Execute(token).FirstOrDefault();
        }

        public int User_ChangePassword(MUser user)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_ChangePassword", map).Execute(user.Id, user.PasswordSaft, user.Password, user.FA3Code).FirstOrDefault();
        }

        public int Session_UpdateIsActive(string token)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Session_UpdateIsActive", map).Execute(token).FirstOrDefault();
        }

        public int MUser_UpdateActive(int id, DateTime date)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("MUser_UpdateActive", map).Execute(id, date).FirstOrDefault();
        }

        public int User_WalletAddress_Insert(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_WalletAddress_Insert", map).Execute(userId).FirstOrDefault();
        }
        public ContentStatic ContentStatic_GetById(int userId)
        {
            var map = NewsMapBuilder<ContentStatic>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ContentStatic_GetById", map);
            return query.Execute(userId).FirstOrDefault();
        }

        public List<SettingEntity> Manage_Setting_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<SettingEntity>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Manage_Setting_GetAll", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;
            return data;
        }
        public SettingEntity Manage_Setting_GetById(int id)
        {
            var map = NewsMapBuilder<SettingEntity>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Manage_Setting_GetById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int Manage_Setting_Insert(SettingEntity model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Manage_Setting_Insert", map).Execute(model.Name,
                model.Value).FirstOrDefault();
        }
        public int Manage_Setting_Update(SettingEntity model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Manage_Setting_Update", map).Execute(model.Id,
                model.Name,
                model.Value).FirstOrDefault();
        }
        public string User_GetUniqueKeyByUserId(int id)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("User_GetUniqueKeyByUserId", map).Execute(id).FirstOrDefault();
        }
        public void User_UpdateUniqueKeyByUserId(int userId, string uniqueKey)
        {
            _db.ExecuteNonQuery("User_UpdateUniqueKeyByUserId", userId, uniqueKey);
        }
        public User_WalletAddress User_WalletAddress_GetByUserId(int userId)
        {
            var map = NewsMapBuilder<User_WalletAddress>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_WalletAddress_GetByUserId", map);
            return query.Execute(userId).FirstOrDefault();
        }

        // create method call User_Wallet_CopyTrade
        public User_WalletAddress User_WalletAddress_CopyTrade_GetByUserName(string Username)
        {
            var map = NewsMapBuilder<User_WalletAddress>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_WalletAddress_CopyTrade_GetByUserName", map);
            return query.Execute(Username).FirstOrDefault();
        }
        public int User_WalletAddress_Update(User_WalletAddress model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_WalletAddress_Update", map).Execute(model.UserId,
                model.WalletBTC,
                model.WalletETH,
                model.WalletMy,
                model.WalletStocks).FirstOrDefault();
        }
        public List<TransactionCoin> Admin_CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<TransactionCoin>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_CoinTransaction_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public List<TransactionCoin> CoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<TransactionCoin>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("CoinTransaction_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<LogDerviceList> LogDervice_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<LogDerviceList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("LogDervice_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<TransactionCoin> Deposit_Last_Get(int type)
        {
            var map = NewsMapBuilder<TransactionCoin>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Deposit_Last_Get", map);
            return query.Execute(type).ToList();
        }
        public int BuyCoin_Insert(BuyCoin model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("BuyCoin_Insert", map).Execute(model.UserId,
                model.NumberCoin,
                model.PriceUSD,
                model.CreateDate,
                model.UpdateDate,
                model.Status,
                model.BEHToUSD,
                model.Transaction,
                model.MethodPaymentId).FirstOrDefault();
        }
        public int BuyCoinWithETH_Insert(BuyCoin model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("BuyCoinWithETH_Insert", map).Execute(model.UserId,
                model.NumberCoin,
                model.PriceUSD,
                model.CreateDate,
                model.UpdateDate,
                model.Status,
                model.BEHToUSD,
                model.Transaction,
                model.MethodPaymentId).FirstOrDefault();
        }
        public List<BuyCoinList> Admin_BuyCoinTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<BuyCoinList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_BuyCoinTransaction_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;
            return data;
        }
        public int Withdraw_Insert(Withdraw model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Withdraw_Insert", map).Execute(model.UserId,
                model.FromType,
                model.ToType,
                model.AmountSet,
                model.Fee,
                model.AmountGet,
                model.Transaction,
                model.Status,
                model.Method,
                model.TokenConfirm,
                model.IsConfirmEmail).FirstOrDefault();
        }
        public int Transfer_USD_From_Forbit_To_CopyTrade(TransfersFromToWalletModel model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Transfer_USD_From_Forbit_To_CopyTrade", map).Execute(
                model.UserIDForbit,
                model.Username,
                model.AmountUSD
                ).FirstOrDefault();
        }

        public int Transfer_USD_From_CopyTrade_To_Forbit(TransfersFromToWalletModel model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Transfer_USD_From_CopyTrade_To_Forbit", map).Execute(
                model.UserIDForbit,
                model.Username,
                model.AmountUSD
                ).FirstOrDefault();
        }


        
        public List<HistoryTransaction> Admin_HistoryTransaction_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<HistoryTransaction>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_HistoryTransaction_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<HighchartSyncTrade> Admin_Trading_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<HighchartSyncTrade>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_Trading_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<Withdraw> Withdraw_History(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<Withdraw>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Withdraw_History", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;
            return data;
        }
        public List<BuyCoinList> Admin_BuyCoinManage_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<BuyCoinList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_BuyCoinManage_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public int BuyCoin_UpdateStatus(int id, int status, int userId, DateTime approveDate)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("BuyCoin_UpdateStatus", map).Execute(id,
                status,
                userId,
                approveDate).FirstOrDefault();
        }

        public List<WithdrawETH> PayProfitDaily_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<WithdrawETH>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("PayProfitDaily_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<WithdrawList> Admin_WithdrawManage_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<WithdrawList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_WithdrawManage_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<CoinTransactionList> Admin_CoinTransactionList(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<CoinTransactionList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_ConTransaction_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<UserData> UserData_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<UserData>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("UserData_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public List<UserData> UserData_List_KYC(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<UserData>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("UserData_List_KYC", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public int Withdraw_UpdateStatus(int id, int status, int userId, DateTime approveDate, string hash)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Withdraw_UpdateStatus", map).Execute(id,
                status,
                userId,
                approveDate,
                hash).FirstOrDefault();
        }
        public decimal Total_CoinBuyByUserId(int userId, DateTime day)
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("Total_CoinBuyByUserId", map).Execute(userId,
                day).FirstOrDefault();
        }
        public string MailTemplate_GetByName(string name)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("MailTemplate_GetByName", map).Execute(name).FirstOrDefault();
        }
        public List<Dblog> Manage_DBLog_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<Dblog>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Admin_Dblog_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public Dblog Manage_DBLog_GetById(int id)
        {
            var map = NewsMapBuilder<Dblog>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Manage_DBLog_GetById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int Manage_Delete_LogById(string ids)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Manage_Delete_LogById", map).Execute(ids).FirstOrDefault();
        }
        public int UserCountAll()
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_CountAll", map).Execute().FirstOrDefault();
        }
        public decimal TotalCoinSold()
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("TotalCoinSold", map).Execute().FirstOrDefault();
        }
        public BuyCoinEntity BuyCoin_GetUserIdById(int id)
        {
            var map = NewsMapBuilder<BuyCoinEntity>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("BuyCoin_GetUserIdById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int GetReferralIdByUserId(int userId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("GetReferralIdByUserId", map).Execute(userId).FirstOrDefault();
        }
        public int BuyCoin_BonusForUser(int userId, int fromUser, decimal coin)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("BuyCoin_BonusForUser", map).Execute(userId, fromUser, coin).FirstOrDefault();
        }
        public int Address_CheckExists(string address)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Address_CheckExists", map).Execute(address).FirstOrDefault();
        }
        public int SendCoin_SendToAddress(int userId, int toUserId, string address, decimal coin, string tranc)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("SendCoin_SendToAddress", map).Execute(userId, toUserId, address, coin, tranc).FirstOrDefault();
        }
        public List<TotalCoinChildren> TotalCoinChildrenOfUser(int userId)
        {
            var map = NewsMapBuilder<TotalCoinChildren>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("BuyCoin_TotalCoinChildrenOfUser", map);
            return query.Execute(userId).ToList();
        }
        public List<User_WalletAddress> Lending_ListUserNotLending()
        {
            var map = NewsMapBuilder<User_WalletAddress>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Lending_ListUserNotLending", map);
            return query.Execute().ToList();
        }
        public List<Users_Marketing_Bonus> Users_Marketing_Bonus_GetBy_Type(string type)
        {
            var map = NewsMapBuilder<Users_Marketing_Bonus>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Users_Marketing_Bonus_GetBy_Type", map);
            return query.Execute(type).ToList();
        }
        //public List<Referral> Admin_Referral_List(int pageIndex, int pageSize, out int total, string whereClause, int userId, int child)
        //{
        //    var map = NewsMapBuilder<Referral>.MapAllProperties().Build();
        //    var parameters = new[] {
        //            _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
        //            _db.CreateParameter("PageSize", pageSize, DbType.Int32),
        //            _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
        //            _db.CreateParameter("WhereClause", whereClause, DbType.String),
        //            _db.CreateParameter("UserId", userId, DbType.Int32),
        //            _db.CreateParameter("Child", child, DbType.Int32)
        //    };
        //    var data = _db.Execute("Admin_Referral_List", map, parameters).ToList();
        //    total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

        //    return data;
        //}
        public BonusCoin GetBonusById(int id)
        {
            var map = NewsMapBuilder<BonusCoin>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("GetBonusById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public ManageDasboard ManageDasboard_Detail()
        {
            var map = NewsMapBuilder<ManageDasboard>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ManageDasboard_Detail", map);
            return query.Execute().FirstOrDefault();
        }
        public int ServerGetTime()
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("ServerGetTime", map);
            return query.Execute().FirstOrDefault();
        }
        public int User_DepositBy_USDT_Insert(UserDepositByUSDT tran)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_DepositBy_USDT_Insert", map)
                .Execute(
                tran.UserId,
                tran.Amount,
                tran.TxHash).FirstOrDefault();
        }
        public List<UserDepositByUSDT> User_DepositBy_USDT_Lst(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<UserDepositByUSDT>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("User_DepositBy_USDT_Lst", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public int User_DepositBy_USDT_ApproveOrCancel(UserDepositByUSDT tran,int type)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_DepositBy_USDT_ApproveOrCancel", map)
                .Execute(
                tran.Id,
                type).FirstOrDefault();
        }
        public int User_Withdraw_Apply(TransactionSession tran)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Withdraw_Apply", map).Execute(tran.Id, tran.ReferentId).FirstOrDefault();
        }
        public int ArbittrageTransaction_Ins(TradeHistoryTransaction model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("ArbittrageTransaction_Ins", map).Execute(model.BuyExchange, model.SellExchange
                , model.BuyPrice, model.SellPrice, model.PercentDifference,model.CoinPair, model.TradeAt,model.TransactionID).FirstOrDefault();
        }
        public List<ArbittrageTransaction_Lst> ArbittrageTransaction_Lst(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<ArbittrageTransaction_Lst>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("ArbittrageTransaction_Lst", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public int Ticket_Ins(TicketEntity ticket)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Ticket_Ins", map)
                .Execute(
                ticket.UserId,
                ticket.FullName,
                ticket.Email,
                ticket.PhoneNumber,
                ticket.Subject,
                ticket.Messages
                ).FirstOrDefault();
        }
        public int Ticket_Update(int id, string ReplyBy,string ReplyMessages)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Ticket_Update", map)
                .Execute(id,ReplyBy,ReplyMessages ).FirstOrDefault();
        }
        public List<TicketEntity> Ticket_Lst(int Userid)
        {
            var map = NewsMapBuilder<TicketEntity>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("@P_UserID", Userid, DbType.Int32)
            };
            var data = _db.Execute("Ticket_Lst", map, parameters).ToList();
           
            return data;
        }
        public List<AccountBalance> AccountBalance(int userId,string formatCommas)
        {
            var map = NewsMapBuilder<AccountBalance>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("@UserId", userId, DbType.Int32),
                     _db.CreateParameter("@formatcommas", formatCommas, DbType.String)
            };
            var data = _db.Execute("AccountBalance", map, parameters).ToList();

            return data;
        }


        public List<TransferHistoryModel> Transfer_History(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<TransferHistoryModel>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Transfer_History", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;
            return data;
        }
        public List<Totalvolumebuysell> Totalvolumebuysells()
        {
            var map = NewsMapBuilder<Totalvolumebuysell>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tools_Totalvolumebuysell", map);
            return query.Execute().ToList();
        }
    }
}