using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.TronCoins
{
    public class CoinTransaction
    {
        public string TransactionId { get; set; }
        public int MethodPayment { get; set; }
        public string AddressWallet { get; set; }
    }
}
