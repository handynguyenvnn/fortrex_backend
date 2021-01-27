using System.Collections.Generic;
using Lib.Domain.Simples;

namespace Lib.Cache
{
    public partial class CacheKeyManager
    {
        public static class SettingValueCache
        {
            public static CacheType BaseKey = new CacheType("fc:ms");
            public static class CacheTypes
            {
                public static CacheType SettingValue = new CacheType("SettingValue");
                public static CacheType PriceBTCToUSD = new CacheType("PriceBTCToUSD");
                public static CacheType PriceETHToUSD = new CacheType("PriceETHToUSD");
                public static CacheType PriceXRPToUSD = new CacheType("PriceXRPToUSD");
                public static CacheType TotalUser = new CacheType("TotalUsers");
                public static CacheType Packages = new CacheType("Packages");
            }

            public static CacheKey<IDictionary<string, Setting>> Cache__GetAllSettingValue
            {
                get
                {
                    return CacheKey<IDictionary<string, Setting>>.CreateKey(BaseKey, CacheTypes.SettingValue, "sets");
                }
            }

            public static CacheKey<decimal> Cache__GetUSDByBTC
            {
                get
                {
                    return CacheKey<decimal>.CreateKey(BaseKey, CacheTypes.PriceBTCToUSD, "btc");
                }
            }
            public static CacheKey<decimal> Cache__GetUSDByETH
            {
                get
                {
                    return CacheKey<decimal>.CreateKey(BaseKey, CacheTypes.PriceETHToUSD, "eth");
                }
            }
            public static CacheKey<decimal> Cache__GetUSDByXRP
            {
                get
                {
                    return CacheKey<decimal>.CreateKey(BaseKey, CacheTypes.PriceXRPToUSD, "xrp");
                }
            }
            public static CacheKey<int> Cache__GetTotalUser
            {
                get
                {
                    return CacheKey<int>.CreateKey(BaseKey, CacheTypes.TotalUser, "user");
                }
            }
            public static CacheKey<List<Lib.Domain.Packages.PackagesEntity>> Cache__GetPackages
            {
                get
                {
                    return CacheKey<List<Lib.Domain.Packages.PackagesEntity>>.CreateKey(BaseKey, CacheTypes.Packages, "pac");
                }
            }
        }
    }
}
