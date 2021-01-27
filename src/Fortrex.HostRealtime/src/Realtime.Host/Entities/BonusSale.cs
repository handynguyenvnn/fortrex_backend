using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class BonusSale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Bonus { get; set; }
        public DateTime CreateOn { get; set; }
        public bool IsActive { get; set; }
        public int RankId { get; set; }
    }
}
