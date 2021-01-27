using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
  
    public class SymbolPriceChangeTickerResponse
    {
        public SymbolPriceChangeTickerResponse()
        {
            PriceChange = 0;
        }

        public decimal PriceChange { get; set; }
        public string symbol { get; set; }
        public string Fsym { get; set; }
        public string Tsym { get; set; }
        [JsonProperty(PropertyName = "PriceChangePercent")]
        public decimal PriceChangePercent { get; set; }

        [JsonProperty(PropertyName = "weightedAvgPrice")]
        public decimal WeightedAveragePercent { get; set; }

        [JsonProperty(PropertyName = "prevClosePrice")]
        public decimal PreviousClosePrice { get; set; }
       
        public decimal LastPrice { get; set; }

        public decimal BidPrice { get; set; }

        public decimal AskPrice { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public decimal Volume { get; set; }

        [JsonProperty(PropertyName = "firstId")]
        public long FirstTradeId { get; set; }

        [JsonProperty(PropertyName = "lastId")]
        public long LastId { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int TradeCount { get; set; }
    }
}
