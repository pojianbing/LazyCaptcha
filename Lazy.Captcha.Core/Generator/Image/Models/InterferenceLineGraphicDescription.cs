using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core.Generator.Image.Models
{
    public class InterferenceLineGraphicDescription
    {
        public Color Color { get; set; }
        public PointF Start { get; set; }
        public PointF Ctrl1 { get; set; }
        public PointF Ctrl2 { get; set; }
        public PointF End { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
