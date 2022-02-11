using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public class DefaultCaptchaGenerator : BaseCaptchaGenerator
    {
        public DefaultCaptchaGenerator() : this(new CaptchaGeneratorOption())
        {

        }

        public DefaultCaptchaGenerator(CaptchaGeneratorOption option): base(option)
        {
    
        }
    }
}
