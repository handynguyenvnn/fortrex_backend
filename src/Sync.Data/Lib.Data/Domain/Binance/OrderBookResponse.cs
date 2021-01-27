using System.Collections.Generic;
using System.Runtime.Serialization;


using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
    [DataContract]
    public class OrderBookResponse
    {
        [DataMember(Order = 1)]
        public long LastUpdateId { get; set; }

    }
}