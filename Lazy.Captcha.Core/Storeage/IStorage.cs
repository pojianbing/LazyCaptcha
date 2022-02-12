using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Storeage
{
    public interface IStorage
    {
        void Set(string key, string value, DateTimeOffset absoluteExpiration);
        string Get(string key);
        void Remove(string key);
    }
}
