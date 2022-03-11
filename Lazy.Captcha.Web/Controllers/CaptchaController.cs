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
        public Task<bool> Validate(string id, string code)
        {
            // 为了演示，这里仅做返回处理
            return _captcha.ValidateAsync(id, code);
        }
         
        [HttpGet("validate_remove_later")]
        public Task<bool> ValidateAndRemoveLater(string id, string code)
        {
            // 为了演示，这里仅做返回处理
            // 与上面方法一样，但是这里校验时，讲过期时间设置为10秒后，多查了一次，性能不如直接删除
            return _captcha.ValidateAsync(id, code, TimeSpan.FromSeconds(10));
        }
    }
}