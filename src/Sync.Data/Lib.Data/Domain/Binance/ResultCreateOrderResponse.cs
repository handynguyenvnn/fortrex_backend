using System.Runtime.Serialization;


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// Result Response following a call to the Create Order endpoint
    /// </summary>
    [DataContract]
    public partial class ResultCreateOrderResponse 
    {
        [DataMember(Order = 4)]
        public decimal Price { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "origQty")]
        public decimal OriginalQuantity { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQuantity { get; set; }

    }
}