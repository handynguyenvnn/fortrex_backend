using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.BuyCoins
{
    public class TotalCoinChildren
    {
        public decimal Total { get; set; }
        public decimal Level { get; set; }
    }
    public class Totalvolumebuysell
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public decimal TotalBuy { get; set; }
        public decimal TotalSell { get; set; }
        public bool IsActive { get; set; }
        public bool TypeRandom { get; set; }
    }

    public class DasboardSumData
    {
        public int TotalTrade { get; set; }
        public int TotalDay { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MaxBonusTrade { get; set; }
        public string MarkWin { get; set; }
    }

    public class AffiliateStatistic
    {
        public int NetworkMember { get; set; }
        public decimal AgencyVol { get; set; }
        public decimal AgencyCom { get; set; }
        public decimal TeamDailyTrade { get; set; }
        public string ReferralCode { get; set; }
    }

    public class NetworkStatistic
    {
        public int Token { get; set; }
        public decimal TotalTrading { get; set; }
        public decimal TotalCom { get; set; }
    }

    public class NetworkLevelSum
    {
        public int Level { get; set; }
        public int Total { get; set; }
    }
}
