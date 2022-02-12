using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public class DefaultCaptchaCodeGenerator : ICaptchaCodeGenerator
    {
        public (string renderText, string code) Generate(int length)
        {
            throw new NotImplementedException();
        }
    }
}
