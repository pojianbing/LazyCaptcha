using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public interface ICaptcha
    {
        CaptchaData Generate(string captchaId);

        Task<CaptchaData> GenerateAsync(string captchaId, CancellationToken token = default);

        bool Validate(string captchaId, string code, TimeSpan? delay = null);

        Task<bool> ValidateAsync(string captchaId, string code, TimeSpan? delay = null, CancellationToken token = default);
    }
}