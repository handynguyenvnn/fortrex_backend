using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lib.Cache
{
    /// <summary>
    /// Represents a MemoryCacheCache
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        static MemoryCacheManager()
        {
            _cache = new MemoryCache("__defaultCache");
        }

        protected static MemoryCache _cache;
        protected static MemoryCache Cache
        {
            get
            {
                return _cache;
            }
            private set
            {
                _cache = value;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key, out bool isSet)
        {
            isSet = false;
            if (IsSet(key))
            {
                isSet = true;
                return (T)Cache[key];
            }
            return default(T);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set<T>(string key, T data, TimeSpan cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            //policy.ChangeMonitors.Add(new MemoryChangeMonitor(CommonHelper.GetPattenKey(key)));
            policy.AbsoluteExpiration = DateTime.Now.Add(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(params string[] keys)
        {
            if (keys == null)
                return;
            foreach (var key in keys)
            {
                Cache.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(params string[] patterns)
        {
            //PatternChangeMonitor.PublicPatternChange(patterns);
            System.Threading.Tasks.Task.Run(() =>
            {
                string pattern = "";
                foreach (var item in patterns)
                {
                    pattern += string.Format("({0})|", item.Replace(".", "\\.").Replace(":", "\\:"));
                }

                var regex = new Regex(pattern.TrimEnd('|'), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var keysToRemove = new List<String>();

                foreach (var item in Cache)
                    if (regex.IsMatch(item.Key))
                        keysToRemove.Add(item.Key);

                foreach (string key in keysToRemove)
                {
                    Remove(key);
                }
            });

        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            var oldCache = _cache;
            _cache = new MemoryCache("__defaultCache");

            oldCache.Dispose();
        }

        public void ClearKeyFor(string key)
        {
            foreach(var item in Cache)
            {
                if(item.Key.Contains(key))
                {
                    Remove(item.Key);
                }
            }
        }

        class MemoryChangeMonitor : ChangeMonitor
        {
            PatternChangeMonitor _patternChangeMonitor;
            public MemoryChangeMonitor(string pattern)
            {
                this._uniqueId = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
                _patternChangeMonitor = PatternChangeMonitor.GetMonitor(pattern, this);
            }


            protected override void Dispose(bool disposing)
            {
                _patternChangeMonitor.MemoryChangeMonitors.Remove(this);
            }

            public override string UniqueId
            {
                get { return _uniqueId; }
            }

            string _uniqueId;
        }

        class PatternChangeMonitor
        {
            public static PatternChangeMonitor GetMonitor(string pattern, MemoryChangeMonitor monitor)
            {
                PatternChangeMonitor result = null;
                if (PatternChangeMonitorContainer.ContainsKey(pattern))
                    result = PatternChangeMonitorContainer[pattern];
                else
                {
                    result = new PatternChangeMonitor(pattern);
                    if (!PatternChangeMonitorContainer.TryAdd(pattern, result))
                        result = PatternChangeMonitorContainer[pattern];
                }

                return result;
            }

            public static void PublicPatternChange(params string[] patterns)
            {
                foreach (var pattern in patterns)
                {
                    PatternChangeMonitor patternChangeMonitor = null;
                    if (PatternChangeMonitorContainer.TryGetValue(pattern, out patternChangeMonitor))
                    {
                        foreach (var item in patternChangeMonitor.MemoryChangeMonitors)
                        {
                            item.NotifyOnChanged(null);
                        }
                    }
                }
            }

            public List<MemoryChangeMonitor> MemoryChangeMonitors { get; set; }

            static System.Collections.Concurrent.ConcurrentDictionary<string, PatternChangeMonitor> PatternChangeMonitorContainer = new System.Collections.Concurrent.ConcurrentDictionary<string, PatternChangeMonitor>();

            PatternChangeMonitor(string pattern)
            {
                this.Pattern = pattern;
            }

            public string Pattern { get; set; }

        }

    }
}