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
            // 为了演示，这里仅做返回处理
            return _captcha.Validate(id, code);
        }
    }
}