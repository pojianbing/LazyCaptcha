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
        /// 是否启用动画
        /// </summary>
        public bool Animation { get; set; } = false;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        /// <summary>
        /// 前景色
        /// </summary>
        public List<Color> ForegroundColors { get; set; }
        /// <summary>
        /// FontFamily
        /// </summary>
        public FontFamily FontFamily { get; set;}= DefaultFontFamilys.Instance.Kaiti;
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
        /// 气泡最小半径
        /// </summary>
        public int BubbleMinRadius { get; set; } = 5;
        /// <summary>
        /// 气泡最小半径
        /// </summary>
        public int BubbleMaxRadius { get; set; } = 15;
        /// <summary>
        /// 气泡数量
        /// </summary>
        public int BubbleCount { get; set; } = 3;
        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        public float BubbleThickness { get; set; } = 1;
        /// <summary>
        /// 干扰线数量
        /// </summary>
        public int InterferenceLineCount { get; set; } = 1;
        /// <summary>
        /// 每帧延迟,Animation=true时有效
        /// </summary>
        public int FrameDelay { get; set; } = 30;

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font
        {
            get
            {
                return this.FontFamily.CreateFont(this.FontSize, this.FontStyle);
            }
        }
    }
}
