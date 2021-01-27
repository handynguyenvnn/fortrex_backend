using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public class UserDepositByUSDT
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string TxHash { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
    }
}