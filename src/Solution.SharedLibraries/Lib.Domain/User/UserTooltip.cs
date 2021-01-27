using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class UserTooltip
    {
        public string FullName { get; set; }
        public string Sponsor { get; set; }
        public decimal MaxInvest { get; set; }
        public decimal TotalInvest { get; set; }
        public decimal TotalLeft { get; set; }
        public decimal TotalRight { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
