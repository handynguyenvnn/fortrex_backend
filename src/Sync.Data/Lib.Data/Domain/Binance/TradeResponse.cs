using System.Runtime.Serialization;


namespace Lib.Domain.Coins
{
    /// <summary>
    /// Trade response, providing price and quantity information
    /// </summary>
    [DataContract]
    public class TradeResponse
    {
        [DataMember(Order = 1)]
        public decimal Price { get; set; }

        [DataMember(Order = 2)]
        public decimal Quantity { get; set; }
    }
}