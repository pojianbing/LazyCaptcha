using SkiaSharp;
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
        public SKColor BackgroundColor { get; set; } = SKColors.White;
        /// <summary>
        /// 前景色
        /// </summary>
        public List<SKColor> ForegroundColors { get; set; }
        /// <summary>
        /// FontFamily
        /// </summary>
        public SKTypeface FontFamily { get; set;}= DefaultFontFamilys.Instance.Kaiti;
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
        public int BubbleMinRadius { get; set; } = 3;
        /// <summary>
        /// 气泡最小半径
        /// </summary>
        public int BubbleMaxRadius { get; set; } = 8;
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
        public int FrameDelay { get; set; } = 300;
        /// <summary>
        /// 图片质量（仅对静态验证有效）
        /// </summary>
        public int Quality { get; set; } = 100;
        /// <summary>
        /// 文本粗体
        /// </summary>
        public bool TextBold { get; set; } = false;
    }
}
