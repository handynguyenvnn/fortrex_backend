using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins
{
    public class Random_Orders_WinLose
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public decimal RandomPrice { get; set; }
        public bool TypeRandom { get; set; }
        public decimal MatchingPrice { get; set; }
        public bool IsActive { get; set; }
    }
    
}
