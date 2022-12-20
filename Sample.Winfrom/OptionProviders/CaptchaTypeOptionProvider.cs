using Lazy.Captcha.Core.Generator;
using Sample.Winfrom.Models;
using System.Collections.Generic;

namespace Sample.Winfrom.OptionProviders
{
    public class CaptchaTypeOptionProvider
    {
        public static List<CaptchaTypeOption> Provide()
        {
            return new List<CaptchaTypeOption>
            {
                new CaptchaTypeOption("默认", CaptchaType.DEFAULT),
                new CaptchaTypeOption("中文汉字", CaptchaType.CHINESE),
                new CaptchaTypeOption("数字", CaptchaType.NUMBER),
                new CaptchaTypeOption("中文数字（小写）", CaptchaType.NUMBER_ZH_CN),
                new CaptchaTypeOption("中文数字（大写）", CaptchaType.NUMBER_ZH_HK),
                new CaptchaTypeOption("字母大小写", CaptchaType.WORD),
                new CaptchaTypeOption("字母小写", CaptchaType.WORD_LOWER),
                new CaptchaTypeOption("字母大写", CaptchaType.WORD_UPPER),
                new CaptchaTypeOption("字母数字小写", CaptchaType.WORD_NUMBER_LOWER),
                new CaptchaTypeOption("字母数字大写", CaptchaType.WORD_NUMBER_UPPER),
                new CaptchaTypeOption("阿拉伯数字运算", CaptchaType.ARITHMETIC),
                new CaptchaTypeOption("中文数字运算", CaptchaType.ARITHMETIC_ZH),
            };
        }
    }
}
