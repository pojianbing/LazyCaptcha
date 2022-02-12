using Lazy.Captcha.Core.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class CaptchaOption
    {
        public ICaptchaCodeGenerator CodeGenerator { get; set; } = new DefaultCaptchaCodeGenerator();

        public CaptchaImageGeneratorOption ImageGeneratorOption { get; set; } = new CaptchaImageGeneratorOption();

        public ICaptchaImageGenerator ImageGenerator { get; set; } = new DefaultCaptchaImageGenerator();

        public int CodeLength { get; set; } = 4;

        public TimeSpan ExpiryTime { get; set; } = TimeSpan.FromMinutes(5);

        public bool IgnoreCase { get; set; } = true;
        
    }
}
