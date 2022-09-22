using Lazy.Captcha.Core.Storage.Caches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core.Storage
{
    public class MemeoryStorage : IStorage
    {
        private MemoryCache Cache;
        public string StoreageKeyPrefix { get; set; } = string.Empty;

        public MemeoryStorage()
        {
            Cache = MemoryCache.Default;
        }

        private string WrapKey(string key)
        {
            return $"{StoreageKeyPrefix}{key}";
        }

        public string Get(string key)
        {
            return Cache.Get(WrapKey(key));
        }

        public void Remove(string key)
        {
            Cache.Remove(WrapKey(key));
        }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            Cache.Set(WrapKey(key), value, absoluteExpiration);
        }
    }
}
