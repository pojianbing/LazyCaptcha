using System;
using System.Collections.Generic;
using System.Text;
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Storage;
using Lazy.Captcha.Core.Storeage;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisCacheCaptchaExtensions
    {
        public static IServiceCollection AddRedisCacheCaptcha(this IServiceCollection services, IConfiguration configuration, Action<CaptchaOptions> optionsAction = default!, string connName = null!)
        {
            var connStr = configuration.GetConnectionString(connName??"RedisCache");
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Please set connection string for key `RedisCache` in setting json file");
            }
            return services
                .AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = connStr;
                    options.InstanceName = "captcha:";
                })
                .AddScoped<IStorage, DefaultStorage>()
                .AddCaptcha(configuration, optionsAction);
        }
    }
}