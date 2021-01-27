using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class BonusCoin
    {
        public string Username { get; set; }
        public decimal AmountGet { get; set; }
        public decimal AmountSet { get; set; }
        public decimal Fee { get; set; }
        public int FromType { get; set; }
        public string Email { get; set; }
        public string HashCode { get; set; }
        public string Transaction { get; set; }
        public int ToType { get; set; }
    }
}
