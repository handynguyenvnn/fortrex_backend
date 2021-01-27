using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class AddressMail
    {
        public AddressMail()
        {
            Bonus = 0;
            Status = 0;
            NextTimeOn = DateTime.Now;
            IsFinish = false;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Bonus { get; set; }
        public int Status { get; set; }
        public DateTime NextTimeOn { get; set; }
        public bool IsFinish { get; set; }
    }
}
