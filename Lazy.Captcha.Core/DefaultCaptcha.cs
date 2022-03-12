using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class DefaultCaptcha : ICaptcha
    {
        private readonly IOptionsMonitor<CaptchaOptions> _options;
        private readonly IStorage _storage;
        private readonly ICaptchaCodeGenerator _captchaCodeGenerator;
        private readonly ICaptchaImageGenerator _captchaImageGenerator;

        public DefaultCaptcha(IOptionsMonitor<CaptchaOptions> options, IStorage storage)
        {
            _options = options;
            _storage = storage;
            _captchaCodeGenerator = new DefaultCaptchaCodeGenerator(options.CurrentValue.CaptchaType);
            _captchaImageGenerator = new DefaultCaptchaImageGenerator();
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
        /// <returns></returns>
        public CaptchaData Generate(string captchaId, int? expirySeconds = null)
        {
            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CurrentValue.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.CurrentValue.ImageOption);
            expirySeconds = expirySeconds.HasValue ? expirySeconds.Value : _options.CurrentValue.ExpirySeconds;
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
            var comparisonType = _options.CurrentValue.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var success = string.Equals(val, code, comparisonType);

            if (!success || (success && removeIfSuccess))
            {
                _storage.Remove(captchaId);
            }

            return success;
        }
    }
}