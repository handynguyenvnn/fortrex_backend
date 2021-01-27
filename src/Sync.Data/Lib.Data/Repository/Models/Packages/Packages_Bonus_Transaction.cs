using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.Packages
{
    public class Packages_Bonus_Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Bonus { get; set; }
        public DateTime Day { get; set; }
        public int PackagesId { get; set; }
        public decimal PercentAmount { get; set; }
        public int Type { get; set; }
        public decimal TotalBonus { get; set; }
        public decimal MaxBonusOnMonth { get; set; }
    }
}
