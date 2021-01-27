using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// Response following a call to the Cancel Order endpoint
    /// </summary>
    [DataContract]
    public class CancelOrderResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "origClientOrderId")]
        public string OriginalClientOrderId { get; set; }

        [DataMember(Order = 4)]
        public string ClientOrderId { get; set; }

    }
}