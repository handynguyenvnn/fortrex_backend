using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
    public class WithdrawResponse 
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }

        [DataMember(Order = 3)]
        public string Id { get; set; }
    }
}
