using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseConnector.ModelCoin.Transactions
{
    public class TransactionBuy
    {
        public string id { get; set; }
        public string resource { get; set; }
        public string resource_path { get; set; }
    }
}
