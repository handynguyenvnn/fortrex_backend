using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class LendingList
    {
        public string Name { get; set; }
        public decimal PriceUSD { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreateOnDate { get; set; }
        public decimal TotalBonus { get; set; }
        public int FinishDay { get; set; }
        public DateTime ExpireDate { get; set; }
        public int EndTime { get; set; }
        public decimal Status { get; set; }
    }
}
