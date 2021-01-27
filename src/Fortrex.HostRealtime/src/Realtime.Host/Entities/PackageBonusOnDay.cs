using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class PackageBonusOnDay
    {
        public DateTime CreateOn { get; set; }
        public decimal BonusPercent { get; set; }
    }
}
