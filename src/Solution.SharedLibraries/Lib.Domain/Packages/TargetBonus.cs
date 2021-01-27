using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class TargetBonus
    {
        public decimal Bonus { get; set; }
        public decimal Target { get; set; }
        public string Node { get; set; }
        public int Percent { get; set; }
        public decimal TotalIncomeCurrent { get; set; }
        public decimal MaxOut { get; set; }
        public decimal Receied { get; set; }
        public decimal MaxoutPercent { get; set; }
    }
}
