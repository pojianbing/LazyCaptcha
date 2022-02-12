using Lazy.Captcha.Core.Storeage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public static class CaptchaServiceCollectionExtensions
    {
        public static IServiceCollection AddCaptcha(this IServiceCollection services)
        {
            services.AddCaptcha(e => { });
            return services;
        }

        public static IServiceCollection AddCaptcha(this IServiceCollection services, Action<CaptchaOption> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Scoped<IStorage, DefaultStorage>());
            services.TryAdd(ServiceDescriptor.Scoped<ICaptcha, DefaultCaptcha>());

            services.Configure(configureOptions);
            return services;
        }
    }
}
