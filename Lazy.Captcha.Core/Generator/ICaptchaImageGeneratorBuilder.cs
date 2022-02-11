using Lazy.Captcha.Core.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public interface ICaptchaImageGeneratorBuilder
    {
        ICaptchaImageGenerator Build();
    }
}
