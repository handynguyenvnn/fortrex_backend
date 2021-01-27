using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Domain.Simples;

namespace Lib.Data.Domain.Trade
{
    public class HighchartSyncTrade
    {
        public HighchartSyncTrade()
        {
            ByType = (int)InvestByType.USD;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MarketName { get; set; }
        public decimal BeginAmount { get; set; }
        public decimal EndAmount { get; set; }
        public decimal Amount { get; set; }
        public bool IsCall { get; set; }
        public bool IsDelete { get; set; }
        public decimal Profit { get; set; }
        public int Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime WaitingOn { get; set; }
        public DateTime CompleteOn { get; set; }
        public decimal Price { get; set; }
        public bool IsDemo { get; set; }
        public int ByType { get; set; }
    }

    public class UserParent
    {
        public int Id { get; set; }
        public int UserLevel { get; set; }
        public int TotalF1 { get; set; }
        public decimal TotalTree { get; set; }
        public decimal TotalDeposit { get; set; }
    }

    public class TickerPriceChange
    {
        public string PairName { get; set; }
        public decimal LastPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public int TradeWinPercent { get; set; }
        public int CandlestickCloseType { get; set; }
    }

}
