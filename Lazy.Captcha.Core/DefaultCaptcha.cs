using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    /// <summary>
    /// 默认验证码服务
    /// </summary>
    public class DefaultCaptcha : ICaptcha
    {
        private readonly CaptchaOptions _options;
        private readonly IStorage _storage;
        private readonly ICaptchaCodeGenerator _captchaCodeGenerator;
        private readonly ICaptchaImageGenerator _captchaImageGenerator;

        public DefaultCaptcha(IOptionsMonitor<CaptchaOptions> options, IStorage storage)
        {
            _options = options.CurrentValue;
            _storage = storage;
            _captchaCodeGenerator = new DefaultCaptchaCodeGenerator(options.CurrentValue.CaptchaType);
            _captchaImageGenerator = new DefaultCaptchaImageGenerator();
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        public CaptchaData Generate(string captchaId)
        {
            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.ImageOption);
            _storage.Set(captchaId, code, DateTime.Now.AddSeconds(_options.ExpirySeconds).ToUniversalTime());

            return new CaptchaData(captchaId, code, image);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        public Task<CaptchaData> GenerateAsync(string captchaId, CancellationToken token = default)
        {
            return Task.Run(() => Generate(captchaId), token);
        }

        /// <summary>
        /// 校验
        /// </summary>
        public bool Validate(string captchaId, string code, TimeSpan? delay = null)
        {
            var val = _storage.Get(captchaId);
            var comparisonType = _options.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var result = string.Equals(val, code, comparisonType);

            // 仅成功并需要延迟移除时
            if (result && delay != null)
            {
                _storage.RemoveLater(captchaId, delay.Value);
            }
            // 否则直接移除
            else
            {
                _storage.Remove(captchaId);
            }

            return result;
        }

        /// <summary>
        /// 校验
        /// </summary>
        public async Task<bool> ValidateAsync(string captchaId, string code, TimeSpan? delay = null, CancellationToken token = default)
        {
            var val = _storage.Get(captchaId);
            var comparisonType = _options.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var result = string.Equals(val, code, comparisonType);

            // 仅成功并需要延迟移除时
            if (result && delay != null)
            {
                await _storage.RemoveLaterAsync(captchaId, delay.Value, token);
            }
            // 否则直接移除
            else
            {
                await _storage.RemoveAsync(captchaId, token);
            }
            return result;
        }
    }
}