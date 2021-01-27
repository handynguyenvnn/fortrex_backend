using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseConnector.ModelCoin.Pagins
{
    public class Pagin
    {
        public string ending_before { get; set; }
        public string starting_after { get; set; }
        public int limit { get; set; }
        public string order { get; set; }
        public string previous_uri { get; set; }
        public string next_uri { get; set; }
    }
}
