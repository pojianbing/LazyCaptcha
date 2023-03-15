
using Lazy.Captcha.Core;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Winfrom.Models
{
    public class CaptchaImageGeneratorOptionJsonModel
    {
        /// <summary>
        /// 是否启用动画
        /// </summary>
        public bool Animation { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public string BackgroundColor { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public string ForegroundColors { get; set; }
        /// <summary>
        /// FontFamily
        /// </summary>
        public string FontFamily { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// 验证码的宽
        /// </summary>
        public int Width { get; set; } 
        /// <summary>
        /// 验证码高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 气泡最小半径
        /// </summary>
        public int BubbleMinRadius { get; set; }
        /// <summary>
        /// 气泡最小半径
        /// </summary>
        public int BubbleMaxRadius { get; set; }
        /// <summary>
        /// 气泡数量
        /// </summary>
        public int BubbleCount { get; set; }
        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        public float BubbleThickness { get; set; }
        /// <summary>
        /// 干扰线数量
        /// </summary>
        public int InterferenceLineCount { get; set; } 
        /// <summary>
        /// 每帧延迟,Animation=true时有效
        /// </summary>
        public int FrameDelay { get; set; }
        /// <summary>
        /// 图片质量（影响生成图片的大小）
        /// </summary>
        public int Quality { get; set; } = 100;
        /// <summary>
        /// 文本粗体
        /// </summary>
        public bool TextBold { get; set; } = false;
    }
}
