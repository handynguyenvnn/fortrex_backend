using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class WithdrawExpire
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FromType { get; set; }
        public int ToType { get; set; }
        public decimal AmountSet { get; set; }
        public decimal Fee { get; set; }
        public decimal AmountGet { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Transaction { get; set; }
        public string HashCode { get; set; }
        public string TokenConfirm { get; set; }
        public bool? IsConfirmEmail { get; set; }
        public DateTime? ConfirmDate { get; set; }
    }
}
