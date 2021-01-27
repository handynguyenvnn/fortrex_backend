using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Tasks.Coinbase
{
    [DataContract]
    public class CoinbaseTimeResponse
    {
        [DataMember(Order = 1)]
        public CoinbaseTime data { get; set; }
    }
    public class CoinbaseTime
    {
        [DataMember(Order = 1)]
        public DateTime iso { get; set; }

        [DataMember(Order = 2)]
        public long epoch { get; set; }
    }
}
