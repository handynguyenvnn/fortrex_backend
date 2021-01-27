using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Lib.Cache;
using System.Web.Script.Serialization;
using System.Collections;
using System.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Lib.Data.Repository.Packages;
using Lib.Domain.Packages;
using RestSharp;
using Lib.Domain.Packages.Trades;
using Lib.Domain.User;
using Lib.Domain.AsynTabs;

namespace Lib.Service.Service.Packages
{
    public interface IPackagesService
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
        Promocode PromocodeGet(DateTime date, int status, int? userId = null);
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
        List<AutoReceiredHash> Auto_Receired();
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
        int Random_Orders_WinLose_Update(string Pairname, bool isTypeRandom, bool isActive);
        List<AffiliateTradingList> Get_AffiliateTradingHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId);
        List<AffiliateTradingList> Get_AffiliateAgencyHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId);
        List<AffiliateMember> Get_AffiliateChartMembers(int userId);
        List<AffiliateAgencyCom> Get_AffiliateChartAgencyCom(int userId, int option);
        List<int> TradingLastResults(string pair); 
    }

    public class PackagesService : IPackagesService
    {
        private string userHost = "https://api.tronscan.org/api";
        private readonly IPackagesRepository _packagesRepository;
        public PackagesService(IPackagesRepository packagesRepository)
        {
            _packagesRepository = packagesRepository;
        }

        public List<AffiliateAgencyCom> Get_AffiliateChartAgencyCom(int userId, int option)
        {
            return _packagesRepository.Get_AffiliateChartAgencyCom(userId, option);
        }

        public List<AffiliateMember> Get_AffiliateChartMembers(int userId)
        {
            return _packagesRepository.Get_AffiliateChartMembers(userId);
        }

        public List<AffiliateTradingList> Get_AffiliateAgencyHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId)
        {
            return _packagesRepository.Get_AffiliateAgencyHistory(pageIndex, pageSize, out total, whereClause, userId);
        }

        public List<AffiliateTradingList> Get_AffiliateTradingHistory(int pageIndex, int pageSize, out int total, string whereClause, int userId)
        {
            return _packagesRepository.Get_AffiliateTradingHistory(pageIndex, pageSize, out total, whereClause, userId);
        }

        public int AsynTab_Insert(int uid, int type, int status, string extra_data, DateTime createOn)
        {
            return _packagesRepository.AsynTab_Insert(uid, type, status, extra_data, createOn);
        }

        public decimal Get_Total_Trade(int userId)
        {
            return _packagesRepository.Get_Total_Trade(userId);
        }

        public int HighchartSyncTrades_Ins(HighchartSyncTrade model)
        {
            return _packagesRepository.HighchartSyncTrades_Ins(model);
        }

        public HighchartPrice HighchartSyncGetPriceCoin(string coin)
        {
            return _packagesRepository.HighchartSyncGetPriceCoin(coin);
        }

        public int Update_BNCT(int uid, decimal coin30, int fromUid, int packageId)
        {
            return _packagesRepository.Update_BNCT(uid, coin30, fromUid, packageId);
        }

        public int SellStock_Create(int userId, decimal requestAmount, decimal responseAmount, decimal responseFee)
        {
            return _packagesRepository.SellStock_Create(userId, requestAmount, responseAmount, responseFee);
        }
        public decimal Get_Max_Package(int uid)
        {
            return _packagesRepository.Get_Max_Package(uid);
        }
        public List<ExpireList> Check_Reinventment_Expire(int userId)
        {
            return _packagesRepository.Check_Reinventment_Expire(userId);
        }
        public List<ExpireList> Get_List_Next_Reinventment(int userId)
        {
            return _packagesRepository.Get_List_Next_Reinventment(userId);
        }
        public bool Check_Reinventment(int userId)
        {
            return _packagesRepository.Check_Reinventment(userId);
        }
        public int Transfer_USD_To_Wallet(int userId, decimal amount, string wallet, string token, decimal responseAmount, decimal responseFee, string tokenAccess)
        {
            return _packagesRepository.Transfer_USD_To_Wallet(userId, amount, wallet, token, responseAmount, responseFee, tokenAccess);
        }
        public decimal Package_GetBonusFinish(int userId, int id)
        {
            return _packagesRepository.Package_GetBonusFinish(userId, id);
        }
        public int T_TreeData_GetTotalUserByParent(int parentId)
        {
            return _packagesRepository.T_TreeData_GetTotalUserByParent(parentId);
        }
        public List<AutoReceiredHash> Auto_Receired()
        {
            var restClient = new RestClient(userHost + "/transaction");
            var request = new RestRequest(Method.GET);
            var respone = restClient.Execute(request);
            List<AutoReceiredHash> responseData = new List<AutoReceiredHash>();
            if (respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = respone.Content;
                try
                {
                    var tronCoins = JsonConvert.DeserializeObject<AutoResponse>(respone.Content.Trim());
                    int num = 1;
                    foreach(AutoReceired tronCoin in tronCoins.data)
                    {
                        try
                        {
                            decimal amount = tronCoin.contractData.amount / 1000000;
                            if(amount > 10 && amount < 5000)
                            {
                                if (responseData.Exists(x=>x.To == tronCoin.contractData.to))
                                {
                                    continue;
                                }
                                if (responseData.Exists(x => x.From == tronCoin.contractData.from))
                                {
                                    continue;
                                }

                                AutoReceiredHash hash = new AutoReceiredHash
                                {
                                    Hash = tronCoin.hash,
                                    Amount = amount,
                                    CreateOn = TimeStampToDateTime(tronCoin.timestamp / 1000).ToString("yyyy-MM-dd HH:mm"),
                                    Status = "completed",
                                    To = tronCoin.contractData.to,
                                    From = tronCoin.contractData.from
                                };
                                responseData.Add(hash);
                                num++;
                                if(num > 15)
                                {
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                catch
                {

                }
            }
            return responseData;
        }

        private DateTime TimeStampToDateTime(double timestamp)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);
            return dateTime;
        }

        public int Packages_GetPackegesIdByPrice(decimal price, int status)
        {
            return _packagesRepository.Packages_GetPackegesIdByPrice(price, status);
        }
        public int Packages_User_Maping_Insert(int userId, int packagesId, decimal coin, decimal usd, DateTime createOn, string tranc, int status)
        {
            return _packagesRepository.Packages_User_Maping_Insert(userId, packagesId, coin, usd, createOn, tranc, status);
        }
        public List<PackagesEntity> Packages_GetDetail()
        {
            //var cacheKey = CacheKeyManager.SettingValueCache.Cache__GetPackages;
            //return CacheExtensions.Get(cacheKey, TimeSpan.FromMinutes(30), () =>
            //{
            //    return _packagesRepository.Packages_GetDetail();
            //});
            return _packagesRepository.Packages_GetDetail();
        }
        public int Packages_Bonus_Insert(Packeges_Bonus detail)
        {
            return _packagesRepository.Packages_Bonus_Insert(detail);
        }
        public int Packages_Buy_MasterIB(int uid, decimal amount, decimal percent, int type)
        {
            return _packagesRepository.Packages_Buy_MasterIB(uid, amount, percent, type);
        }
        public int Packages_User_Maping_TotalCoin(int status)
        {
            return _packagesRepository.Packages_User_Maping_TotalCoin(status);
        }
        public List<InvestmentList> Investment_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _packagesRepository.Investment_List(pageIndex, pageSize, out total, whereClause);
        }
        public string Package_BonusOnDayGet(DateTime day)
        {
            return _packagesRepository.Package_BonusOnDayGet(day);
        }
        public List<Packeges_Bonus> Packeges_Bonus_GetByUserId(int userId, int status)
        {
            return _packagesRepository.Packeges_Bonus_GetByUserId(userId, status);
        }
        public List<Package_BonusOnDay> Package_BonusOnDay_Get(DateTime day)
        {
            return _packagesRepository.Package_BonusOnDay_Get(day);
        }
        public DasboarchDetail Dasboarch_Detail(int userId)
        {
            return _packagesRepository.Dasboarch_Detail(userId);
        }
        public DasboarchDetail Dasboarch(int userId)
        {
            return _packagesRepository.Dasboarch(userId);
        }
        public int Packages_BonusF(int fromId, int userId, decimal bonus, int level, int type, decimal maxout)
        {
            return _packagesRepository.Packages_BonusF(fromId, userId, bonus, level, type, maxout);
        }
        public int Packages_BonusOnBonusF(int fromId, int userId, decimal bonus, int level, int type)
        {
            return _packagesRepository.Packages_BonusOnBonusF(fromId, userId, bonus, level, type);
        }
        public List<Receive> LastReceivedGet(int top)
        {
            return _packagesRepository.LastReceivedGet(top);
        }
        public Promocode PromocodeGet(DateTime date, int status, int? userId = null)
        {
            return _packagesRepository.PromocodeGet(date, status, userId);
        }
        public int ReceivecPromocode_insert(ReceivecPromocode pro)
        {
            return _packagesRepository.ReceivecPromocode_insert(pro);
        }
        public List<Lib.Domain.Promocodes.Promocode> Promocode_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _packagesRepository.Promocode_List(pageIndex, pageSize, out total, whereClause);
        }
        public Lib.Domain.Promocodes.Promocode Promocode_GetById(int id)
        {
            return _packagesRepository.Promocode_GetById(id);
        }
        public int Promocode_InsertUpdate(Lib.Domain.Promocodes.Promocode data)
        {
            return _packagesRepository.Promocode_InsertUpdate(data);
        }
        public List<Lib.Domain.Promocodes.Promocode_User_Mapping> PromocodeItems_List(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _packagesRepository.PromocodeItems_List(pageIndex, pageSize, out total, whereClause);
        }
        public Lib.Domain.Promocodes.Promocode_User_Mapping PromocodeItems_GetById(int id)
        {
            return _packagesRepository.PromocodeItems_GetById(id);
        }
        public int PromocodeItems_InsertUpdate(Lib.Domain.Promocodes.Promocode_User_Mapping data)
        {
            return _packagesRepository.PromocodeItems_InsertUpdate(data);
        }
        public List<int> Promocode_User_Mapping_By_Promotion(int id)
        {
            return _packagesRepository.Promocode_User_Mapping_By_Promotion(id);
        }
        public int PromotionSendMail_Insert(Lib.Domain.Promocodes.PromotionSendMail data)
        {
            return _packagesRepository.PromotionSendMail_Insert(data);
        }
        public List<TickerPriceChange> TradePairs_Gets(string pair = "")
        {
            return _packagesRepository.TradePairs_Gets(pair);
        }
        public List<Candlesticks> Candlestick_GetBy_Pair(string pair = "", string interval="", int row = 1)
        {
            return _packagesRepository.Candlestick_GetBy_Pair(pair, interval, row);
        }
        public Candlesticks Candlestick_GetBy_Pair_LastTime(string pair = "")
        {
            return _packagesRepository.Candlestick_GetBy_Pair_LastTime(pair);
        }
        public List<User_PairName_Mapping> User_PairName_Mapping_Select(int userId)
        {
            return _packagesRepository.User_PairName_Mapping_Select(userId);
        }

        public int PairName_Favorite_Del(int userId, string pairname)
        {
            return _packagesRepository.PairName_Favorite_Del(userId, pairname);
        }
        public int PairName_Favorite_Ins(int userId, string pairname)
        {
            return _packagesRepository.PairName_Favorite_Ins(userId, pairname);
        }

        public string Get_WalletAddressUSD_ByUser(int userId)
        {
            return _packagesRepository.Get_WalletAddressUSD_ByUser(userId);
        }


        public User_WalletAddress Get_WalletAddressInfo_ByUser(int userId)
        {
            return _packagesRepository.Get_WalletAddressInfo_ByUser(userId);
        }


        public int Transfer_USD_By_WalletAddress(int UserId_Transfer, decimal AmountUSD, string WalletReceived, string NoteText) {
            return _packagesRepository.Transfer_USD_By_WalletAddress(UserId_Transfer, AmountUSD, WalletReceived, NoteText);
        }

        public int Random_Orders_WinLose_Update(string Pairname, bool isTypeRandom, bool isActive)
        {
            return _packagesRepository.Random_Orders_WinLose_Update(Pairname, isTypeRandom, isActive);
        }
        public List<int> TradingLastResults(string pair) {
            return _packagesRepository.TradingLastResults(pair);
        }
    }
}