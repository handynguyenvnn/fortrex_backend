using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins
{
    public class MarketsExchange
    {
        public List<Arbittrage> result { get; set; }
    }
    public class Arbittrage
    {
        public decimal id { get; set; }
        public string exchange { get; set; }
        public string pair { get; set; }
        public bool active { get; set; }
        public string route { get; set; }

    }
    public class ArbittrageCoinPrice
    {
        public decimal price { get; set; }
    }
    public class CoinPairPrice
    {
        public ArbittrageCoinPrice result { get; set; }
    }
    public class TradeHistoryTransaction
    {
        public decimal id { get; set; }
        public string BuyExchange { get; set; }
        public string SellExchange { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal PercentDifference { get; set; }
        public string CoinName { get; set; }
        public DateTime TradeAt { get; set; }
        public string CoinPair { get; set; }
        public string TransactionID { get; set; }
    }
    public class ArbittrageTransaction_Lst
    {
        public decimal id { get; set; }
        public string BuyExchange { get; set; }
        public string SellExchange { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal PercentDifference { get; set; }
        public string CoinName { get; set; }
        public string TradeAt { get; set; }
        public string CoinPair { get; set; }
        public string TransactionID { get; set; }
    }
}
