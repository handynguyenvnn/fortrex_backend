using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class CoinTransactionList
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal PriceCoin { get; set; }
        public decimal PriceUSD { get; set; }
        public string Status { get; set; }
        public int MethodPayment { get; set; }
        public string TransactionId { get; set; }
        public int TimeMinutes { get; set; }
        public string AddressWallet { get; set; }
    }
}
