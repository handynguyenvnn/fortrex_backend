using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Text;

namespace Lib.Cache
{

    [Serializable]
    public class CacheVersionWraper<T>
    {
        public CacheVersionWraper()
        {

        }
        public CacheVersionWraper(T value, int version = 0)
        {
            this.CacheValue = value;
            this.Version = version;

        }
        public T CacheValue { get; set; }
        public int Version { get; set; }
    }

    [Serializable]
    public class CacheWraper<T>
    {
        public CacheWraper()
        {

        }
        public CacheWraper(T value)
        {
            CacheValue = value;
        }
        public T CacheValue { get; set; }
    }

    [Serializable]
    public class CacheWraper<T, P1, P2>
    {
        public CacheWraper()
        {

        }
        public CacheWraper(T value, P1 p1, P2 p2)
        {
            CacheValue = value;
            Param1 = p1;
            Param2 = p2;
        }
        public T CacheValue { get; set; }

        public P1 Param1 { get; set; }
        public P2 Param2 { get; set; }
    }

    [Serializable]
    public class CacheWraper<T, P1, P2, P3>
    {
        public CacheWraper()
        {

        }
        public CacheWraper(T value, P1 p1, P2 p2, P3 p3)
        {
            CacheValue = value;
            Param1 = p1;
            Param2 = p2;
            Param3 = p3;
        }
        public T CacheValue { get; set; }

        public P1 Param1 { get; set; }
        public P2 Param2 { get; set; }
        public P3 Param3 { get; set; }
    }

    [Serializable]
    public class CacheWraper<T, P1>
    {
        public CacheWraper()
        {

        }
        public CacheWraper(T value, P1 p1)
        {
            CacheValue = value;
            Param1 = p1;
        }
        public T CacheValue { get; set; }

        public P1 Param1 { get; set; }
    }
    /// <summary>
    /// Extensions
    /// </summary>
    public static partial class CacheExtensions
    {
        static TimeSpan DefaultCacheTime = new TimeSpan(0, 15, 0);
        public static T GetByStringCacheKey<T>(string key, Func<T> acquire)
        {
            return GetByStringCacheKey(key, DefaultCacheTime, acquire);
        }

        public static T GetByStringCacheKey<T>(string key, TimeSpan cacheTime, Func<T> acquire)
        {
            MemoryCacheManager ca = new MemoryCacheManager();
            try
            {
#if TRACE
                Stopwatch watch = new Stopwatch();
                watch.Start();

#endif
                T result;

                bool isSet = false;
                var cacheResult = ca.Get<T>(key, out isSet);
                var cacheExeTime = watch.Elapsed;
                if (cacheExeTime >= TimeSpan.FromSeconds(.1))
                {
                    string stackInfo = Environment.StackTrace;
                    if (stackInfo.Length > 2000)
                        stackInfo = stackInfo.Substring(0, 2000);
                }
                if (!isSet)
                {
                    cacheResult = acquire();
                    if (!ReferenceEquals(cacheResult, null))
                        ca.Set<T>(key, cacheResult, cacheTime);
                }
                result = cacheResult;
                return result;

            }
            catch (Exception)
            {
#if DEBUG
                throw;
#else
                return acquire();
#endif
            }

        }

        public static T Get<T>(CacheKey<T> key, Func<T> acquire)
        {
            return Get(key, key.CacheTime, acquire);
        }

        public static T Get<T>(CacheKey<T> key, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey().Key, cacheTime, acquire);
        }

        public static T Get<T, P1>(CacheKey<T, P1> key, P1 p1, Func<T> acquire)
        {
            return Get(key, p1, key.CacheTime, acquire);
        }

        public static T Get<T, P1>(CacheKey<T, P1> key, P1 p1, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1).Key, cacheTime, acquire);
        }

        public static T Get<T, P1, P2>(CacheKey<T, P1, P2> key, P1 p1, P2 p2, Func<T> acquire)
        {
            return Get(key, p1, p2, key.CacheTime, acquire);
        }

        public static T Get<T, P1, P2>(CacheKey<T, P1, P2> key, P1 p1, P2 p2, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1, p2).Key, cacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3>(CacheKey<T, P1, P2, P3> key, P1 p1, P2 p2, P3 p3, Func<T> acquire)
        {
            return Get(key, p1, p2, p3, key.CacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3>(CacheKey<T, P1, P2, P3> key, P1 p1, P2 p2, P3 p3, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1, p2, p3).Key, cacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4>(CacheKey<T, P1, P2, P3, P4> key, P1 p1, P2 p2, P3 p3, P4 p4, Func<T> acquire)
        {
            return Get(key, p1, p2, p3, p4, key.CacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4>(CacheKey<T, P1, P2, P3, P4> key, P1 p1, P2 p2, P3 p3, P4 p4, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1, p2, p3, p4).Key, cacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4, P5>(CacheKey<T, P1, P2, P3, P4, P5> key, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, Func<T> acquire)
        {
            return Get(key, p1, p2, p3, p4, p5, key.CacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4, P5>(CacheKey<T, P1, P2, P3, P4, P5> key, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1, p2, p3, p4, p5).Key, cacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4, P5, P6>(CacheKey<T, P1, P2, P3, P4, P5, P6> key, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, Func<T> acquire)
        {
            return Get(key, p1, p2, p3, p4, p5, p6, key.CacheTime, acquire);
        }

        public static T Get<T, P1, P2, P3, P4, P5, P6>(CacheKey<T, P1, P2, P3, P4, P5, P6> key, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, TimeSpan cacheTime, Func<T> acquire)
        {
            return GetByStringCacheKey(key.ToKey(p1, p2, p3, p4, p5, p6).Key, cacheTime, acquire);
        }

        public static void RemoveCache(string key)
        {
            MemoryCacheManager ca = new MemoryCacheManager();
            ca.Remove(key);
        }
    }
}
