using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserBranchBalance
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
        public decimal Bonus { get; set; }
        public decimal? BranchLeft { get; set; }
        public decimal? BranchRight { get; set; }
    }
}
