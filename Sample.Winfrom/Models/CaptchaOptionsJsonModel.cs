using Lazy.Captcha.Core.Generator.Image.Option;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Winfrom.Models
{
    public class CaptchaOptionsJsonModel
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public int CaptchaType { get; set; }

        /// <summary>
        /// 验证码长度 当CaptchaType为ARITHMETIC, ARITHMETIC_ZH时， 长度代表乘数个数
        /// </summary>
        public int CodeLength { get; set; }

        /// <summary>
        /// 过期时长
        /// </summary>
        public int ExpirySeconds { get; set; }

        /// <summary>
        /// code比较是否忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// 存储键前缀
        /// </summary>
        public string StoreageKeyPrefix { get; set; }

        /// <summary>
        /// 图片选项
        /// </summary>
        public CaptchaImageGeneratorOptionJsonModel ImageOption { get; set; } = new CaptchaImageGeneratorOptionJsonModel();
    }
}
