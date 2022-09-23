using SixLabors.Fonts;
using System.Reflection;

namespace Sample.NetCore
{
    public class ResourceFontFamilysFinder
    {
        private static Lazy<List<FontFamily>> _fontFamilies = new Lazy<List<FontFamily>>(() =>
        {
            var fontFamilies = new List<FontFamily>();
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();

            if (names?.Length > 0 == true)
            {
                var fontCollection = new FontCollection();
                foreach (var name in names)
                {
                    if (!name.EndsWith("ttf")) continue;
                    fontFamilies.Add(fontCollection.Add(assembly.GetManifestResourceStream(name)));
                }
            }

            return fontFamilies;
        });


        public static FontFamily Find(string name)
        {
            return _fontFamilies.Value.First(e => e.Name == name);
        }
    }
}
