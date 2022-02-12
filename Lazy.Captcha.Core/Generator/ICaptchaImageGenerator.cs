using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public interface ICaptchaImageGenerator
    {
        byte[] Generate(string text, CaptchaImageGeneratorOption option);
    }
}
