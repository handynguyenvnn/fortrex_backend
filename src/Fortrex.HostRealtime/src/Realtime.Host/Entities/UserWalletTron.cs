using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserWalletTron
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Bonus73Percent { get; set; }
        public decimal Bonus20Percent { get; set; }
        public decimal Bonus7Percent { get; set; }
        public DateTime UpdateOn { get; set; }
        public int PackageId { get; set; }
        public bool? IsReinvestment { get; set; }
    }
}
