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
        
        public List<Color> Colors = new List<Color>
        {
            Color.Parse("#0087ff"),
            Color.Parse("#339933"),
            Color.Parse("#ff6666"),
            Color.Parse("#ff9900"),
            Color.Parse("#996600"),
            Color.Parse("#996699"),
            Color.Parse("#339999"),
            Color.Parse("#6666ff"),
            Color.Parse("#0066cc"),
            Color.Parse("#cc3333"),
            Color.Parse("#0099cc"),
            Color.Parse("#003366"),
        };
    }
}
