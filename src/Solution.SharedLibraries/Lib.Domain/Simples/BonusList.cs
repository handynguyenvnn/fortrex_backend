using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    public class BonusList
    {
        public decimal Bonus { get; set; }
        public decimal BonusBranch { get; set; }
        public decimal MaxBonusBranch { get; set; }
        public decimal LuckyBonus { get; set; }
        public int MaxLevelLuckyBonus { get; set; }
    }
}
