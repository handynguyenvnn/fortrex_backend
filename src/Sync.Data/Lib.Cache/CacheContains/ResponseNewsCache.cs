namespace Lib.Cache
{
    public partial class CacheKeyManager
    {
        public static class ResponseNewsCache
        {
            public static CacheType BaseKey = new CacheType("fc:cate");
            public static class CacheTypes
            {
                public static CacheType ResponseNews = new CacheType("ResponseNews");
            }

            //public static CacheKey<ResponseNewsList, int, int, int> NewsCache__ByCategoryId
            //{
            //    get
            //    {
            //        string key = "ResponseNews__ByCategoryId";
            //        CacheKey result;
            //        if (!KeyContainer.TryGetValue(key, out result))
            //        {
            //            var newKey = CacheKey<ResponseNewsList, int, int, int>.CreateKey(BaseKey, CacheTypes.ResponseNews, "catep-{0}.s-{1}.id-{2}");
            //            newKey.KeyFor = (type, param) =>
            //            {
            //                return new KeyReturnType();
            //            };
            //            KeyContainer.Add(key, newKey);
            //            return newKey;
            //        }
            //        return result as CacheKey<ResponseNewsList, int, int, int>;
            //    }
            //}
        }
    }
}
