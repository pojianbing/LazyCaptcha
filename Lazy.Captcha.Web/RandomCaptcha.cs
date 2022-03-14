using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Options;

namespace Lazy.Captcha.Web
{
    /// <summary>
    /// 随机验证码
    /// </summary>
    public class RandomCaptcha : DefaultCaptcha
    {
        public RandomCaptcha(IOptionsSnapshot<CaptchaOptions> options, IStorage storage) : base(options, storage)
        {

        }

        /// <summary>
        /// 更新选项
        /// </summary>
        /// <param name="options"></param>
        protected override void ChangeOptions(CaptchaOptions options)
        {
            var random = new Random();

            // 随机验证码类型
            var captchaTypes = new CaptchaType[] { CaptchaType.ARITHMETIC, CaptchaType.NUMBER, CaptchaType.WORD_LOWER, CaptchaType.ARITHMETIC_ZH, };
            options.CaptchaType = captchaTypes[random.Next(0, captchaTypes.Length)];

            // 当是算数运算时，CodeLength是指运算数个数
            if (options.CaptchaType.IsArithmetic())
            {
                options.CodeLength = 2;
            }
            else
            {
                options.CodeLength = 4;
            }

            // 如果包含中文时，使用kaiti字体，否则文字乱码
            if (options.CaptchaType.ContainsChinese())
            {
                options.ImageOption.FontFamily = DefaultFontFamilys.Instance.Kaiti;
            }
            else
            {
                options.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj;
            }

            // 动静随机
            options.ImageOption.Animation = random.Next(2) == 0;

            // 其他选项...
        }
    }
}
