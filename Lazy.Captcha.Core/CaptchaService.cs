using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using Lazy.Captcha.Core.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core
{
    public class CaptchaService
    {
        private CaptchaOptions CaptchaOptions;
        private IStorage Storage;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="captchaOptions"></param>
        public CaptchaService(CaptchaOptions captchaOptions, IStorage storage)
        {
            CaptchaOptions = captchaOptions;
            Storage = storage;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
        /// <returns></returns>
        public CaptchaData Generate(string captchaId, int? expirySeconds = null)
        {
            var _captchaCodeGenerator = new DefaultCaptchaCodeGenerator(CaptchaOptions.CaptchaType);
            var _captchaImageGenerator = new DefaultCaptchaImageGenerator();

            var (renderText, code) = _captchaCodeGenerator.Generate(CaptchaOptions.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, CaptchaOptions.ImageOption);
            expirySeconds = expirySeconds.HasValue ? expirySeconds.Value : CaptchaOptions.ExpirySeconds;
            Storage.Set(captchaId, code, DateTime.Now.AddSeconds(expirySeconds.Value).ToUniversalTime());

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
            var val = Storage.Get(captchaId);
            var comparisonType = CaptchaOptions.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var success = !string.IsNullOrWhiteSpace(code) &&
                          !string.IsNullOrWhiteSpace(val) &&
                          string.Equals(val, code, comparisonType);

            if (!success || (success && removeIfSuccess))
            {
                Storage.Remove(captchaId);
            }

            return success;
        }
    }
}
