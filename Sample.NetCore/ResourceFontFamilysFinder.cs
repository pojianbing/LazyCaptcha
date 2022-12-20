using SkiaSharp;
using System.Reflection;

namespace Sample.NetCore
{
    public class ResourceFontFamilysFinder
    {
        private static Lazy<List<SKTypeface>> _fontFamilies = new Lazy<List<SKTypeface>>(() =>
        {
            var fontFamilies = new List<SKTypeface>();
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();

            if (names?.Length > 0 == true)
            {
                foreach (var name in names)
                {
                    if (!name.EndsWith("ttf")) continue;
                    fontFamilies.Add(SKTypeface.FromStream(assembly.GetManifestResourceStream(name)));
                }
            }

            return fontFamilies;
        });


        public static SKTypeface Find(string name)
        {
            return _fontFamilies.Value.First(e => e.FamilyName == name);
        }
    }
}
