using System.Runtime.Serialization;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxNumIcebergOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public int MaxNumIcebergOrders { get; set; }
    }
}