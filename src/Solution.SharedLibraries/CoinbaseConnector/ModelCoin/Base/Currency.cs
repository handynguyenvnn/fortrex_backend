using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseConnector.ModelCoin.Base
{
    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int exponent { get; set; }
        public string type { get; set; }
    }
}
