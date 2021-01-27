using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Lib.Domain.Packages.Trades;
using Lib.Domain.TransactionHistorys;
using Lib.Domain.Withdraws;
using Lib.Domain.Packages;
using Lib.Domain.Transfers;
using Lib.Domain.Coins.Deposit;
using Lib.Domain.User;

namespace Web.SourceCoin.Models.Users
{
    public class PushOrderModel
    {
        public PushOrderModel()
        {
            IsDemo = false;
        }
        public string MarketName { get; set; }
        public decimal Amount { get; set; }
        public int IsCall { get; set; }
        public bool IsDemo { get; set; }
        public int Formatdecimal { get; set; }
        public int ByType { get; set; }
    }

    public class PagingModel
    {
        public PagingModel()
        {
            Type = -2;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 1: all order, 2 pending
        /// </summary>
        public int? Type { get; set; }
    }
    public class PagingWithdrawModel
    {
       
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class PagingResponse
    {
        public int Total { get; set; }
        //public string Item { get; set; }
        public List<ResponseTradings> Item { get; set; }
    }
    public class PagingResponseTradings
    {
        public int Total { get; set; }
        public List<ResponseTradings> Item { get; set; }
    }
    public class PagingResponseHistoryTransaction
    {
        public int Total { get; set; }
        public List<ResponseHistoryTransaction> Item { get; set; }
    }
    public class PagingResponseDepositHistorys
    {
        public int Total { get; set; }
        public List<DepositList> Item { get; set; }
    }
    public class WithdrawPagingResponse
    {
        public int Total { get; set; }
        public List<WithdrawList> Item { get; set; }
    }
    public class UsersAffiliatesPagingResponse
    {
        public int Total { get; set; }
        public List<UsersAffiliates> Item { get; set; }
    }
    
    public class InvestmentModel
    {
        public decimal Amount { get; set; }
    }
    public class InvestmentHistoryModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class InvestmentPagingResponse
    {
        public int Total { get; set; }
        public List<InvestmentList> Item { get; set; }
    }


    public class TransferHistoryPagingResponse
    {
        public int Total { get; set; }
        public List<TransferHistoryModel> Item { get; set; }
    }

    public class TradingPagingResponse
    {
        public int Total { get; set; }
        public List<AffiliateTradingList> Item { get; set; }
    }

    public class AgencyPagingResponse
    {
        public int Total { get; set; }
        public List<AffiliateTradingList> Item { get; set; }
    }

    public class ChartAgencyComModel
    {
        public int Option { get; set; }
    }
}