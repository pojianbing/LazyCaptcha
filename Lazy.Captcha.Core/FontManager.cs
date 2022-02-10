using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class FontManager
    {
        private static List<Font> _fonts;

        static FontManager()
        {
            if (_fonts == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var names = assembly.GetManifestResourceNames();

                if (names?.Length > 0 == true)
                {
                    var fontList = new List<Font>();
                    var fontCollection = new FontCollection();

                    foreach (var name in names)
                    {
                        fontList.Add(new Font(fontCollection.Install(assembly.GetManifestResourceStream(name)), 32));
                    }

                    _fonts = fontList;
                }
                else
                {
                    throw new Exception($"绘制验证码字体文件加载失败");
                }
            }
        }

        public static Font GetFont()
        {
            var random = new Random();
            return _fonts[random.Next(_fonts.Count)];
        }
    }
}
