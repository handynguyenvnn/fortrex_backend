using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Response
{
    public class DepositListCoinNameResponse
    {
        public string Symbol { get; set; }
        public string CoinName { get; set; }
        public string CoinType { get; set; }
        public string CoinInfo { get; set; }
    }
    public class DeposiGetWalletResponse
    {
        public string Symbol { get; set; }
        public string CoinName { get; set; }
        public string WalletAddress { get; set; }
        public string Memo { get; set; }
        public string CoinInfo { get; set; }
    }
}
