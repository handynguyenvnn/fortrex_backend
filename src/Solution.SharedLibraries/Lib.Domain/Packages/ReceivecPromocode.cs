using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class ReceivecPromocode
    {
        public int Id { get; set; }
        public decimal Received { get; set; }
        public DateTime DayOn { get; set; }
        public bool IsFinish { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public decimal Money { get; set; }
    }
}
