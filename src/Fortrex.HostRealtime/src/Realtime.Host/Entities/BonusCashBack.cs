using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class BonusCashBack
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Bonus { get; set; }
        public int Type { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
