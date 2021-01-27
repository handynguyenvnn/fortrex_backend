using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;



namespace Lib.Domain.Coins
{
    [DataContract]
    public class ExchangeInfoResponse
    {
        [DataMember(Order = 1)]
        public string Timezone { get; set; }

        [DataMember(Order = 2)]
        
        public DateTime ServerTime { get; set; }

        [DataMember(Order = 3)]
        public List<ExchangeInfoRateLimit> RateLimits { get; set; }

        // ExchangeFilters, array of unknown type

        [DataMember(Order = 5)]
        public List<ExchangeInfoSymbol> Symbols { get; set; }
    }
}
