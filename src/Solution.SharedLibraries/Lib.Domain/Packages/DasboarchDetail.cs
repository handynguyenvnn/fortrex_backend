using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class DasboarchDetail
    {
        public DasboarchDetail()
        {
            IsEnableFA = true;
        }
        public decimal MoneyBTC { get; set; }
        public decimal MoneyETH { get; set; }
        public decimal TotalInvestment { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal BonusCommission { get; set; }
        public decimal MoneyUSD { get; set; }
        public int TotalNetwork { get; set; }
        public decimal BonusBranch { get; set; }
        public decimal BonusLucky { get; set; }
        public decimal Reinvestment { get; set; }
        public decimal TotalInvestLeft { get; set; }
        public decimal TotalInvestRight { get; set; }
        public int TotalUserLeft { get; set; }
        public int TotalUserRight { get; set; }
        public decimal MaxInvest { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal Fee { get; set; }
        public string FA2Code { get; set; }
        public string Username { get; set; }
        public decimal TotalBranchLeft { get; set; }
        public decimal TotalBranchRight { get; set; }
        public bool IsEnableFA { get; set; }
        public decimal TotalBonus { get; set; }
        public decimal MoneyDemo { get; set; }
        public int TotalF1 { get; set; }
        public decimal TotalTrade { get; set; }
        public string StrTotalTrade { get; set; }

        public string StrTotalNetworkTrading { get; set; }

    }
}
