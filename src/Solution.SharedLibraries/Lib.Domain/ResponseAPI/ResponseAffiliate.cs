using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Lib.Domain.ModelApi
{
    public class ResponseAffiliate
    {
        public int TotalF1 { get; set; }
        public int TotalNetworks { get; set; }
        public string YourTradingInWeek { get; set; }
        public string TotalNetWorkTradingInWeek { get; set; }
        public string ReferralCode { get; set; }
    }
    public class ResponseAffiliate_lst
    {
        public string CreateOn { get; set; }
        public string Account { get; set; }
        public bool IsActive { get; set; }
        public string Nation { get; set; }
        public string TotalTrading { get; set; }
    }
}