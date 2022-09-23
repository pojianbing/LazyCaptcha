using Lazy.Captcha.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.MvcFramework.Controllers
{
    public class CaptchaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var id = Guid.NewGuid().ToString().Replace("_", "").Replace("-", "");
            var captchaData = CaptchaHelper.Generate(id);
            var output = new CaptchaResponse
            {
                Id = id,
                Base64 = captchaData.Base64
            };
            return Json(output, JsonRequestBehavior.AllowGet);
           
        }

        /// <summary>
        /// 演示时使用HttpGet传参方便，这里仅做返回处理
        /// </summary>
        [HttpGet()]
        public bool Validate(string id, string code)
        {
            return CaptchaHelper.Validate(id, code);
        }
    }

    public class CaptchaResponse
    {
        public string Id { get; set; }
        public string Base64 { get; set; }
    }
}