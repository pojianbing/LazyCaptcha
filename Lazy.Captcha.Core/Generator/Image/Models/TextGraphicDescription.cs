using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lazy.Captcha.Core.Generator.Image.Models
{
    public class TextGraphicDescription
    {
        public string Text { get; set; }
        public SKTypeface Font { get; set; }
        public SKColor Color { get; set; }
        public PointF Location { get; set; }
        public float FontSize { get; set; }
        public float BlendPercentage { get; set; } = 1;
        public bool TextBold { get; set; }
    }
}
