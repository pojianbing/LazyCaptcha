﻿using Lazy.Captcha.Core.Storeage;
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
        private readonly IOptionsMonitor<CaptchaOption> _options;
        private readonly IStorage _storage;

        public DefaultCaptcha(IOptionsMonitor<CaptchaOption> options, IStorage storage)
        {
            _options = options;
            _storage = storage;
        }

        public CaptchaData Generate(string captchaId)
        {
            var (renderText, code) = _options.CurrentValue.CodeGenerator.Generate(_options.CurrentValue.CodeLength);
            var image = _options.CurrentValue.ImageGenerator.Generate(renderText, _options.CurrentValue.ImageGeneratorOption);
            _storage.Set(captchaId, code, DateTimeOffset.UtcNow.Add(_options.CurrentValue.ExpiryTime));

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