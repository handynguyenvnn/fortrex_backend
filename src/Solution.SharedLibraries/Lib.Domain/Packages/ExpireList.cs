using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class ExpireList
    {
        public int Id { get; set; }
        public decimal Invested { get; set; }
        public DateTime ExpireDate { get; set; }
        public string StrExpireDate {
            get {return ExpireDate.ToString("yyyy/MM/dd HH:mm:ss");}
        }
    }
}
