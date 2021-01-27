using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Withdraws
{
    public class WithdrawETH
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatePay { get; set; }
        public string WalletETH { get; set; }
        public string Username { get; set; }
        public string DisplayDate { get { return CreatePay.ToString("yyyy-MM-dd HH:mm:ss"); } }
        public int Status { get; set; }
    }
}
