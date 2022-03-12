using System;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Lazy.Captcha.Core.Storeage
{
    public class DefaultStorage : IStorage
    {
        private readonly IDistributedCache _cache;
        private readonly IOptionsMonitor<CaptchaOptions> _options;

        public DefaultStorage(IOptionsMonitor<CaptchaOptions> options, IDistributedCache cache)
        {
            _options = options;
            _cache = cache;
        }

        private string WrapKey(string key)
        {
            return $"{this._options.CurrentValue.StoreageKeyPrefix}{key}";
        }

        public string Get(string key)
        {
            return _cache.GetString(WrapKey(key));
        }

        public void Remove(string key)
        {
            _cache.Remove(WrapKey(key));
        }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            _cache.SetString(WrapKey(key), value, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration
            });
        }
    }
}