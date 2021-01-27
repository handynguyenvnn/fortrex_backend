using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Withdraws
{
    public class DataWithdraw
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal MoneyBTC { get; set; }
        public decimal MoneyETH { get; set; }
        public decimal MoneyBEH { get; set; }
        public decimal MoneyUSD { get; set; }
        public decimal Fee { get; set; }
        public decimal BTCToUSD { get; set; }
        public decimal ETHToUSD { get; set; }
        public decimal BEHToUSD { get; set; }
        public int FromType { get; set; }
        public int ToType { get; set; }
        public string FA2Code { get; set; }
        public string WalletAddress { get; set; }
        public int DrawType { get; set; }
    }
}
