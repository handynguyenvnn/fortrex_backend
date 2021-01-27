using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class PackagesEntity
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
        public int Status { get; set; }
    }
}
