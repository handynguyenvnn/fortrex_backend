using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserWalletAddress
    {
      
        public int UserId { get; set; }

        public string WalletBTC { get; set; }

       
        public string WalletETH { get; set; }

        public string WalletMy { get; set; }

        public decimal MoneyBTC { get; set; }

        public decimal MoneyETH { get; set; }

        public decimal MoneyUSD { get; set; }

        public decimal MoneyGES { get; set; }
        public decimal MoneyELD { get; set; }
        public decimal MoneyBRI { get; set; }

        public decimal BonusBranch { get; set; }

        public decimal BonusLucky { get; set; }

        public decimal BonusCommission { get; set; }

        public decimal MaxInvest { get; set; }

        public string WalletStocks { get; set; }

        public decimal TotalBonus { get; set; }

        public decimal BonusSale { get; set; }

        public decimal? MoneyDemo { get; set; }
    }
}
