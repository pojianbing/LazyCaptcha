using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Storage
{
    public interface IStorage
    {
        void Set(string key, string value, DateTimeOffset absoluteExpiration);

        string Get(string key);

        void Remove(string key);
        Task<string> GetAsync(string key, CancellationToken token = default);
        Task RemoveAsync(string key, CancellationToken token = default);
        Task SetAsync(string key, string value, DateTimeOffset absoluteExpiration, CancellationToken token = default);
    }
}