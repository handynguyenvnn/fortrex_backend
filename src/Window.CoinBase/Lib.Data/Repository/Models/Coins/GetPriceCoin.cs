using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins
{
    public class GetPriceCoin
    {
        public double price_usd { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
    }
    public class CoinBaseGetPriceCoinData
    {
        public CoinBaseGetPriceCoin data { get; set; }
    }
    public class CoinBaseGetPriceCoin
    {
        public string bases { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
    }
}
