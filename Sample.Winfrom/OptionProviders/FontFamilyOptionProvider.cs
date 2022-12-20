using Lazy.Captcha.Core;
using Sample.Winfrom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Winfrom.OptionProviders
{
    public class FontFamilyOptionProvider
    {
        public static List<FontFamilyOption> Provide()
        {
            return new List<FontFamilyOption>
            {
                new FontFamilyOption("Actionj", DefaultFontFamilys.Instance.Actionj),
                new FontFamilyOption("Kaiti", DefaultFontFamilys.Instance.Kaiti),
                new FontFamilyOption("Fresnel", DefaultFontFamilys.Instance.Fresnel),
                new FontFamilyOption("Prefix", DefaultFontFamilys.Instance.Prefix),
                new FontFamilyOption("Ransom", DefaultFontFamilys.Instance.Ransom),
                new FontFamilyOption("Scandal", DefaultFontFamilys.Instance.Scandal),
                new FontFamilyOption("Epilog", DefaultFontFamilys.Instance.Epilog),
                new FontFamilyOption("Headache", DefaultFontFamilys.Instance.Headache),
                new FontFamilyOption("Lexo", DefaultFontFamilys.Instance.Lexo),
                new FontFamilyOption("Progbot", DefaultFontFamilys.Instance.Progbot),
                new FontFamilyOption("Robot", DefaultFontFamilys.Instance.Robot),
            };
        }
    }
}
