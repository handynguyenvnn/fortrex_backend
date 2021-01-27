using System.Runtime.Serialization;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoSymbolFilterExchangeMaxNumAlgoOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public int Limit { get; set; }
    }
}