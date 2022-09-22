using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core.Storage.Caches
{
    /// <summary>
    /// 缓存实体
    /// </summary>
    public class CacheEntity
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTimeOffset AbsoluteExpiration { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired
        {
            get
            {
                DateTimeOffset now = DateTime.Now.ToUniversalTime();
                return AbsoluteExpiration.Subtract(now).TotalMilliseconds < 0;
            }
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        public CacheEntity(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
