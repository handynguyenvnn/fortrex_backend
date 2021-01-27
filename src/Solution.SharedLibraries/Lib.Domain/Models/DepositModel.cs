using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Models
{
    public class DepositModel
    {
        public string Symbol { get; set; }
    }
    public class DepositHisotyModel
    {
        public string Symbol { get; set; }
        public string WalletName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
