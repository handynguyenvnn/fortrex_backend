using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Withdraws
{

    public class Withdraw
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FromType { get; set; }
        public string FromTypeName { get; set; }
        public int ToType { get; set; }
        public string ToTypeName { get; set; }
        public decimal AmountSet { get; set; }
        public string strAmountSet { get; set; }
        public decimal Fee { get; set; }
        public decimal AmountGet { get; set; }
        public string strAmountGet { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreateDate { get; set; }
        public string strCreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? ApproveBy { get; set; }
        public string ApproveName { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string strApproveDate { get; set; }
        public string Transaction { get; set; }
        public string HashCode { get; set; }
        public string Method { get; set; }
        public string TokenConfirm { get; set; }
        public bool? IsConfirmEmail { get; set; }
    }
}
