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

        public void RemoveLater(string key, TimeSpan expireTimeSpan)
        {
            var value = _cache.GetString(WrapKey(key));
            if (!string.IsNullOrEmpty(value))
            {
                _cache.SetString(WrapKey(key), value, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expireTimeSpan
                });
            }
        }

        /// <summary>
        /// 重新设置过期时间 TODO：看看有没有其他仅访问一次缓存的方式
        /// </summary>
        public async Task RemoveLaterAsync(string key, TimeSpan expireTimeSpan, CancellationToken token = default)
        {
            var value = await _cache.GetStringAsync(WrapKey(key), token);
            if (!string.IsNullOrEmpty(value))
            {
                await _cache.SetStringAsync(WrapKey(key), value, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expireTimeSpan
                });
            }
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return _cache.RemoveAsync(key, token);
        }

        /// <summary>
        /// 缓存验证码
        /// </summary>
        public void Set(string key, string value, TimeSpan expireTimeSpan)
        {
            _cache.SetString(WrapKey(key), value, new DistributedCacheEntryOptions
            {
                SlidingExpiration = expireTimeSpan
            });
        }

        /// <summary>
        /// 缓存验证码
        /// </summary>
        public Task SetAsync(string key, string value, TimeSpan expireTimeSpan, CancellationToken token = default)
        {
            return _cache.SetStringAsync(WrapKey(key), value, new DistributedCacheEntryOptions
            {
                SlidingExpiration = expireTimeSpan
            }, token);
        }
    }
}