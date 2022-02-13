using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class DefaultColors
    {
        public static DefaultColors Instance = new DefaultColors();

        private static int[,] COLORS = new int[,] { { 0, 135, 255 }, { 51, 153, 51 }, { 255, 102, 102 }, { 255, 153, 0 }, { 153, 102, 0 }, { 153, 102, 153 }, { 51, 153, 153 }, { 102, 102, 255 }, { 0, 102, 204 }, { 204, 51, 51 }, { 0, 153, 204 }, { 0, 51, 102 } };

        public Color RandomColor()
        {
            var random = new Random();
            var colorIndex = random.Next(COLORS.GetUpperBound(0));
            var r = (byte)COLORS[colorIndex, 0];
            var g = (byte)COLORS[colorIndex, 1];
            var b = (byte)COLORS[colorIndex, 2];
            return new Color(new SixLabors.ImageSharp.PixelFormats.Rgb24(r, g, b));
        }
    }
}
