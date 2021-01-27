using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.Packages
{
    public class Packages_Bonus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Invested { get; set; }
        public bool IsProfit { get; set; }
        public decimal SharePercent { get; set; }
        public decimal SharePrice { get; set; }
        public decimal ShareTotal { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime StartProfitDate { get; set; }
        public int IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public decimal ProfitRangeOne { get; set; }
        public decimal ProfitRangeTwo { get; set; }
        public decimal ProfitRangeThree { get; set; }
        public decimal ProfitRangeFour { get; set; }
        public decimal ProfitRangeFive { get; set; }
        public decimal ProfitRangeSix { get; set; }
        public decimal ProfitRangeSeven { get; set; }
        public decimal ProfitRangeEgx { get; set; }
    }
}
