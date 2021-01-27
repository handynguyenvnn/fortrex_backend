using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins.Deposit
{
    [DataContract()]
    public partial class DepositList
    {
        public decimal Id { get; set; }

        public int UserId { get; set; }
        [DataMember(Name = "type", Order = 6)]
        public string WalletType { get; set; }
        public string CoinType { get; set; }
        [DataMember(Name = "amount", Order = 4)]
        public string Amount { get; set; }
        public decimal CoinValue { get; set; }
        [DataMember(Name = "from", Order = 2)]
        public string FromAddress { get; set; }
        [DataMember(Name = "to", Order = 3)]
        public string WalletAddress { get; set; }
        [DataMember(Name = "txID", Order = 5)]
        public string TxHash { get; set; }

        public decimal BlockNumber { get; set; }
        [DataMember(Name = "fillConfirm", Order = 8)]
        public int? FillConfirm { get; set; }
        [DataMember(Name = "confirmations", Order = 9)]
        public int? Confirmations { get; set; }

        public bool? Success { get; set; }
        public int? timestamp { get; set; }
        
        public DateTime? CreateAt { get; set; }
        [DataMember(Name = "time", Order = 0)]
        public string StrCreateDate { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        [DataMember(Name = "status", Order = 7)]
        public string Status { get; set; }
    }
    [DataContract()]
    public partial class DepositWithdrawsReponse
    {
        public decimal Id { get; set; }

        public int UserId { get; set; }
        [DataMember(Name = "type", Order = 6)]
        public string TypeAction { get; set; }
        public string CoinType { get; set; }

        public decimal CoinValue { get; set; }

        [DataMember(Name = "amount", Order = 4)]
        public decimal AmountUSD { get; set; }

        public string WalletAddress { get; set; }
        [DataMember(Name = "from", Order = 2)]
        public string FromAddress { get; set; }
        [DataMember(Name = "to", Order = 3)]
        public string ToAddress { get; set; }
        [DataMember(Name = "txID", Order = 5)]
        public string TxHash { get; set; }

        public decimal BlockNumber { get; set; }

        public int? FillConfirm { get; set; }

        public int? Confirmations { get; set; }

        public bool? Success { get; set; }
        public int? timestamp { get; set; }
        [DataMember(Name = "time", Order = 0)]
        public DateTime? CreateAt { get; set; }

        public DateTime? CompletedAt { get; set; }
        [DataMember(Name = "status", Order = 7)]
        public string Status { get; set; }
    }
}
