using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinbaseConnector.ModelCoin.Pagins;

namespace CoinbaseConnector.ModelCoin
{
    public class AccountList
    {
        public Pagin pagination { get; set; }
        public List<Account> data { get; set; }
    }
}
