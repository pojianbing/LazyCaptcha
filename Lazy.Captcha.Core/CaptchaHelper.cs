using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core
{
    /// <summary>
    /// 验证码帮助类
    /// </summary>
    public class CaptchaHelper
    {
        private static CaptchaService CaptchaService;

        public static void Initialization(CaptchaService captchaService)
        {
            CaptchaService = captchaService;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
        /// <returns></returns>
        public static CaptchaData Generate(string captchaId, int? expirySeconds = null)
        {
            return CaptchaService.Generate(captchaId, expirySeconds);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="code">用户输入的验证码</param>
        /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
        /// <returns></returns>
        public static bool Validate(string captchaId, string code, bool removeIfSuccess = true)
        {
            return CaptchaService.Validate(captchaId, code, removeIfSuccess);
        }
    }
}
