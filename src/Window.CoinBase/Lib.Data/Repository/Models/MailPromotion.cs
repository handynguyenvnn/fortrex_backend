using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class MailPromotion
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Code { get; set; }
        public decimal MinValueBtc { get; set; }
        public decimal MinValueEth { get; set; }
        public decimal TotalReceivedBtc { get; set; }
        public decimal TotalReceivedEth { get; set; }
    }
}
