using Lazy.Captcha.Core.Generator;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Winfrom.Models
{
    public class FontFamilyOption : Option<SKTypeface>
    {
        public FontFamilyOption(string text, SKTypeface value)
        {
            Text = text;
            Value = value;
        }
    }
}
