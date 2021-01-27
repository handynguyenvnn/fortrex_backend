using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Response
{
    public class WithdrawGetFromWalletResponse
    {
        public string Symbol { get; set; }
        public string CoinName { get; set; }
        public string CoinType { get; set; }
        public string CoinInfo { get; set; }
    }
    
}
