using Lazy.Captcha.Core.Generator.Image.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Image
{
    public class DefaultCaptchaImageGenerator : BaseCaptchaImageGenerator
    {
        public DefaultCaptchaImageGenerator() : this(new CaptchaImageGeneratorOption())
        {

        }

        public DefaultCaptchaImageGenerator(CaptchaImageGeneratorOption option): base(option)
        {
    
        }
    }
}
