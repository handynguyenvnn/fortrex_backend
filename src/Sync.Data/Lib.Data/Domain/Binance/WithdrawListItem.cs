using System;
using System.Runtime.Serialization;



using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class WithdrawListItem
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public decimal Amount { get; set; }

        [DataMember(Order = 3)]
        public string Address { get; set; }

        [DataMember(Order = 4)]
        public string AddressTag { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "txId")]
        public string TransactionId { get; set; }

        [DataMember(Order = 6)]
        public string Asset { get; set; }

        [DataMember(Order = 7)]
        
        public DateTime ApplyTime { get; set; }

    }
}