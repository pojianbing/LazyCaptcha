using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Generator.Image.Option;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class CaptchaOption
    {
        private const int DEFAULT_CODE_LENGTH = 4;
        private CaptchaType _captchaType = CaptchaType.DEFAULT;

        /// <summary>
        /// 验证码类型
        /// </summary>
        public CaptchaType CaptchaType
        {
            get { return _captchaType; }
            set
            {
                if (value.IsArithmetic())
                {
                    if (this.CodeLength == DEFAULT_CODE_LENGTH)
                    {
                        this.CodeLength = 2;
                    }
                }

                if (value.ContainsChinese())
                {
                    this.ImageOption.FontFamily = DefaultFontFamilys.instance.Kaiti;
                }

                _captchaType = value;
            }
        }
        /// <summary>
        /// 验证码长度
        /// 当CaptchaType为ARITHMETIC, ARITHMETIC_ZH时， 长度代表乘数个数
        /// </summary>
        public int CodeLength { get; set; } = DEFAULT_CODE_LENGTH;
        /// <summary>
        /// 过期时长
        /// </summary>
        public TimeSpan ExpiryTime { get; set; } = TimeSpan.FromMinutes(5);
        /// <summary>
        /// code比较是否忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; } = true;
        /// <summary>
        /// 图片选项
        /// </summary>
        public CaptchaImageGeneratorOption ImageOption { get; set; } = new CaptchaImageGeneratorOption();
    }
}
