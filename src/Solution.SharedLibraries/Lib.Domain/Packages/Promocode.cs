using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class Promocode
    {
        public int Id { get; set; }
        public decimal Percent { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Package { get; set; }
        public int? Status { get; set; }
        public string Code { get; set; }
        public decimal MinValueBtc { get; set; }
        public decimal MinValueEth { get; set; }
        public int TotalDays { get; set; }
        public decimal TotalReceivedBtc { get; set; }
        public decimal TotalReceivedEth { get; set; }
    }
}
