using System;
using System.Runtime.Serialization;



using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// Response object received when querying a Binance order
    /// </summary>
    [DataContract]
    public class OrderResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        public string ClientOrderId { get; set; }

        [DataMember(Order = 4)]
        public decimal Price { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "origQty")]
        public decimal OriginalQuantity { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQuantity { get; set; }

        [DataMember(Order = 11)]
        public decimal StopPrice { get; set; }

        [DataMember(Order = 12)]
        [JsonProperty(PropertyName = "icebergQty")]
        public decimal IcebergQuantity { get; set; }

        [DataMember(Order = 13)]
        
        public DateTime Time { get; set; }

        [DataMember(Order = 14)]
        public bool IsWorking { get; set; }

        [DataMember(Order = 15)]
        [JsonProperty(PropertyName = "cummulativeQuoteQty")]
        public decimal CummulativeQuoteQuantity { get; set; }
    }
}