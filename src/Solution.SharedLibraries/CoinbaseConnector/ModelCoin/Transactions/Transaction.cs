using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinbaseConnector.ModelCoin.Base;

namespace CoinbaseConnector.ModelCoin.Transactions
{
    public class Transaction
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public Balance amount { get; set; }
        public Balance native_amount { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string resource { get; set; }
        public string resource_path { get; set; }
        public TransactionBuy buy { get; set; }
        public TransactionDetail details { get; set; }
        public TransactionNetwork network { get; set; }
    }
}
