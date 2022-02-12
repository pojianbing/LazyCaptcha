using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Image.Option
{
    public class CaptchaImageGeneratorOption
    {
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        /// <summary>
        /// FontFamily
        /// </summary>
        public FontFamily FontFamily { get; set;}
        /// <summary>
        /// FontStyle
        /// </summary>
        public FontStyle FontStyle { get; set;} = FontStyle.Regular;
        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; } = 28;

        /// <summary>
        /// 验证码的宽
        /// </summary>
        public int Width { get; set; } = 130;
        /// <summary>
        /// 验证码高
        /// </summary>
        public int Height { get; set; } = 48;
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; } = 4;
        /// <summary>
        /// 是否绘制气泡
        /// </summary>
        public bool DrawBubble { get; set; } = true;
        /// <summary>
        /// 气泡数量
        /// </summary>
        public int BubbleCount { get; set; } = 3;
        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        public float BubbleThickness { get; set; } = 1;
        /// <summary>
        /// 是否绘制干扰线
        /// </summary>
        public bool DrawInterferenceLine { get; set; } = true;
        /// <summary>
        /// 干扰线数量
        /// </summary>
        public int InterferenceLineCount { get; set; } = 1;

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font
        {
            get
            {
                if (this.FontFamily == null)
                {
                    var fontFamily = this.IsChineseIn() ? DefaultFonts.instance.Kaiti : DefaultFonts.instance.Epilog;
                    return new Font(fontFamily, this.FontSize, this.FontStyle);
                }
                else
                {
                    var fontFamily = this.IsChineseIn() ? DefaultFonts.instance.Kaiti : DefaultFonts.instance.Epilog;
                    return new Font(this.FontFamily, this.FontSize, this.FontStyle);
                }
            }
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <returns></returns>
        private bool IsChineseIn()
        {
            //return this.CaptchaType == CaptchaType.CHINESE ||
            //       this.CaptchaType == CaptchaType.NUMBER_ZH_CN ||
            //       this.CaptchaType == CaptchaType.NUMBER_ZH_HK ||
            //       this.CaptchaType == CaptchaType.NUMBER_ZH_HK;
            return false;
        }
    }
}
