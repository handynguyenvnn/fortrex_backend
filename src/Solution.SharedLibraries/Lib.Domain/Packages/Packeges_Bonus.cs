using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class Packeges_Bonus
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
        public string Type { get; set; }
        public decimal StockAmount { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
