using Lazy.Captcha.Core.Generator;

namespace Sample.Winfrom.Models
{
    public class CaptchaTypeOption : Option<CaptchaType>
    {
        public CaptchaTypeOption(string text, CaptchaType value)
        {
            Text = text;
            Value = value;
        }
    }
}
