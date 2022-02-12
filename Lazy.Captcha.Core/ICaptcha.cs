using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public interface ICaptcha
    {
        CaptchaData Generate(string captchaId);

        bool Validate(string captchaId, string code);
    }
}
