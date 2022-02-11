using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public class DefaultFonts
    {
        public static DefaultFonts instance = new DefaultFonts();
        private static List<FontFamily> _fontFamilies = null;

        static DefaultFonts()
        {
            if (_fontFamilies == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var names = assembly.GetManifestResourceNames();
                _fontFamilies = new List<FontFamily>();

                if (names?.Length > 0 == true)
                {
                    var fontList = new List<Font>();
                    var fontCollection = new FontCollection();

                    foreach (var name in names)
                    {
                        _fontFamilies.Add(fontCollection.Install(assembly.GetManifestResourceStream(name)));
                    }
                }
                else
                {
                    throw new Exception($"绘制验证码字体文件加载失败");
                }
            }
        }

        /// <summary>
        /// 随机获取字体
        /// </summary>
        /// <returns></returns>
        public FontFamily GetFontFamily(string name)
        {
            return _fontFamilies.First(f => f.Name == name);
        }

        /// <summary>
        /// ACTIONJ
        /// </summary>
        public FontFamily Actionj
        {
            get
            {
                return GetFontFamily("Action Jackson");
            }
        }

        /// <summary>
        /// Epilog
        /// </summary>
        public FontFamily Epilog
        {
            get
            {
                return GetFontFamily("Epilog");
            }
        }

        /// <summary>
        /// Fresnel
        /// </summary>
        public FontFamily Fresnel
        {
            get
            {
                return GetFontFamily("Fresnel");
            }
        }

        /// <summary>
        /// headache
        /// </summary>
        public FontFamily Headache
        {
            get
            {
                return GetFontFamily("Tom's Headache");
            }
        }

        /// <summary>
        /// Lexo
        /// </summary>
        public FontFamily Lexo
        {
            get
            {
                return GetFontFamily("Lexographer");
            }
        }

        /// <summary>
        /// Prefix
        /// </summary>
        public FontFamily Prefix
        {
            get
            {
                return GetFontFamily("Prefix");
            }
        }

        /// <summary>
        /// Progbot
        /// </summary>
        public FontFamily Progbot
        {
            get
            {
                return GetFontFamily("PROG.BOT");
            }
        }

        /// <summary>
        /// Ransom
        /// </summary>
        public FontFamily Ransom
        {
            get
            {
                return GetFontFamily("Ransom");
            }
        }

        /// <summary>
        /// Robot
        /// </summary>
        public FontFamily Robot
        {
            get
            {
                return GetFontFamily("Robot Teacher");
            }
        }

        /// <summary>
        /// Scandal
        /// </summary>
        public FontFamily Scandal
        {
            get
            {
                return GetFontFamily("Potassium Scandal");
            }
        }

        /// <summary>
        /// 楷体
        /// </summary>
        public FontFamily Kaiti
        {
            get
            {
                return GetFontFamily("FZKai-Z03");
            }
        }
    }
}
