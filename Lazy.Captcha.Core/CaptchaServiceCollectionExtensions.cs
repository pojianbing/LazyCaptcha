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
            services.Configure<CaptchaOptions>(configuration?.GetSection("CaptchaOptions"));
            services.PostConfigure<CaptchaOptions>(options =>
            {
                var fontFamily = configuration?.GetSection("CaptchaOptions:ImageOption:FontFamily")?.Value;
                if (!string.IsNullOrWhiteSpace(fontFamily))
                {
                    options.ImageOption.FontFamily = DefaultFontFamilys.Instance.GetFontFamily(fontFamily);
                }
            });
            if (optionsAction != null) services.PostConfigure<CaptchaOptions>(optionsAction);

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