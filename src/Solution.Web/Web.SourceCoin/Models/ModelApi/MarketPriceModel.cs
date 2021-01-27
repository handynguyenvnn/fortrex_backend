using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.SourceCoin.Models.ModelApi
{
    public class MarketPriceModel
    {
        public MarketPriceModel()
        {
            Item = 300;
            Sortby = "asc";
        }

        public string Pair { get; set; }
        public int Item { get; set; }
        public string Sortby { get; set; }
    }

    public class AffiliateChartMembersResponse
    {
        public int F1 { get; set; }
        public int F2 { get; set; }
        public int F3 { get; set; }
        public int F4 { get; set; }
        public int F5 { get; set; }
        public int F6 { get; set; }
        public int F7 { get; set; }
        public int F8 { get; set; }
        public int F9 { get; set; }
        public int F10 { get; set; }
        public int F11 { get; set; }
        public int F12 { get; set; }
        public int F13 { get; set; }
    }
}