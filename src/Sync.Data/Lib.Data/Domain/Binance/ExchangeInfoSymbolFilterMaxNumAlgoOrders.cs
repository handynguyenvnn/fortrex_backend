using System;
using System.Runtime.Serialization;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxNumAlgoOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MaxNumAlgoOrders { get; set; }
    }
}
