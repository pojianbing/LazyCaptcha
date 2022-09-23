using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lazy.Captcha.Core.Storage.Caches
{
    public class MemoryCache
    {
        private Timer CacheCheckTimer;
        private ConcurrentDictionary<string, CacheEntity> Cache = new ConcurrentDictionary<string, CacheEntity>();
        public static MemoryCache Default = new MemoryCache(60);

        public MemoryCache(int checkCacheIntervalInSecs)
        {
            CacheCheckTimer = new Timer();
            CacheCheckTimer.Interval = checkCacheIntervalInSecs * 1000;
            CacheCheckTimer.Elapsed += CacheCheckTimer_Elapsed;
            CacheCheckTimer.Start();
        }

        public string Get(string key)
        {
            if (Cache.TryGetValue(key, out var data))
            {
                if (data.IsExpired)
                {
                    Cache.TryRemove(key, out var _);
                    return null;
                }
                return data.Value;
            }
            return null;
        }

        public void Remove(string key)
        {
            Cache.TryRemove(key, out var _);
        }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            var data = new CacheEntity(key, value);
            data.AbsoluteExpiration = absoluteExpiration;

            Cache.TryRemove(key, out _);
            Cache.TryAdd(key, data);
        }

        /// <summary>
        /// 过期检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CacheCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    foreach (string cacheKey in Cache.Keys)
                    {
                        if (Cache.TryGetValue(cacheKey, out var data))
                        {
                            if (data.IsExpired)
                            {
                                Cache.TryRemove(cacheKey, out var _);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }
    }
}
