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

        public CaptchaData Generate(string captchaId)
        {
            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CurrentValue.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.CurrentValue.ImageOption);
            _storage.Set(captchaId, code, DateTime.Now.AddSeconds(_options.CurrentValue.ExpirySeconds).ToUniversalTime());

            return new CaptchaData(captchaId, code, image);
        }

        public bool Validate(string captchaId, string code)
        {
            var val = _storage.Get(captchaId);
            var comparisonType = _options.CurrentValue.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var result = string.Equals(val, code, comparisonType);
            if (result)
            {
                _storage.Remove(captchaId);
            }

            return result;
        }
    }
}