using System;
using System.Collections.Generic;
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Storage;
using Lazy.Captcha.Core.Storeage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CaptchaServiceCollectionExtensions
    {
        public static IServiceCollection AddCaptcha(this IServiceCollection services)
        {
            return services.AddScoped<ICaptcha, DefaultCaptcha>();
        }

        public static IServiceCollection AddCaptcha(this IServiceCollection services, IConfiguration configuration, Action<CaptchaOptions> optionsAction = default!)
        {
            var options = new CaptchaOptions();
            optionsAction?.Invoke(options);
            services.Configure<CaptchaOptions>(opt =>
            {
                opt = options;
            });
            services.Configure<CaptchaOptions>(configuration?.GetSection("CaptchaOptions"));
            services.TryAdd(ServiceDescriptor.Scoped<ICaptcha, DefaultCaptcha>());
            return services;
        }

        public static IServiceCollection AddMemoryCacheCaptcha(this IServiceCollection services, IConfiguration configuration, Action<CaptchaOptions> optionsAction = default!)
        {
            return services
                .AddDistributedMemoryCache()
                .AddScoped<IStorage, DefaultStorage>()
                .AddCaptcha(configuration, optionsAction);
        }
    }
}