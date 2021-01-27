using System.Runtime.Serialization;

using Newtonsoft.Json;
using System;



namespace Lib.Domain.Coins
{

    [DataContract]
    public class DepositListItem
    {
        [DataMember(Order = 1)]

        public DateTime InsertTime { get; set; }

        [DataMember(Order = 2)]
        public decimal Amount { get; set; }

        [DataMember(Order = 3)]
        public string Symbol { get; set; }

        [DataMember(Order = 4)]
        public string Address { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "txId")]
        public string TransactionId { get; set; }

    }
}