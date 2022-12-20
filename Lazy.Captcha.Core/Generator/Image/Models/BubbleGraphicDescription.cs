using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lazy.Captcha.Core.Generator.Image.Models
{
    public class BubbleGraphicDescription
    {
        public float Cx { get; set; }
        public float Cy { get; set; }
        public float Radius { get; set; }
        public SKColor Color { get; set; }
        public float Thickness { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
