using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class Receive
    {
        public decimal Bonus { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string HashCode { get; set; }
    }
}
