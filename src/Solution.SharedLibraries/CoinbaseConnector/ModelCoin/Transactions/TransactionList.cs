using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinbaseConnector.ModelCoin.Base;
using CoinbaseConnector.ModelCoin.Pagins;

namespace CoinbaseConnector.ModelCoin.Transactions
{
    public class TransactionList
    {
        public Pagin pagination { get; set; }
        public List<Transaction> data { get; set; }
    }
}
