using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    internal class CaptchaBuilder : ICaptchaBuilder
    {
        public CaptchaBuilder(IServiceCollection services)
        {
            Services = services;
        }
        public IServiceCollection Services { get; }
    }
}
