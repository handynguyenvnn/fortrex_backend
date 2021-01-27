using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class User_WalletAddress
    {
        public int UserId { get; set; }
        public string WalletBTC { get; set; }
        public string WalletETH { get; set; }
        public string WalletMy { get; set; }
        public string WalletStocks { get; set; }
        public decimal MoneyBTC { get; set; }
        public decimal MoneyETH { get; set; }
        public decimal MoneyUSD { get; set; }
        public decimal BonusBranch { get; set; }
        public decimal BonusLucky { get; set; }
        public decimal BonusCommission { get; set; }
        public decimal MaxInvest { get; set; }
        public int EnumMethod { get; set; }
        public decimal TotalBonus { get; set; }
        public decimal BonusSale { get; set; }
    }
}
