using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Lib.Data.MapBuilder;
using Lib.Core.Data;
using Lib.Data.ResultSetMapper;
using Lib.Data.Repository.Models;
using System;
using Lib.Data.Repository.Models.Packages;
using Lib.Data.Repository.Models.TronCoins;
using Lib.Domain.Coins;
using System.Net;
using Lib.Data.Domain;
using Newtonsoft.Json;
using Lib.Data.Domain.Trade;
using Lib.Domain.AsynTabs;
using Lib.Domain.KLines;

namespace Lib.Data.Repository.Tasks
{
    public class TaskRepository : BaseRepository
    {
        public long ConvertToUnixTime(DateTime datetime)
        {
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime());
            return (long)span.TotalSeconds;

        }
        public Int32 UnixTimeStampUTC()
        {
            Int32 unixTimeStamp;
            DateTime currentTime = DateTime.Now;
            DateTime zuluTime = currentTime.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
            return unixTimeStamp;
        }

        public List<AsynTab> AsynTab_Get(int type, int status)
        {
            var map = NewsMapBuilder<AsynTab>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("AsynTab_Get", map);
            return query.Execute(type, status).ToList();
        }

        public int AsynTab_Update(int id, int status)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("AsynTab_Update", map);
            return query.Execute(id, status).FirstOrDefault();
        }

        public List<ParentInvest> Muser_Get_Referal_Id(int uid)
        {
            var map = NewsMapBuilder<ParentInvest>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Muser_Get_Referal_Id", map);
            return query.Execute(uid).ToList();
        }

        public List<VolumnSystemModel> VomumeSystem_Get(int top, int lastId, int dayOfWeek, int process)
        {
            var map = NewsMapBuilder<VolumnSystemModel>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("VomumeSystem_Get", map);
            return query.Execute(top, lastId, dayOfWeek, process).ToList();
        }

        public List<TotalUserTrade> Get_Total_Trade_Of_UIDS(string uids, DateTime from, DateTime to)
        {
            var map = NewsMapBuilder<TotalUserTrade>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_Total_Trade_Of_UIDS", map);
            return query.Execute(uids, from, to).ToList();
        }

        public List<ProcessLevelData> Get_Process_Level_data(string uids)
        {
            var map = NewsMapBuilder<ProcessLevelData>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_Process_Level_data", map);
            return query.Execute(uids).ToList();
        }

        public int Update_Process_Level_data(int uid, int level, decimal masterIb)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Update_Process_Level_data", map);
            return query.Execute(uid, level, masterIb).FirstOrDefault();
        }

        public int VomumeSystem_Update(int id, int process)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("VomumeSystem_Update", map);
            return query.Execute(id, process).FirstOrDefault();
        }

        public int VomumeSystem_Insert(string uids, int weekOfYear, int money)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("VomumeSystem_Insert", map);
            return query.Execute(uids, weekOfYear, money).FirstOrDefault();
        }

        public List<int> Invest_Get_MasterIB(string uids)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Invest_Get_MasterIB", map);
            return query.Execute(uids).ToList();
        }

        public int Packages_BonusF_Insert(int from, int to, decimal bonus, int level, int packageId, DateTime create, int type, string meg, int byType)
        {
            var map = new IntegerResultSetMapper();
            //var query = _db.CreateSprocAccessor("Packages_BonusF_Insert", map);
            var query = _db.CreateSprocAccessor("Packages_BonusF_Insert_New", map);
            return query.Execute(from,
                to,
                bonus,
                level,
                packageId,
                0,
                create,
                type,
                meg,
                byType).FirstOrDefault();
        }

        #region task
        public List<ScheduleTask> ScheduleTask_GetByProjectId(int projectType)
        {
            var map = NewsMapBuilder<ScheduleTask>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ScheduleTask_GetByProjectId", map);
            return query.Execute(projectType).ToList();
        }

        public ScheduleTask GetTaskByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return null;

            var map = NewsMapBuilder<ScheduleTask>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ScheduleTask_GetTaskByType", map);
            return query.Execute(type).FirstOrDefault();
        }

        public int UpdateTask(ScheduleTask task)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("ScheduleTask_UpdateTask", map);
            return query.Execute(task.Id,
                task.LastStartUtc,
                task.LastEndUtc,
                task.LastSuccessUtc).FirstOrDefault();
        }
        #endregion

        #region loaddata
        public string GetLastedSync(int id)
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("SystemConfig_SysnTransactionCoinLastest", map);

            return query.Execute(id).FirstOrDefault();
        }

        public string Transaction_GetAddress(string address1, string address2, int methodPayment)
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("Transaction_GetAddress", map);

            return query.Execute(address1, address2, methodPayment).FirstOrDefault();
        }

        public List<string> Transaction_GetAddressBTC(string addresss)
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("Transaction_GetAddressBTC", map);

            return query.Execute(addresss).ToList();
        }

        public void LastedSyncUpdate(string date, int id)
        {
            _db.ExecuteNonQuery("SystemConfig_SysnTransactionCoinLastest_Update", date, id);
        }

        public void UpdateSetting(string key, string value)
        {
            _db.ExecuteNonQuery("Tool_Setting_Update", key, value);
        }

        public int CoinTransactionInsert(TransactionCoin model)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("CoinTransaction_Insert", map);
            return query.Execute(model.Type,
                model.Status,
                model.BTC,
                model.USD,
                model.AddressWallet,
                model.CreateDate,
                model.UpdateDate,
                model.HashCode,
                model.TransactionId,
                model.MethodPayment).FirstOrDefault();
        }

        public int CoinTransaction_Clone_Insert(TransactionCoin model)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("CoinTransaction_Clone_Insert", map);
            return query.Execute(model.Type,
                model.Status,
                model.BTC,
                model.USD,
                model.AddressWallet,
                model.CreateDate,
                model.UpdateDate,
                model.HashCode,
                model.TransactionId,
                model.MethodPayment).FirstOrDefault();
        }

        public List<TransactionCoin> CoinTransaction_GetDataClone()
        {
            var map = NewsMapBuilder<TransactionCoin>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("CoinTransaction_GetDataClone", map);
            return query.Execute().ToList();
        }

        public List<CoinTransaction> Tool_CompleteDeposit_Btc()
        {
            var map = NewsMapBuilder<CoinTransaction>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_CompleteDeposit_Btc", map);
            return query.Execute().ToList();
        }

        public int Admin_UpdateMoneyDeposit(string addressWallet, string transactionId, int methodPayment)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Admin_UpdateMoneyDeposit", map);
            return query.Execute(addressWallet, transactionId, methodPayment).FirstOrDefault();
        }

        public bool CoinTransaction_CheckExistHashCode(string hashCode)
        {
            var map = new BooleanResultSetMapper();
            var query = _db.CreateSprocAccessor("CoinTransaction_CheckExistHashCode", map);

            return query.Execute(hashCode).FirstOrDefault();
        }

        public int CoinTransaction_DataClone_Delete(int id)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("CoinTransaction_DataClone_Delete", map);

            return query.Execute(id).FirstOrDefault();
        }

        public int ErrorLog_Insert(int? referentceId, string message, string title, int type = 0)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("DBLog_Insert", map);
            return query.Execute(title,
                message,
                referentceId,
                type).FirstOrDefault();
        }
        public List<TransactionCoin> CoinTransaction_GetDataPendding(int methodPayment)
        {
            var map = NewsMapBuilder<TransactionCoin>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("CoinTransaction_GetDataPendding", map);
            return query.Execute(methodPayment).ToList();
        }

        public void CoinTransaction_StatusUpdate(string status, int id, string transId, string updateAt)
        {
            _db.ExecuteNonQuery("CoinTransaction_StatusUpdate", status, id, transId, updateAt);
        }

        public void Tool_UpdateWalletUserId(string wallet, string transId, int methodPayment)
        {
            _db.ExecuteNonQuery("Tool_UpdateWalletUserId", wallet, transId, methodPayment);
        }
        #endregion

        #region marketing
        public List<MailMarketing> Tool_GetAddressMail(bool? isActive)
        {
            var map = NewsMapBuilder<MailMarketing>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetAddressMail", map);
            return query.Execute(isActive).ToList();
        }
        public MailMarketing Tool_GetAddressMailSendUserMining()
        {
            var map = NewsMapBuilder<MailMarketing>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetAddressMailSendUserMining", map);
            return query.Execute().FirstOrDefault();
        }
        public List<AddressMail> Tool_GetAddressAllMailUser(int top, int lastId)
        {
            var map = NewsMapBuilder<AddressMail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetAddressAllMailUser", map);
            return query.Execute(top, lastId).ToList();
        }

        public List<Genaral_Marketing_Mail> Tool_GetExtensionMail(int top, int lastId, int marketingId)
        {
            var map = NewsMapBuilder<Genaral_Marketing_Mail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetExtensionMail", map);
            return query.Execute(top, lastId, marketingId).ToList();
        }

        public void Tool_GetExtensionMail_IsSend(int id)
        {
            _db.ExecuteNonQuery("Tool_GetExtensionMail_IsSend", id);
        }

        public void Genaral_Marketing_Mail_Insert(Genaral_Marketing_Mail detail)
        {
            _db.ExecuteNonQuery("Geanaral_Mail_Marketting", detail.Email,
                detail.DisplayName,
                detail.Host,
                detail.Port,
                detail.Username,
                detail.Password,
                detail.EnableSsl,
                detail.UseDefaultCaredential,
                detail.ToMail,
                detail.Title,
                detail.Body,
                detail.IsSend,
                detail.MarketingId
            );
        }

        public List<MailPromotion> Get_All_MailPromotion()
        {
            var map = NewsMapBuilder<MailPromotion>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_All_MailPromotion", map);
            return query.Execute().ToList();
        }
        public int PromotionSendMail_Update(int id)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("PromotionSendMail_Update", map);

            return query.Execute(id).FirstOrDefault();
        }
        public List<AddressMail> Tool_GetAddressAllMailUser_Mining(int top)
        {
            var map = NewsMapBuilder<AddressMail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetAddressAllMailUser_Mining", map);
            return query.Execute(top).ToList();
        }
        public List<AddressMail> Tool_GetAddressAllMailUserBuyCoin(int top, int lastId)
        {
            var map = NewsMapBuilder<AddressMail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_GetAddressAllMailUserBuyCoin", map);
            return query.Execute(top, lastId).ToList();
        }
        public int Tool_UpdateLastIdOrFinish(int id, int? lastId, bool? isActive)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_UpdateLastIdOrFinish", map);

            return query.Execute(id, lastId, isActive).FirstOrDefault();
        }

        public string User_Tron_Create(TronCoin tron)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("User_Tron_Create", map).Execute(tron.UserId,
                tron.Key,
                tron.Address,
                tron.Balance).FirstOrDefault();
        }

        public List<UserWallet> Tool_Get_New_User_Not_wallet()
        {
            var map = NewsMapBuilder<UserWallet>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_New_User_Not_wallet", map);

            return query.Execute().ToList();
        }

        public User_WalletAddress User_WalletAddress_GetByUserId(int userId)
        {
            var map = NewsMapBuilder<User_WalletAddress>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_WalletAddress_GetByUserId", map);
            return query.Execute(userId).FirstOrDefault();
        }

        public int User_WalletAddress_Update(int userId, string btc, string eth)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_WalletAddress_Update", map);
            return query.Execute(userId, btc, eth, null, null).FirstOrDefault();
        }

        public int Tool_Lock_Account(int id)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Lock_Account", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int Mail_UserMining_Update(int id)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Mail_UserMining_Update", map);

            return query.Execute(id).FirstOrDefault();
        }
        public List<AddressMail> Users_List_Mail_Marketing(int top, int lastId)
        {
            var map = NewsMapBuilder<AddressMail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Users_List_Mail_Marketing", map);
            return query.Execute(top, lastId).ToList();
        }
        #endregion

        #region packages
        public string Setting_GetValueByName(string name)
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("Setting_GetValueByName", map);
            return query.Execute(name).FirstOrDefault();
        }
        public string Package_BonusOnDayGet(DateTime day)
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("Package_BonusOnDayGet", map);
            return query.Execute(day).FirstOrDefault();
        }
        public List<Packages_Bonus> Packages_Bonus_UserList(DateTime day, int top = 100)
        {
            var map = NewsMapBuilder<Packages_Bonus>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Packages_Bonus_UserList", map);
            return query.Execute(top, day).ToList();
        }
        public List<UserInfo> Tool_Get_All_Package_Reinvestment()
        {
            var map = NewsMapBuilder<UserInfo>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_All_Package_Reinvestment", map);
            return query.Execute().ToList();
        }
        public int Packages_Bonus_Transaction_Insert(int fromId, int userId, DateTime date, int status, int packagesId, decimal totalBonus, decimal bonus73, decimal bonus20, decimal bonus7)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Packages_Bonus_Transaction_Insert", map);
            return query.Execute(fromId,
               userId,
               date,
               status,
               packagesId,
               totalBonus,
               bonus73,
               bonus20,
               bonus7).FirstOrDefault();
        }
        public int Mail_UserMining_Insert(Mail_UserMining userMail)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Mail_UserMining_Insert", map);
            return query.Execute(userMail.UserId,
                userMail.Bonus,
               userMail.CreateOn,
               userMail.NextTimeOn,
               userMail.Status,
               userMail.IsFinish).FirstOrDefault();
        }
        public List<Packages_Bonus> Tool_Get_Packeges_Bonus()
        {
            var map = NewsMapBuilder<Packages_Bonus>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_Packeges_Bonus", map);
            return query.Execute().ToList();
        }

        public List<Packages_Bonus> Tool_Get_Packeges_Stock()
        {
            var map = NewsMapBuilder<Packages_Bonus>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_Packeges_Stock", map);
            return query.Execute().ToList();
        }

        public int Tool_Packages_Bonus_Transaction_Insert(Packages_Bonus_Transaction model)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Packages_Bonus_Transaction_Insert", map);
            return query.Execute(model.UserId,
               model.Bonus,
               model.PercentAmount,
               model.PackagesId,
               model.CreateDate,
               model.Type,
               model.TotalBonus,
               model.MaxBonusOnMonth).FirstOrDefault();
        }
        #endregion

        #region tron coin
        public List<TronCoin> Tron_Get_all_Address(int lastestId)
        {
            var map = NewsMapBuilder<TronCoin>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tron_Get_all_Address", map);
            return query.Execute(lastestId).ToList();
        }

        public int User_Tron_Update_Tool(TronCoin tron)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Tron_Update_Tool", map);
            return query.Execute(tron.Id,
                tron.UserId,
                tron.Address,
                tron.Balance).FirstOrDefault();
        }
        #endregion

        #region branch balance
        public List<User_Branch_Balance> User_Branch_Balance_GetALL()
        {
            var map = NewsMapBuilder<User_Branch_Balance>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_GetALL", map);
            return query.Execute().ToList();
        }

        public List<User_Branch_Balance> User_Branch_Balance_Completed(string uids)
        {
            var map = NewsMapBuilder<User_Branch_Balance>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_Completed", map);
            return query.Execute(uids).ToList();
        }

        public List<User_Branch_Balance> User_Branch_Balance_GetBonus()
        {
            var map = NewsMapBuilder<User_Branch_Balance>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_GetBonus", map);
            return query.Execute().ToList();
        }

        public List<User_Branch_Balance> User_Branch_Balance_VOL(DateTime begin, DateTime end, int lastId)
        {
            var map = NewsMapBuilder<User_Branch_Balance>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_VOL", map);
            return query.Execute(begin, end, lastId).ToList();
        }

        public int User_Branch_Balance_VOL_Update(int uid, decimal bonus, int type, string desc, decimal efs)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_VOL_Update", map);
            return query.Execute(uid, bonus, type, desc, efs).FirstOrDefault();
        }

        public List<HighchartSyncTrade> Tool_Bonus_Sale(DateTime begin, DateTime end, int lastId)
        {
            var map = NewsMapBuilder<HighchartSyncTrade>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Bonus_Sale", map);
            return query.Execute(begin, end, lastId).ToList();
        }

        public List<int> Tool_Get_UserId_Level_Five()
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Get_UserId_Level_Five", map);
            return query.Execute().ToList();
        }

        public int Tool_Bonus_Sale_Insert(int userId, int rankId, decimal percent, decimal bonusETH)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Bonus_Sale_Insert", map);
            return query.Execute(userId, rankId, percent, bonusETH).FirstOrDefault();
        }

        public decimal Tool_Get_Total_Exchange()
        {
            var map = new DecimalResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Get_Total_Exchange", map);
            return query.Execute().FirstOrDefault();
        }

        public List<UserParent> Tool_Bonus_sale_Get_Parrent_Data(string uids)
        {
            var map = NewsMapBuilder<UserParent>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Bonus_sale_Get_Parrent_Data", map);
            return query.Execute(uids).ToList();
        }

        public int Tool_Bonus_Sale_Update_Level(int userId, int level)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Tool_Bonus_Sale_Update_Level", map);
            return query.Execute(userId, level).FirstOrDefault();
        }

        public int User_Branch_Balance_Update(User_Branch_Balance model)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_Update", map);
            return query.Execute(model.Id,
                model.LeftReset,
                model.RightReset,
                model.Status,
                model.Bonus).FirstOrDefault();
        }

        public int User_Branch_Balance_Update_Bonus_F(int uid, decimal bonus)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_Update_Bonus_F", map);
            return query.Execute(uid, bonus).FirstOrDefault();
        }

        public int User_Branch_Balance_Get_Is_F(int uid)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("User_Branch_Balance_Get_Is_F", map);
            return query.Execute(uid).FirstOrDefault();
        }

        public int Bonus_branch_Update(int id, int uid, int byUid, int status, decimal amount, decimal amountFBT)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Bonus_branch_Update", map);
            return query.Execute(id,
                uid,
                byUid,
                status,
                amount,
                amountFBT).FirstOrDefault();
        }

        public int Update_BNCT(int uid, decimal coin30, int fromUid, int packageId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Update_BNCT", map).Execute(uid,
                coin30,
                fromUid,
                packageId).FirstOrDefault();
        }

        public List<int> Get_All_User_on_Tree_Not_Invest()
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Get_All_User_on_Tree_Not_Invest", map);
            return query.Execute().ToList();
        }

        public void Lock_All_User_on_Tree_Not_Invest(string uids)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Lock_All_User_on_Tree_Not_Invest", map);
            query.Execute(uids).FirstOrDefault();
        }
        #endregion

        public DateTime Get_Server_Time()
        {
            var map = new StringResultSetMapper();
            var query = _db.CreateSprocAccessor("Get_Server_Time", map);
            return DateTime.Parse(query.Execute().FirstOrDefault());
        }

        public TickerPriceChange Get_TickerPriceChange()
        {
            var map = NewsMapBuilder<TickerPriceChange>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_TickerPriceChange", map);
            return query.Execute().FirstOrDefault();
        }

        public List<HighchartSyncTrade> HighchartSync_OrderMatching_v2(DateTime date, int GetTop)
        {
            var map = NewsMapBuilder<HighchartSyncTrade>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("HighchartSync_OrderMatching_v2", map);
            return query.Execute(date, GetTop).ToList();
        }

        public List<HighchartSyncTrade> HighchartSync_OrderMatching(DateTime date, int GetTop)
        {
            var map = NewsMapBuilder<HighchartSyncTrade>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("HighchartSync_OrderMatching", map);
            return query.Execute(date, GetTop).ToList();
        }

        public int HighchartSync_OrderMatching_Update(HighchartSyncTrade info)
        {
            var map = new IntegerResultSetMapper();
            //var query = _db.CreateSprocAccessor("HighchartSync_OrderMatching_Update", map);
            var query = _db.CreateSprocAccessor("HighchartSync_OrderMatching_Update_New", map);
            return query.Execute(info.Id, info.UserId, info.Status, info.Price, info.Profit, info.Amount, info.IsDemo, info.ByType, info.BeginAmount).FirstOrDefault();
        }

        public List<CoinPriceSync> HighchartSync_GetTempData()
        {
            var map = NewsMapBuilder<CoinPriceSync>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("HighchartSync_GetTempData", map);
            return query.Execute().ToList();
        }

        public List<User_Vol> Tool_Get_Vol_By_Uid(int lastId, int top)
        {
            var map = NewsMapBuilder<User_Vol>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_Vol_By_Uid", map);
            return query.Execute(lastId, top).ToList();
        }

        public List<SyncDataTab> Sync_Get_Data(UInt64 lastId, int top)
        {
            var map = NewsMapBuilder<SyncDataTab>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Sync_Get_Data", map);
            return query.Execute(lastId, top).ToList();
        }

        public List<Parents> MUser_GetParentByUserId(int userId)
        {
            var map = NewsMapBuilder<Parents>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("MUser_GetParentByUserId", map);

            return query.Execute(userId, 3).ToList();
        }

        public int User_Branch_Balance_Insert(Sync_User_Branch_Balance branch)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Branch_Balance_Insert", map).Execute(
                branch.UserId,
                branch.LeftAmount,
                branch.RightAmount,
                branch.LeftReset,
                branch.RightReset,
                branch.Status,
                branch.CreateDate,
                branch.ByUid,
                branch.PackageId,
                branch.MaxInvest
                ).FirstOrDefault();
        }

        public int Sync_Remove(string uids)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Sync_Remove", map);
            return query.Execute(uids).FirstOrDefault();
        }

        public int RobotTrade_Remoce()
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("RobotTrade_Remoce", map);
            return query.Execute().FirstOrDefault();
        }

        public int GetReferralIdByUserId(int uid)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("GetReferralIdByUserId", map);
            return query.Execute(uid).FirstOrDefault();
        }

        public int HighchartSync_OrderMatching_Update_BonusF(int uid, decimal bonus, int byuid)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("HighchartSync_OrderMatching_Update_BonusF", map);
            return query.Execute(uid, bonus, byuid).FirstOrDefault();
        }

        public int HighchartSync_InsertData(string querySQL)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("HighchartSync_InsertData", map);
            return query.Execute(querySQL).FirstOrDefault();
        }

        public int HighchartSync_RemoveData(DateTime date)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("HighchartSync_RemoveData", map);
            return query.Execute(date).FirstOrDefault();
        }

        //public List<PricesModel> Get_SynAarbitrage()
        //{
        //    var map = NewsMapBuilder<PricesModel>.BuildAllProperties();
        //    var query = _db.CreateSprocAccessor("Get_SynAarbitrage", map);
        //    return query.Execute().ToList();
        //}

        //public int Tool_SynAarbitrage_Update(PricesModel model)
        //{
        //    var map = new IntegerResultSetMapper();
        //    var query = _db.CreateSprocAccessor("Tool_SynAarbitrage_Update", map);
        //    return query.Execute(model.id,
        //        model.color_price,
        //        model.prices,
        //        model.bit_x,
        //        model.bitfinex,
        //        model.bitmex,
        //        model.bitstamp,
        //        model.cex_io,
        //        model.coinbase,
        //        model.exmo,
        //        model.gemini,
        //        model.itbit,
        //        model.kraken,
        //        model.wallofcoins
        //    ).FirstOrDefault();
        //}

        #region Withdraw
        public List<WithdrawExpire> WithdrawExpires()
        {
            var map = NewsMapBuilder<WithdrawExpire>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Withdraw_List_Expire_UnconfirmedEmail", map);
            return query.Execute().ToList();
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
        #endregion

        //public MarketsExchange ExchangeMarkets()
        //{
        //    try
        //    {
        //        string urlApi = "https://api.cryptowat.ch/markets";
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
        //                                           SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
        //        var client = new WebClient();
        //        //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
        //        var result = new CustomJsonResult();
        //        result.Result = client.DownloadString(urlApi);
        //        //var obj = JArray.Parse(resultToken);
        //        MarketsExchange markets = new MarketsExchange();
        //        if (result != null && result.Result != null)
        //        {
        //            markets = JsonConvert.DeserializeObject<MarketsExchange>(result.Result.ToString());
        //        }
        //        return markets;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MarketsExchange();
        //    }
        //}

        //public CoinPairPrice ExchangeGetPriceCoin(string exchangename, string pair)
        //{
        //    try
        //    {
        //        string urlApi = string.Format("https://api.cryptowat.ch/markets/{0}/{1}/price", exchangename, pair);
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
        //                                           SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
        //        var client = new WebClient();
        //        //client.Headers.Add("X-CMC_PRO_API_KEY", "3015747c-c5ab-4b32-8f9e-c92599a6f772");
        //        var result = new CustomJsonResult();
        //        result.Result = client.DownloadString(urlApi);
        //        //var obj = JArray.Parse(resultToken);
        //        CoinPairPrice markets = new CoinPairPrice();
        //        if (result != null && result.Result != null)
        //        {
        //            markets = JsonConvert.DeserializeObject<CoinPairPrice>(result.Result.ToString());
        //        }

        //        return markets;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CoinPairPrice();
        //    }
        //}

        public int ArbittrageTransaction_Ins(TradeHistoryTransaction model)
        {
            try
            {
                var map = new IntegerResultSetMapper();
                return _db.CreateSprocAccessor("ArbittrageTransaction_Ins", map).Execute(model.BuyExchange, model.SellExchange
                    , model.BuyPrice, model.SellPrice, model.PercentDifference, model.CoinPair, model.TradeAt, model.TransactionID).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                return -1;
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
                string urlApi = @"https://api.coinbase.com/v2/prices/ETH-USD/spot";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

                var client = new WebClient();
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

        public decimal Convert_USD_To_ETH(decimal usd, decimal oneEth)
        {
            try
            {
                return Math.Round(usd / oneEth, 8);
            }
            catch
            {
                return 0;
            }
        }
        
        public int? KLinesCandlestick_GetMaxTime(string pair)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("KLinesCandlestick_GetMaxTime", map).Execute(pair).FirstOrDefault();
        }

        public List<ViewModelUsers> Tool_Get_User_Not_Register_In_Copytrade()
        {
            var map = NewsMapBuilder<ViewModelUsers>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tool_Get_User_Not_Register_In_Copytrade", map);

            return query.Execute().ToList();
        }

        #region Random Price
        public int Random_Orders_WinLose_Reset()
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("Random_Orders_WinLose_Reset", map);
            return query.Execute().FirstOrDefault();
        }
        public Random_Orders_WinLose Random_Orders_WinLose_Get(string pairname)
        {
            var map = NewsMapBuilder<Random_Orders_WinLose>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Random_Orders_WinLose_Get", map);
            return query.Execute(pairname).FirstOrDefault();
        }
        #endregion
        #region Candlestick Data
        public void KLinesCandlestick_Update(KlineCandlesticks candles)
        {
            var map = new IntegerResultSetMapper();
            _db.CreateSprocAccessor("CandlestickData_UpdatePriceClose", map).Execute(
               candles.Open,
               candles.High,
               candles.Low,
               candles.VolumeFrom,
               candles.VolumeTo,
               candles.Close,
               candles.ConversionType,
               candles.ConversionSymbol,
               candles.PairName,
               candles.TimeClose,
               candles.TimeOpen,
               candles.IntervalValue,
               candles.PriceChangePercent).FirstOrDefault();
        }
        public KlineCandlesticks Candlestick_GetBy_Pair_LastTime(string pair)
        {
            var map = NewsMapBuilder<KlineCandlesticks>.BuildAllProperties();
            return _db.CreateSprocAccessor("Candlestick_GetBy_Pair_LastTime", map).Execute(pair).FirstOrDefault();
        }
        #endregion
    }
}