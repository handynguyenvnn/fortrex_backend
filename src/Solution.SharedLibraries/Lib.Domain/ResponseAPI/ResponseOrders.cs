using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Lib.Domain.ModelApi
{
    public class ResponsePushOrders
    {
        public decimal PriceOrder { get; set; }
        public string Pairname { get; set; }
    }
    public class ResponseOrdersResult
    {
        public decimal PriceOrder { get; set; }
        public string Pairname { get; set; }
    }

    public class ViewDasboardSumData
    {
        public int Total_Win_Max { get; set; }
        public int PercentWin { get; set; }
        public int PercentLose { get; set; }
        public double Avg_Trade_On_Day { get; set; }
        public double Avg_Amount { get; set; }
        public double Max_Receive_Bonus { get; set; }
    }

    public class ViewAffiliateStatistic
    {
        public string NetworkMember { get; set; }
        public string AgencyVol { get; set; }
        public string AgencyCom { get; set; }
        public string TeamDailyTrade { get; set; }
        public string ReferralCode { get; set; }
    }

    public class ViewNetworkStatistic
    {
        public string Token { get; set; }
        public string Vol { get; set; }
        public string Com { get; set; }
    }

    public class ViewProfitStatistic
    {
        public string Token { get; set; }
        public string Vol { get; set; }
        public string Profit { get; set; }
    }

    public class ViewLevelNetwork
    {
        public int Level { get; set; }
        public int Total { get; set; }
    }
}