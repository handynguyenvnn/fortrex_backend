using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Trees
{
    public class User_Branch_Balance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal LeftAmount { get; set; }
        public decimal RightAmount { get; set; }
        public decimal LeftReset { get; set; }
        public decimal RightReset { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int ByUid { get; set; }
        public int PackageId { get; set; }
        public decimal MaxInvest { get; set; }
    }
}
