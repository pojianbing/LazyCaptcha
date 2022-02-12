using Lazy.Captcha.Core.Generator.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public static class CaptchaTypeExtensions
    {
        public static bool ContainsChinese(this CaptchaType captchaType)
        {
            return captchaType == CaptchaType.ARITHMETIC_ZH ||
                   captchaType == CaptchaType.NUMBER_ZH_CN ||
                   captchaType == CaptchaType.NUMBER_ZH_HK ||
                   captchaType == CaptchaType.CHINESE;
        }

        public static bool IsArithmetic(this CaptchaType captchaType)
        {
            return captchaType == CaptchaType.ARITHMETIC || captchaType == CaptchaType.ARITHMETIC_ZH;
        }
    }
}
