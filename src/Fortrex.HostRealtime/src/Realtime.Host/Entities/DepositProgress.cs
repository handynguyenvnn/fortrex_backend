using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class DepositProgress
    {
        public decimal Id { get; set; }
        public int UserId { get; set; }
        public string WalletType { get; set; }
        public decimal CoinValue { get; set; }
        public decimal AmountUsd { get; set; }
        public string WalletAddress { get; set; }
        public string TxHash { get; set; }
        public int? FillConfirm { get; set; }
        public int? Confirmations { get; set; }
        public bool? Success { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? Timestamp { get; set; }
        public string FromAddress { get; set; }
    }
}
