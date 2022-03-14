using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Options;

namespace Lazy.Captcha.Web
{
    public class RandomCaptcha : ICaptcha
    {
        private readonly CaptchaOptions _options;
        private readonly IStorage _storage;
        private readonly ICaptchaCodeGenerator _captchaCodeGenerator;
        private readonly ICaptchaImageGenerator _captchaImageGenerator;

        public RandomCaptcha(IOptionsMonitor<CaptchaOptions> options, IStorage storage)
        {
            _options = options.CurrentValue;
            _storage = storage;

            RandomOptions(options.CurrentValue);

            _captchaCodeGenerator = new DefaultCaptchaCodeGenerator(_options.CaptchaType);
            _captchaImageGenerator = new DefaultCaptchaImageGenerator();
        }

        private void RandomOptions(CaptchaOptions options)
        {
            var random = new Random();

            // 随机验证码类型
            var captchaTypes = new CaptchaType[] { CaptchaType.ARITHMETIC, CaptchaType.NUMBER, CaptchaType.WORD_LOWER, CaptchaType.ARITHMETIC_ZH, };
            options.CaptchaType = captchaTypes[random.Next(0, captchaTypes.Length)];

            // 当是算数表达式时，CodeLength是指运算数个数
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
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
        /// <returns></returns>
        public CaptchaData Generate(string captchaId, int? expirySeconds = null)
        {
            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.ImageOption);
            expirySeconds = expirySeconds.HasValue ? expirySeconds.Value : _options.ExpirySeconds;
            _storage.Set(captchaId, code, DateTime.Now.AddSeconds(expirySeconds.Value).ToUniversalTime());

            return new CaptchaData(captchaId, code, image);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="code">用户输入的验证码</param>
        /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
        /// <returns></returns>
        public bool Validate(string captchaId, string code, bool removeIfSuccess = true)
        {
            var val = _storage.Get(captchaId);
            var comparisonType = _options.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var success = string.Equals(val, code, comparisonType);

            if (!success || (success && removeIfSuccess))
            {
                _storage.Remove(captchaId);
            }

            return success;
        }
    }
}
