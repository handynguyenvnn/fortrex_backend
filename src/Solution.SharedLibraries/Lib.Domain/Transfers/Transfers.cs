using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Transfers
{
    // using Send and Received money FROM FORBITOPTION TO COPYTRADE AND 
    public class TransfersFromToWalletModel
    {
        public int UserIDForbit { get; set; }
        public decimal AmountUSD { get; set; }
        public string Username { get; set; }
        public string FromtoWallet { get; set; }
    }
}
