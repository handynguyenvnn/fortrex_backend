using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TradeHistoryTransaction
    {
        public decimal Id { get; set; }
        public string BuyExchange { get; set; }
        public string SellExchange { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? PercentDifference { get; set; }
        public string CoinName { get; set; }
        public DateTime? TradeAt { get; set; }
        public string CoinPair { get; set; }
        public string TransactionId { get; set; }
    }
}
