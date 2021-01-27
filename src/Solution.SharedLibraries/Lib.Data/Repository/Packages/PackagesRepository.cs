using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Lib.Data.MapBuilder;
using Lib.Data.ResultSetMapper;
using Lib.Domain.Simples;
using System;
using Lib.Core.Data;
using Lib.Domain.Packages;
using Lib.Domain.Packages.Trades;
using Lib.Domain.User;
using Lib.Domain.AsynTabs;

namespace Lib.Data.Repository.Packages
{
    public interface IPackagesRepository
    {
        int Packages_GetPackegesIdByPrice(decimal price, int status);
        int Packages_User_Maping_Insert(int userId, int packagesId, decimal coin, decimal usd, DateTime createOn, string tranc, int status);
        List<PackagesEntity> Packages_GetDetail();
        int Packages_Bonus_Insert(Packeges_Bonus detail);
        int Packages_User_Maping_TotalCoin(int status);
        List<InvestmentList> Investment_List(int pageIndex, int pageSize, out int total, string whereClause);
        string Package_BonusOnDayGet(DateTime day);
        List<Packeges_Bonus> Packeges_Bonus_GetByUserId(int userId, int status);
        List<Package_BonusOnDay> Package_BonusOnDay_Get(DateTime day);
        DasboarchDetail Dasboarch_Detail(int userId);
        int Packages_BonusF(int fromId, int userId, decimal bonus, int level, int type, decimal maxout);
        int Packages_BonusOnBonusF(int fromId, int userId, decimal bonus, int level, int type);
        List<Receive> LastReceivedGet(int top);
        Promocode PromocodeGet(DateTime date, int status, int? userId);
        int ReceivecPromocode_insert(ReceivecPromocode pro);
        List<Lib.Domain.Promocodes.Promocode> Promocode_List(int pageIndex, int pageSize, out int total, string whereClause);
        Lib.Domain.Promocodes.Promocode Promocode_GetById(int id);
        int Promocode_InsertUpdate(Lib.Domain.Promocodes.Promocode data);
        List<Lib.Domain.Promocodes.Promocode_User_Mapping> PromocodeItems_List(int pageIndex, int pageSize, out int total, string whereClause);
        Lib.Domain.Promocodes.Promocode_User_Mapping PromocodeItems_GetById(int id);
        int PromocodeItems_InsertUpdate(Lib.Domain.Promocodes.Promocode_User_Mapping data);
        List<int> Promocode_User_Mapping_By_Promotion(int id);
        int PromotionSendMail_Insert(Lib.Domain.Promocodes.PromotionSendMail data);
        DasboarchDetail Dasboarch(int userId);
        int T_TreeData_GetTotalUserByParent(int parentId);
        decimal Package_GetBonusFinish(int userId, int id);
        int Transfer_USD_To_Wallet(int userId, decimal amount, string wallet, string token, decimal responseAmount, decimal responseFee, string tokenAccess);
        bool Check_Reinventment(int userId);
        List<ExpireList> Get_List_Next_Reinventment(int userId);
        List<ExpireList> Check_Reinventment_Expire(int userId);
        decimal Get_Max_Package(int uid);
        int SellStock_Create(int userId, decimal requestAMount, decimal responseAmount, decimal responseFee);
        int Update_BNCT(int uid, decimal coin30, int fromUid, int packageId);
        HighchartPrice HighchartSyncGetPriceCoin(string coin);
        int HighchartSyncTrades_Ins(HighchartSyncTrade model);
        List<TickerPriceChange> TradePairs_Gets(string pair = "");
        List<Candlesticks> Candlestick_GetBy_Pair(string pair = "", string interval="", int row = 1);
        Candlesticks Candlestick_GetBy_Pair_LastTime(string pair = "");
        List<User_PairName_Mapping> User_PairName_Mapping_Select(int userId);
        int PairName_Favorite_Del(int userId, string pairname);
        int PairName_Favorite_Ins(int userId, string pairname);
        decimal Get_Total_Trade(int userId);
        int AsynTab_Insert(int uid, int type, int status, string extra_data, DateTime createOn);

        string Get_WalletAddressUSD_ByUser(int userId);

        User_WalletAddress Get_WalletAddressInfo_ByUser(int userId);

        int Transfer_USD_By_WalletAddress(int UserId_Transfer, decimal AmountUSD, string WalletReceived, string NoteText);
        int Packages_Buy_MasterIB(int uid, decimal amount, decimal percent, int type);
        int Random_Orders_WinLose_Update(string Pairname, bool isTypeRandom,bool isActive);
        List<AffiliateTradingList> Get_AffiliateTradingHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId);
        List<AffiliateTradingList> Get_AffiliateAgencyHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId);
        List<AffiliateMember> Get_AffiliateChartMembers(int userId);
        List<AffiliateAgencyCom> Get_AffiliateChartAgencyCom(int userId, int option);
        List<int> TradingLastResults(string pair);
    }

    public class PackagesRepository : BaseRepository, IPackagesRepository
    {

        public List<AffiliateAgencyCom> Get_AffiliateChartAgencyCom(int userId, int option)
        {
            var map = NewsMapBuilder<AffiliateAgencyCom>.BuildAllProperties();
            return _db.CreateSprocAccessor("Get_AffiliateChartAgencyCom", map).Execute(userId).ToList();
        }

        public List<AffiliateMember> Get_AffiliateChartMembers(int userId)
        {
            var map = NewsMapBuilder<AffiliateMember>.BuildAllProperties();
            return _db.CreateSprocAccessor("Get_AffiliateChartMembers", map).Execute(userId).ToList();
        }


        public List<AffiliateTradingList> Get_AffiliateAgencyHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId)
        {
            var map = NewsMapBuilder<AffiliateTradingList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String),
                    _db.CreateParameter("UserId", userId, DbType.Int32)
            };
            var data = _db.Execute("Get_AffiliateAgencyHistory", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public List<AffiliateTradingList> Get_AffiliateTradingHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId)
        {
            var map = NewsMapBuilder<AffiliateTradingList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String),
                    _db.CreateParameter("UserId", userId, DbType.Int32)
            };
            var data = _db.Execute("Get_AffiliateTradingHistory", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }

        public int AsynTab_Insert(int uid, int type, int status, string extra_data, DateTime createOn)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("AsynTab_Insert", map).Execute(uid,
                type,
                status,
                extra_data,
                createOn).FirstOrDefault();
        }
        public decimal Get_Total_Trade(int userId)
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("Get_Total_Trade", map).Execute(userId).FirstOrDefault();
        }

        public int HighchartSyncTrades_Ins(HighchartSyncTrade model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("HighchartSyncTrades_Ins_New", map).Execute(model.UserId,
                model.MarketName,
                model.Amount,
                model.IsCall,
                model.ByType,
                model.CurrentPrice).FirstOrDefault();


            //return _db.CreateSprocAccessor("HighchartSyncTrades_Ins", map).Execute(model.UserId,
            //    model.MarketName,
            //    model.Amount,
            //    model.IsCall,
            //    model.IsDemo,
            //    model.CurrentPrice,
            //    model.ByType).FirstOrDefault();
        }

        public HighchartPrice HighchartSyncGetPriceCoin(string coin)
        {
            var map = NewsMapBuilder<HighchartPrice>.BuildAllProperties();
            return _db.CreateSprocAccessor("Candlestick_GetBy_Pair", map).Execute(coin).FirstOrDefault();
        }
        public List<TickerPriceChange> TradePairs_Gets(string pair="")
        {
            var map = NewsMapBuilder<TickerPriceChange>.BuildAllProperties();
            return _db.CreateSprocAccessor("TradePairs_Gets", map).Execute(pair).ToList();
        }
        public List<Candlesticks> Candlestick_GetBy_Pair(string pair = "", string interval="", int row=1)
        {
            var map = NewsMapBuilder<Candlesticks>.BuildAllProperties();
            return _db.CreateSprocAccessor("Candlestick_GetBy_Pair", map).Execute(pair, interval, row).ToList();
        }
        public Candlesticks Candlestick_GetBy_Pair_LastTime(string pair = "")
        {
            var map = NewsMapBuilder<Candlesticks>.BuildAllProperties();
            return _db.CreateSprocAccessor("Candlestick_GetBy_Pair_LastTime", map).Execute(pair).FirstOrDefault();
        }
        public int Update_BNCT(int uid, decimal coin30, int fromUid, int packageId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Update_GES", map).Execute(uid,
                coin30,
                fromUid,
                packageId).FirstOrDefault();
        }

        public int SellStock_Create(int userId, decimal requestAmount, decimal responseAmount, decimal responseFee)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("SellStock_Create", map).Execute(userId,
                requestAmount,
                responseFee,
                responseAmount).FirstOrDefault();
        }

        public decimal Get_Max_Package(int uid)
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("Get_Max_Package", map).Execute(uid).FirstOrDefault();
        }
        public List<ExpireList> Check_Reinventment_Expire(int userId)
        {
            var map = NewsMapBuilder<ExpireList>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Check_Reinventment_Expire", map);
            return query.Execute(userId).ToList();
        }
        public List<ExpireList> Get_List_Next_Reinventment(int userId)
        {
            var map = NewsMapBuilder<ExpireList>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_List_Next_Reinventment", map);
            return query.Execute(userId).ToList();
        }
        public bool Check_Reinventment(int userId)
        {
            var map = new BooleanResultSetMapper();
            return _db.CreateSprocAccessor("Check_Reinventment", map).Execute(userId).FirstOrDefault();
        }
        public int Transfer_USD_To_Wallet(int userId, decimal amount, string wallet, string token, decimal responseAmount, decimal responseFee, string tokenAccess)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Transfer_USD_To_Wallet", map).Execute(userId, amount, wallet, token, responseAmount, responseFee, tokenAccess).FirstOrDefault();
        }
        public decimal Package_GetBonusFinish(int userId, int id)
        {
            var map = new DecimalResultSetMapper();
            return _db.CreateSprocAccessor("Package_GetBonusFinish", map).Execute(userId, id).FirstOrDefault();
        }
        public int T_TreeData_GetTotalUserByParent(int parentId)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("T_TreeData_GetTotalUserByParent", map).Execute(parentId).FirstOrDefault();
        }
        public int Packages_GetPackegesIdByPrice(decimal price, int status)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_GetPackegesIdByPrice", map).Execute(price, status).FirstOrDefault();
        }
        public int Packages_User_Maping_Insert(int userId, int packagesId, decimal coin, decimal usd, DateTime createOn, string tranc, int status)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_User_Maping_Insert", map).Execute(userId,
                packagesId,
                coin,
                usd,
                createOn,
                tranc,
                status).FirstOrDefault();
        }
        public List<PackagesEntity> Packages_GetDetail()
        {
            var map = NewsMapBuilder<PackagesEntity>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Packages_GetDetail", map);
            return query.Execute().ToList();
        }

        public int Packages_Bonus_Insert(Packeges_Bonus detail)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_Bonus_Insert", map).Execute(
                detail.UserId,
                detail.Invested,
                detail.IsProfit,
                detail.SharePercent,
                detail.SharePrice,
                detail.ShareTotal,
                detail.CreateOn,
                detail.StartProfitDate,
                detail.Type,
                detail.StockAmount,
                detail.ExpireDate).FirstOrDefault();
        }
        public int Packages_Buy_MasterIB(int uid, decimal amount, decimal percent, int type)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_Buy_MasterIB", map).Execute(
                uid,
                amount,
                percent,
                type).FirstOrDefault();
        }
        public int Packages_User_Maping_TotalCoin(int status)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_User_Maping_TotalCoin", map).Execute(status).FirstOrDefault();
        }
        public List<InvestmentList> Investment_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<InvestmentList>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Investment_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public string Package_BonusOnDayGet(DateTime day)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("Package_BonusOnDayGet", map).Execute(day).FirstOrDefault();
        }
        public List<Packeges_Bonus> Packeges_Bonus_GetByUserId(int userId, int status)
        {
            var map = NewsMapBuilder<Packeges_Bonus>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Packeges_Bonus_GetByUserId", map);
            return query.Execute(userId, status).ToList();
        }
        public List<Package_BonusOnDay> Package_BonusOnDay_Get(DateTime day)
        {
            var map = NewsMapBuilder<Package_BonusOnDay>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Package_BonusOnDay_Get", map);
            return query.Execute(day).ToList();
        }
        public DasboarchDetail Dasboarch_Detail(int userId)
        {
            var map = NewsMapBuilder<DasboarchDetail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Dasboarch_Detail", map);
            return query.Execute(userId).FirstOrDefault();
        }
        public DasboarchDetail Dasboarch(int userId)
        {
            var map = NewsMapBuilder<DasboarchDetail>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Dasboarch", map);
            return query.Execute(userId).FirstOrDefault();
        }
        public int Packages_BonusF(int fromId, int userId, decimal bonus, int level, int type, decimal maxout)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_BonusF_Insert", map).Execute(fromId, userId, bonus, level, type, maxout).FirstOrDefault();
        }
        public int Packages_BonusOnBonusF(int fromId, int userId, decimal bonus, int level, int type)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Packages_BonusOnBonusF_Insert", map).Execute(fromId, userId, bonus, level, type).FirstOrDefault();
        }
        public List<Receive> LastReceivedGet(int top)
        {
            var map = NewsMapBuilder<Receive>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("LastReceivedGet", map);
            return query.Execute(top).ToList();
        }
        public Promocode PromocodeGet(DateTime date, int status, int? userId)
        {
            var map = NewsMapBuilder<Promocode>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("PromocodeGet", map);
            return query.Execute(date, status, userId).FirstOrDefault();
        }
        public int ReceivecPromocode_insert(ReceivecPromocode pro)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("ReceivecPromocode_insert", map).Execute(
                pro.Received, pro.DayOn, pro.IsFinish, pro.Status, pro.Money, pro.UserId).FirstOrDefault();
        }
        public List<Lib.Domain.Promocodes.Promocode> Promocode_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<Lib.Domain.Promocodes.Promocode>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Promocode_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public Lib.Domain.Promocodes.Promocode Promocode_GetById(int id)
        {
            var map = NewsMapBuilder<Lib.Domain.Promocodes.Promocode>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Promocode_GetById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int Promocode_InsertUpdate(Lib.Domain.Promocodes.Promocode data)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Promocode_InsertUpdate", map).Execute(
                data.Id,
                data.Percent,
                data.FromDate,
                data.EndDate,
                data.Status,
                data.Code,
                data.MinValueBtc,
                data.MinValueEth,
                data.TotalDays,
                data.TotalReceivedBtc,
                data.TotalReceivedEth
                ).FirstOrDefault();
        }
        public List<Lib.Domain.Promocodes.Promocode_User_Mapping> PromocodeItems_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<Lib.Domain.Promocodes.Promocode_User_Mapping>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("PromocodeItems_List", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public Lib.Domain.Promocodes.Promocode_User_Mapping PromocodeItems_GetById(int id)
        {
            var map = NewsMapBuilder<Lib.Domain.Promocodes.Promocode_User_Mapping>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("PromocodeItems_GetById", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int PromocodeItems_InsertUpdate(Lib.Domain.Promocodes.Promocode_User_Mapping data)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("PromocodeItems_InsertUpdate", map).Execute(
                data.Id,
                data.UserId,
                data.PromocodeId
                ).FirstOrDefault();
        }
        public List<int> Promocode_User_Mapping_By_Promotion(int id)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Promocode_User_Mapping_By_Promotion", map).Execute(id).ToList();
        }
        public int PromotionSendMail_Insert(Lib.Domain.Promocodes.PromotionSendMail data)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("PromotionSendMail_Insert", map).Execute(data.PromotionId, data.UserId, data.IsActive).FirstOrDefault();
        }

        public List<User_PairName_Mapping> User_PairName_Mapping_Select(int userId)
        {
            var map = NewsMapBuilder<User_PairName_Mapping>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("userId", userId, DbType.Int32)
            };
            var data = _db.Execute("User_PairName_Mapping_Select", map, parameters).ToList();
            return data;  
        }
       
        public int PairName_Favorite_Del(int userId, string pairname)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_PairName_Mapping_Delete", map).Execute(userId, pairname).FirstOrDefault();
        }

        public int PairName_Favorite_Ins(int userId, string pairname)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_PairName_Mapping_Insert", map).Execute(userId, pairname).FirstOrDefault();
        }

        public string Get_WalletAddressUSD_ByUser(int userId)
        {
            var map = new StringResultSetMapper();
            return _db.CreateSprocAccessor("Get_WalletAddressUSD_ByUser", map).Execute(userId).FirstOrDefault();
        }

        public User_WalletAddress Get_WalletAddressInfo_ByUser(int userId)
        {
            var map = NewsMapBuilder<User_WalletAddress>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Get_WalletAddressInfo_ByUser", map);
            return query.Execute(userId).FirstOrDefault();
        }

        public int Transfer_USD_By_WalletAddress(int UserId_Transfer, decimal AmountUSD, string WalletReceived, string NoteText)
        {
               var map = new IntegerResultSetMapper();
               return _db.CreateSprocAccessor("Transfer_USD_By_WalletAddress", map).Execute(
               UserId_Transfer, WalletReceived,
               AmountUSD,
               NoteText).FirstOrDefault();
        }

        public int Random_Orders_WinLose_Update(string Pairname, bool isTypeRandom, bool isActive)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Random_Orders_WinLose_Update", map).Execute(
            Pairname, isTypeRandom,isActive).FirstOrDefault();
        }
        public List<int> TradingLastResults(string pair)
        {
            var map = NewsMapBuilder<int>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("TradingLastResults", map);
            return query.Execute(pair).ToList();
        }

    }
}