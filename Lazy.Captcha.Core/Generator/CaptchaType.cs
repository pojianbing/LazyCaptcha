using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public enum CaptchaType
    {
        /// <summary>
        /// 中文
        /// </summary>
        CHINESE,
        /// <summary>
        /// 数字
        /// </summary>
        NUMBER,
        /// <summary>
        /// 中文数字小写
        /// </summary>
        NUMBER_ZH_CN,
        /// <summary>
        /// 中文数字大写
        /// </summary>
        NUMBER_ZH_HK,
        /// <summary>
        /// 默认(英文字符大小写，数字混合)
        /// </summary>
        DEFAULT,
        /// <summary>
        /// 英文字符大小写混合
        /// </summary>
        WORD,
        /// <summary>
        /// 英文字符小写
        /// </summary>
        WORD_LOWER,
        /// <summary>
        /// 英文字符大写
        /// </summary>
        WORD_UPPER,
        /// <summary>
        /// 英文小写，数字混合
        /// </summary>
        WORD_NUMBER_LOWER,
        /// <summary>
        /// 英文大写，数字混合
        /// </summary>
        WORD_NUMBER_UPPER,
        /// <summary>
        /// 数字计算
        /// </summary>
        ARITHMETIC,
        /// <summary>
        /// 数字计算，中文
        /// </summary>
        ARITHMETIC_ZH
    }
}
