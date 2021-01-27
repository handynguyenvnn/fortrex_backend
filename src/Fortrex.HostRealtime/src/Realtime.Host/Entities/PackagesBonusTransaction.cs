using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class PackagesBonusTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Bonus { get; set; }
        public DateTime Day { get; set; }
        public int PackagesId { get; set; }
        public decimal PercentAmount { get; set; }
        public int Status { get; set; }
    }
}
