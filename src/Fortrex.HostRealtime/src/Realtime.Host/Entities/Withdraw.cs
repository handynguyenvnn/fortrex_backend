using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Withdraw
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FromType { get; set; }
        public int ToType { get; set; }
        public decimal AmountSet { get; set; }
        public decimal Fee { get; set; }
        public decimal AmountGet { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Transaction { get; set; }
        public string HashCode { get; set; }
        public string TokenConfirm { get; set; }
        public bool? IsConfirmEmail { get; set; }
        public DateTime? ConfirmDate { get; set; }
    }
}
