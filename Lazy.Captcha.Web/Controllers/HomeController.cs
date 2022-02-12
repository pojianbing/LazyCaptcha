using Lazy.Captcha.Core;
using Lazy.Captcha.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lazy.Captcha.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICaptcha _captcha;

        public HomeController(ILogger<HomeController> logger, ICaptcha captcha)
        {
            _logger = logger;
            _captcha = captcha;
        }

        [HttpGet]
        public IActionResult Captcha(string id)
        {
            var info = _captcha.Generate(id);
            var stream = new MemoryStream(info.Bytes);
            return File(stream, "image/png");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}