using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins.Deposit
{
    
    public partial class DepositList
    {
        public decimal Id { get; set; }

        public int UserId { get; set; }

        public string WalletType { get; set; }
        public string CoinType { get; set; }

        public decimal CoinValue { get; set; }


        public decimal AmountUSD { get; set; }

        public string WalletAddress { get; set; }

        public string FromAddress { get; set; }

        public string TxHash { get; set; }

        public decimal BlockNumber { get; set; }

        public int? FillConfirm { get; set; }

        public int? Confirmations { get; set; }

        public bool? Success { get; set; }
        public int? timestamp { get; set; }
        
        public DateTime? CreateAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
