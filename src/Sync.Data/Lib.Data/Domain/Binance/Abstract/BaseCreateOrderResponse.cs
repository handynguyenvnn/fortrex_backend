using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Lib.Domain.Coins.Abstract
{
    [DataContract]
    public abstract class BaseCreateOrderResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        public string ClientOrderId { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty("transactTime")]
      
        public DateTime TransactionTime { get; set; }
    }
}
