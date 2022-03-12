using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.Captcha.Web.Controllers
{
    [Route("captcha")]
    [ApiController]
    public class CaptchaController : Controller
    {
        private readonly ICaptcha _captcha;

        public CaptchaController(ICaptcha captcha)
        {
            _captcha = captcha;
        }

        [HttpGet]
        public IActionResult Captcha(string id)
        {
            var info = _captcha.Generate(id);
            // 可能会有多处验证码且过期时间不一样，可传第二个参数覆盖默认配置。（https://gitee.com/pojianbing/lazy-captcha/issues/I4XHGM）
            //var info = _captcha.Generate(id,120);
            var stream = new MemoryStream(info.Bytes);
            return File(stream, "image/gif");
        }

        /// <summary>
        /// 演示时使用HttpGet传参方便，这里仅做返回处理
        /// </summary>
        [HttpGet("validate")]
        public bool Validate(string id, string code)
        {
            return _captcha.Validate(id, code);
        }

        /// <summary>
        /// 一个验证码多次校验（https://gitee.com/pojianbing/lazy-captcha/issues/I4XHGM）
        /// 演示时使用HttpGet传参方便，这里仅做返回处理
        /// </summary>
        [HttpGet("validate2")]
        public bool Validate2(string id, string code)
        {
            return _captcha.Validate(id, code, false);
        }
    }
}