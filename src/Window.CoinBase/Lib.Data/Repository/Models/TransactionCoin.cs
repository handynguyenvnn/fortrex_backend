using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class TransactionCoin
    {
        public int Id { get; set; }
        public int MethodPayment { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal BTC { get; set; }
        public decimal USD { get; set; }
        public string AddressWallet { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string HashCode { get; set; }
        public string TransactionId { get; set; }
        public DateTime ServerTime { get; set; }
    }
}
