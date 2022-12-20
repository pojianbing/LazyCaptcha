using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core.Generator.Image.Models
{
    public class InterferenceLineGraphicDescription
    {
        public SKColor Color { get; set; }
        public SKPoint Start { get; set; }
        public SKPoint Ctrl1 { get; set; }
        public SKPoint Ctrl2 { get; set; }
        public SKPoint End { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
