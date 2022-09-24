using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sample.MvcFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CaptchaConfig();
        }

        private void CaptchaConfig()
        {
            var captchaService = CaptchaServiceBuilder
               .New()
               .Width(98)
               .Height(35)
               .FontSize(20)
               .CaptchaType(CaptchaType.ARITHMETIC)
               .FontFamily(DefaultFontFamilys.Instance.Ransom)
               .InterferenceLineCount(3)
               .Animation(false)
               .Build();
            CaptchaHelper.Initialization(captchaService);
        }
    }
}
