using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public class DefaultImageCaptchaGenerator : BaseCaptchaImageGenerator
    {
        public DefaultImageCaptchaGenerator() : this(new CaptchaImageGeneratorOption())
        {

        }

        public DefaultImageCaptchaGenerator(CaptchaImageGeneratorOption option): base(option)
        {
    
        }
    }
}
