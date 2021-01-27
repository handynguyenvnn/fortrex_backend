using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Domain.Coins
{
    public class BalanceCoin
    {
        public BalanceCoin()
        {
            Total = 0;
            Available = 0;
            this.symbol = "";
        }

        public decimal Total { get; set; }
        public decimal Available { get; set; }
        public string symbol { get; set; }
    }
}
