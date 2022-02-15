using System;
using System.Collections.Generic;
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Storage;
using Lazy.Captcha.Core.Storeage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
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

            services.TryAdd(ServiceDescriptor.Scoped<ICaptcha, DefaultCaptcha>());

            services.Configure(configureOptions);
            return services;
        }

        public static IServiceCollection AddMemoryCacheCaptcha(this IServiceCollection services, Action<CaptchaOption> configureOptions)
        {
            return services
                .AddDistributedMemoryCache()
                .AddScoped<IStorage, DefaultStorage>()
                .AddCaptcha(configureOptions);
        }
    }
}