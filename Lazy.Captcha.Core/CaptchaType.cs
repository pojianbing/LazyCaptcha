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
        /// 默认(英文字符大小写，数字混合)
        /// </summary>
        DEFAULT=0,

        /// <summary>
        /// 中文
        /// </summary>
        CHINESE=1,

        /// <summary>
        /// 数字
        /// </summary>
        NUMBER=2,

        /// <summary>
        /// 中文数字小写
        /// </summary>
        NUMBER_ZH_CN=3,

        /// <summary>
        /// 中文数字大写
        /// </summary>
        NUMBER_ZH_HK=4,

        /// <summary>
        /// 英文字符大小写混合
        /// </summary>
        WORD=5,

        /// <summary>
        /// 英文字符小写
        /// </summary>
        WORD_LOWER=6,

        /// <summary>
        /// 英文字符大写
        /// </summary>
        WORD_UPPER=7,

        /// <summary>
        /// 英文小写，数字混合
        /// </summary>
        WORD_NUMBER_LOWER=8,

        /// <summary>
        /// 英文大写，数字混合
        /// </summary>
        WORD_NUMBER_UPPER=9,

        /// <summary>
        /// 数字计算
        /// </summary>
        ARITHMETIC=10,

        /// <summary>
        /// 数字计算，中文
        /// </summary>
        ARITHMETIC_ZH=11
    }
}