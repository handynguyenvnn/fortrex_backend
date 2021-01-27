using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class WithdrawProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WalletType { get; set; }
        public string WalletAddress { get; set; }
        public decimal AmountSet { get; set; }
        public decimal Fee { get; set; }
        public decimal AmountGet { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? FillConfirm { get; set; }
        public int? Confirmations { get; set; }
        public string TxHash { get; set; }
        public bool? IsConfirmEmail { get; set; }
    }
}
