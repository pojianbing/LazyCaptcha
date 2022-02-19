using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SixLabors.Fonts;

namespace Lazy.Captcha.Core
{
    public class DefaultFontFamilys
    {
        public static DefaultFontFamilys Instance = new DefaultFontFamilys();
        private static List<FontFamily> _fontFamilies = null;
        private static Dictionary<string, string> FamilyNameMapper = new Dictionary<string, string>
        {
            { "actionj", "Action Jackson" },
            { "epilog", "Epilog" },
            { "fresnel", "Fresnel" },
            { "headache", "Tom's Headache" },
            { "lexo", "Lexographer" },
            { "prefix", "Prefix" },
            { "progbot", "PROG.BOT" },
            { "ransom", "Ransom" },
            { "robot", "Robot Teacher" },
            { "scandal", "Potassium Scandal" },
            { "kaiti", "FZKai-Z03" }
        };

        static DefaultFontFamilys()
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
        /// 获取字体
        /// </summary>
        public FontFamily GetFontFamily(string name)
        {
            var realName = "Epilog";
            var normalizeName = name.ToLowerInvariant();
            if (FamilyNameMapper.ContainsKey(normalizeName))
            {
                // 默认字体
                realName = FamilyNameMapper[normalizeName];
            }
            return _fontFamilies.First(f => f.Name == realName);
        }

        /// <summary>
        /// ACTIONJ
        /// </summary>
        public FontFamily Actionj
        {
            get
            {
                return GetFontFamily("Actionj");
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
                return GetFontFamily("Headache");
            }
        }

        /// <summary>
        /// Lexo
        /// </summary>
        public FontFamily Lexo
        {
            get
            {
                return GetFontFamily("Lexo");
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
                return GetFontFamily("Progbot");
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
                return GetFontFamily("Robot");
            }
        }

        /// <summary>
        /// Scandal
        /// </summary>
        public FontFamily Scandal
        {
            get
            {
                return GetFontFamily("Scandal");
            }
        }

        /// <summary>
        /// 楷体
        /// </summary>
        public FontFamily Kaiti
        {
            get
            {
                return GetFontFamily("Kaiti");
            }
        }
    }
}