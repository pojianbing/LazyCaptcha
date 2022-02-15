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
            var stream = new MemoryStream(info.Bytes);
            return File(stream, "image/gif");
        }

        [HttpGet("validate")]
        public bool Validate(string id, string code)
        {
            if (!_captcha.Validate(id, code))
            {
                throw new Exception("无效验证码");
            }

            // 具体业务

            // 为了演示，这里仅做返回处理
            return true;
        }
    }
}