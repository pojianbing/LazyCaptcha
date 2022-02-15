using System;
using System.Collections.Generic;
using System.Text;
using Lazy.Captcha.Core.Storage;

namespace Lazy.Captcha.Redis
{
    public class RedisStorage : IStorage
    {
        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, string value, DateTimeOffset absoluteExpiration)
        {
            throw new NotImplementedException();
        }
    }
}