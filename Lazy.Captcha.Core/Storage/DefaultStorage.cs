using System;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Caching.Distributed;

namespace Lazy.Captcha.Core.Storeage
{
    public class DefaultStorage : IStorage
    {
        private readonly IDistributedCache _cache;

        public DefaultStorage(IDistributedCache cache)
        { _cache = cache; }

        public string Get(string key)
        { return _cache.GetString(key); }

        public void Remove(string key)
        { _cache.Remove(key); }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            _cache.SetString(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration
            });
        }
    }
}