using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class Mail_UserMining
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateOn { get; set; }
        public decimal Bonus { get; set; }
        public DateTime NextTimeOn { get; set; }
        public int Status { get; set; }
        public bool IsFinish { get; set; }
    }
}
