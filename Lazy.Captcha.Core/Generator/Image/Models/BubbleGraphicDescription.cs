using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy.Captcha.Core.Generator.Image.Models
{
    public class BubbleGraphicDescription
    {
        public IPath Path { get; set; }
        public Color Color { get; set; }
        public float Thickness { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
