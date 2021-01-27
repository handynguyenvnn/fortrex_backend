using System.Runtime.Serialization;


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class SystemStatusResponse
    {
        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }
    }
}