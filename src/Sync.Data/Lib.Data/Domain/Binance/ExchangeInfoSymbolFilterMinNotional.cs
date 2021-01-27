using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;



namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMinNotional : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinNotional { get; set; }
    }
}
