using System;
using System.Threading;
using System.Threading.Tasks;
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

        public Task<string> GetAsync(string key, CancellationToken token = default)
        {
            return _cache.GetStringAsync(WrapKey(key), token);
        }

        public void Remove(string key)
        {
            _cache.Remove(WrapKey(key));
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return _cache.RemoveAsync(key, token);
        }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            _cache.SetString(WrapKey(key), value, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration
            });
        }

        public Task SetAsync(string key, string value, DateTimeOffset absoluteExpiration, CancellationToken token = default(CancellationToken))
        {
            return _cache.SetStringAsync(WrapKey(key), value, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration
            }, token);
        }
    }
}