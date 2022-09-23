using Lazy.Captcha.Core;
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
               .FontFamily(DefaultFontFamilys.Instance.Actionj)
               .Animation(true)
               .Build();
            CaptchaHelper.Initialization(captchaService);
        }
    }
}
