using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Storage;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core
{
    /// <summary>
    /// 验证服务构建器
    /// </summary>
    public class CaptchaServiceBuilder
    {
        private  CaptchaOptions CaptchaOptions;
        private  IStorage InnerStorage;

        public  CaptchaServiceBuilder()
        {
            CaptchaOptions = GenerateDefaultOptions();
            InnerStorage = new MemeoryStorage();
        }

        public static CaptchaServiceBuilder New()
        {
            return new CaptchaServiceBuilder();
        }

        /// <summary>
        /// 默认选项
        /// </summary>
        /// <returns></returns>
        private CaptchaOptions GenerateDefaultOptions()
        {
            var options = new CaptchaOptions();
            options.ImageOption.Width = 98;
            options.ImageOption.Height = 35;
            options.ImageOption.ForegroundColors = DefaultColors.Instance.Colors;
            return options;
        }

        /// <summary>
        /// 设定存储
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Storage(IStorage storage)
        {
            InnerStorage = storage;
            return this;
        }

        /// <summary>
        /// 验证码类型
        /// </summary>
        /// <param name="captchaType"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CaptchaType(CaptchaType captchaType)
        {
            CaptchaOptions.CaptchaType = captchaType;
            return this;
        }

        /// <summary>
        /// 验证码长度 当CaptchaType为ARITHMETIC, ARITHMETIC_ZH时， 长度代表乘数个数
        /// </summary>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CodeLength(int codeLength)
        {
            CaptchaOptions.CodeLength = codeLength;
            return this;
        }

        /// <summary>
        /// 过期时长
        /// </summary>
        /// <param name="expirySeconds"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder ExpirySeconds(int expirySeconds)
        {
            CaptchaOptions.ExpirySeconds = expirySeconds;
            return this;
        }

        /// <summary>
        /// code比较是否忽略大小写
        /// </summary>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CodeLength(bool ignoreCase)
        {
            CaptchaOptions.IgnoreCase = ignoreCase;
            return this;
        }

        /// <summary>
        /// 存储键前缀
        /// </summary>
        /// <param name="storeageKeyPrefix"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder StoreageKeyPrefix(string storeageKeyPrefix)
        {
            CaptchaOptions.StoreageKeyPrefix = storeageKeyPrefix;
            return this;
        }

        /// <summary>
        /// 是否启用动画
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Animation(bool animation)
        {
            CaptchaOptions.ImageOption.Animation = animation;
            return this;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BackgroundColor(SKColor backgroundColor)
        {
            CaptchaOptions.ImageOption.BackgroundColor = backgroundColor;
            return this;
        }

        /// <summary>
        /// 前景色
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder ForegroundColors(List<SKColor> foregroundColors)
        {
            CaptchaOptions.ImageOption.ForegroundColors = foregroundColors;
            return this;
        }

        /// <summary>
        /// FontFamily
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FontFamily(SKTypeface fontFamily)
        {
            CaptchaOptions.ImageOption.FontFamily = fontFamily;
            return this;
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FontSize(float fontSize)
        {
            CaptchaOptions.ImageOption.FontSize = fontSize;
            return this;
        }

        /// <summary>
        /// 验证码的宽
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Width(int width)
        {
            CaptchaOptions.ImageOption.Width = width;
            return this;
        }

        /// <summary>
        /// 验证码高
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Height(int height)
        {
            CaptchaOptions.ImageOption.Height = height;
            return this;
        }

        /// <summary>
        /// 气泡最小半径
        /// </summary>
        /// <param name="bubbleMinRadius"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleMinRadius(int bubbleMinRadius)
        {
            CaptchaOptions.ImageOption.BubbleMinRadius = bubbleMinRadius;
            return this;
        }

        /// <summary>
        /// 气泡最小半径
        /// </summary>
        /// <param name="bubbleMaxRadius"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleMaxRadius(int bubbleMaxRadius)
        {
            CaptchaOptions.ImageOption.BubbleMaxRadius = bubbleMaxRadius;
            return this;
        }

        /// <summary>
        /// 气泡数量
        /// </summary>
        /// <param name="bubbleCount"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleCount(int bubbleCount)
        {
            CaptchaOptions.ImageOption.BubbleCount = bubbleCount;
            return this;
        }

        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        /// <param name="bubbleThickness"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleThickness(float bubbleThickness)
        {
            CaptchaOptions.ImageOption.BubbleThickness = bubbleThickness;
            return this;
        }

        /// <summary>
        /// 干扰线数量
        /// </summary>
        /// <param name="interferenceLineCount"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder InterferenceLineCount(int interferenceLineCount)
        {
            CaptchaOptions.ImageOption.InterferenceLineCount = interferenceLineCount;
            return this;
        }

        /// <summary>
        /// 每帧延迟,Animation=true时有效
        /// </summary>
        /// <param name="frameDelay"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FrameDelay(int frameDelay)
        {
            CaptchaOptions.ImageOption.FrameDelay = frameDelay;
            return this;
        }

        /// <summary>
        /// 图片质量
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Quality(int quality)
        {
            CaptchaOptions.ImageOption.Quality = quality;
            return this;
        }

        /// <summary>
        /// 文本粗体
        /// </summary>
        /// <param name="textBold"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder TextBold(bool textBold)
        {
            CaptchaOptions.ImageOption.TextBold = textBold;
            return this;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public CaptchaService Build()
        {
            return new CaptchaService(CaptchaOptions, InnerStorage);
        }
    }
}
