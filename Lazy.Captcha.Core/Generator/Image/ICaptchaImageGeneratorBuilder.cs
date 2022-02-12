using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Image.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Image
{
    public interface ICaptchaImageOptionBuilder
    {
        CaptchaImageGeneratorOption Build();
    }
}
