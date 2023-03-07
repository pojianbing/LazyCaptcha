using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SkiaSharp;

namespace Lazy.Captcha.Core
{
    public class DefaultFontFamilys
    {
        public static DefaultFontFamilys Instance = new DefaultFontFamilys();
        private static List<SKTypeface> _fontFamilies = null;
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
                _fontFamilies = new List<SKTypeface>();

                if (names?.Length > 0 == true)
                {
                    foreach (var name in names)
                    {
                        _fontFamilies.Add(SKTypeface.FromStream(assembly.GetManifestResourceStream(name)));
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
        public SKTypeface GetFontFamily(string name)
        {
            var realName = "Epilog";
            var normalizeName = name.ToLowerInvariant();
            if (FamilyNameMapper.ContainsKey(normalizeName))
            {
                // 默认字体
                realName = FamilyNameMapper[normalizeName];
            }
            // 改用StartsWith, 某些环境下： Prefix取到的值为Prefix Endangered, Ransom取到的值为Ransom CutUpLetters
            return _fontFamilies.First(f => f.FamilyName.StartsWith(realName));
        }

        /// <summary>
        /// ACTIONJ
        /// </summary>
        public SKTypeface Actionj
        {
            get
            {
                return GetFontFamily("Actionj");
            }
        }

        /// <summary>
        /// Epilog
        /// </summary>
        public SKTypeface Epilog
        {
            get
            {
                return GetFontFamily("Epilog");
            }
        }

        /// <summary>
        /// Fresnel
        /// </summary>
        public SKTypeface Fresnel
        {
            get
            {
                return GetFontFamily("Fresnel");
            }
        }

        /// <summary>
        /// headache
        /// </summary>
        public SKTypeface Headache
        {
            get
            {
                return GetFontFamily("Headache");
            }
        }

        /// <summary>
        /// Lexo
        /// </summary>
        public SKTypeface Lexo
        {
            get
            {
                return GetFontFamily("Lexo");
            }
        }

        /// <summary>
        /// Prefix
        /// </summary>
        public SKTypeface Prefix
        {
            get
            {
                return GetFontFamily("Prefix");
            }
        }

        /// <summary>
        /// Progbot
        /// </summary>
        public SKTypeface Progbot
        {
            get
            {
                return GetFontFamily("Progbot");
            }
        }

        /// <summary>
        /// Ransom
        /// </summary>
        public SKTypeface Ransom
        {
            get
            {
                return GetFontFamily("Ransom");
            }
        }

        /// <summary>
        /// Robot
        /// </summary>
        public SKTypeface Robot
        {
            get
            {
                return GetFontFamily("Robot");
            }
        }

        /// <summary>
        /// Scandal
        /// </summary>
        public SKTypeface Scandal
        {
            get
            {
                return GetFontFamily("Scandal");
            }
        }

        /// <summary>
        /// 楷体
        /// </summary>
        public SKTypeface Kaiti
        {
            get
            {
                return GetFontFamily("Kaiti");
            }
        }
    }
}