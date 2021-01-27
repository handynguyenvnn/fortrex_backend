using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.Packages
{
    public class BonusSale
    {
        public BonusSale()
        {
            IsLock = false;
        }
        public int UserId { get; set; }
        public decimal Bonus { get; set; }
        public decimal TotalLeft { get; set; }
        public decimal TotalRight { get; set; }
        public int RankId { get; set; }
        public bool IsLock { get; set; }
    }

    public class RankIdPercent
    {
        public int RankId { get; set; }
        public decimal Percent { get; set; }
    }
}
