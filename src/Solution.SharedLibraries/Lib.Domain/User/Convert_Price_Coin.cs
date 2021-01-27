using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class Convert_Price_Coin
    {
        public decimal OriginUSD { get; set; }
        public decimal Amount { get; set; }
        public decimal XRP { get; set; }
        public decimal XRPUSD { get; set; }
        public decimal XGT { get; set; }
        public string strAmount { get; set; }
        public string strXRP { get; set; }
        public string strXGT { get; set; }
    }
}
