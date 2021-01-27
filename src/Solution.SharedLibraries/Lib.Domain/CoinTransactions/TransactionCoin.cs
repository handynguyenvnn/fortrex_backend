using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.CoinTransactions
{
    public class TransactionCoin
    {
        public int Id { get; set; }
        public int MethodPayment { get; set; }
        public string MethodPaymentName { get; set; }
        public string MethodSymbol { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal PriceCoin { get; set; }
        public decimal PriceUSD { get; set; }
        public string AddressWallet { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string HashCode { get; set; }
        public string TransactionId { get; set; }
    }

    public class TransactionCoinList
    {
        public List<TransactionCoin> LastDeposit { get; set; }
        public List<TransactionCoin> LastReciered { get; set; }
    }

    public class LogDerviceList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string CreateOn { get; set; }
        public string Status { get; set; }
    }



}
