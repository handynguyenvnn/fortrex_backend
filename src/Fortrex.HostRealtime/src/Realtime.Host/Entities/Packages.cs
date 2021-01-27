using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Packages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public decimal PercentOnDay { get; set; }
        public decimal PercentTotal { get; set; }
        public decimal PlusPercent { get; set; }
        public decimal FinishDay { get; set; }
        public int DisplayOrder { get; set; }
        public int? Status { get; set; }
    }
}
