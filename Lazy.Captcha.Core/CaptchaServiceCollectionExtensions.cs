using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

                var backgroundColor = configuration?.GetSection("CaptchaOptions:ImageOption:BackgroundColor")?.Value;
                if (!string.IsNullOrWhiteSpace(backgroundColor))
                {
                    if (SixLabors.ImageSharp.Color.TryParse(backgroundColor, out var color))
                    {
                        options.ImageOption.BackgroundColor = color;
                    }
                }
                                                                
                var foregroudColors = configuration?.GetSection("CaptchaOptions:ImageOption:ForegroundColors")?.Value;
                if (!string.IsNullOrWhiteSpace(foregroudColors))
                {
                    var colors = foregroudColors.Split(",").ToList().Where(e => !string.IsNullOrEmpty(e));
                    foreach (var item in colors)
                    {
                        if (SixLabors.ImageSharp.Color.TryParse(item, out var color))
                        {
                            if (options.ImageOption.ForegroundColors == null)
                            {
                                options.ImageOption.ForegroundColors = new List<SixLabors.ImageSharp.Color>();
                            }
                            options.ImageOption.ForegroundColors.Add(color);
                        }
                    }
                }
                if (options.ImageOption.ForegroundColors == null || options.ImageOption.ForegroundColors.Count() == 0)
                {
                    options.ImageOption.ForegroundColors = DefaultColors.Instance.Colors;
                }
            });
            if (optionsAction != null) services.PostConfigure(optionsAction);

            services.TryAdd(ServiceDescriptor.Scoped<ICaptcha, DefaultCaptcha>()); 
            services.AddScoped<IStorage, DefaultStorage>();
            services.AddDistributedMemoryCache();

            return services;
        }

        [Obsolete("请直接使用 AddCaptcha 方法，已更改为默认注册内存缓存", false)]
        public static IServiceCollection AddMemoryCacheCaptcha(this IServiceCollection services, IConfiguration configuration, Action<CaptchaOptions> optionsAction = default!)
        {
            return services.AddCaptcha(configuration, optionsAction);
        }
    }
}