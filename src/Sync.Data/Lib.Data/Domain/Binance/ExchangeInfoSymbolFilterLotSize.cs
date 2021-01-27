using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;



namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoSymbolFilterLotSize : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinQty { get; set; }

        [DataMember(Order = 2)]
        public Decimal MaxQty { get; set; }

        [DataMember(Order = 3)]
        public Decimal StepSize { get; set; }
    }
}
