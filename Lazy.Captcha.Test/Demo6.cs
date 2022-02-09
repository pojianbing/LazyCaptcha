using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Test
{
    public class Demo6
    {
        public static void Run()
        {
            System.IO.Directory.CreateDirectory("output");
            using (Image img = new Image<Rgba32>(1500, 500))
            {
                PathBuilder pathBuilder = new PathBuilder();
                pathBuilder.SetOrigin(new PointF(500, 0));
                pathBuilder.AddBezier(new PointF(50, 450), new PointF(200, 50), new PointF(300, 50), new PointF(450, 450));
                // add more complex paths and shapes here.

                IPath path = pathBuilder.Build();

                // For production application we would recomend you create a FontCollection
                // singleton and manually install the ttf fonts yourself as using SystemFonts
                // can be expensive and you risk font existing or not existing on a deployment
                // by deployment basis.
                var font = SystemFonts.CreateFont("Arial", 39, FontStyle.Regular);

                string text = "Hello World Hello World Hello World Hello World Hello World";
                var textGraphicsOptions = new DrawingOptions() // draw the text along the path wrapping at the end of the line
                {
                    TextOptions = 
                    {
                        WrapTextWidth = path.Bounds.Width
                    }
                };

                // lets generate the text as a set of vectors drawn along the path
                var glyphs = TextBuilder.GenerateGlyphs(text, path, new RendererOptions(font, textGraphicsOptions.TextOptions.DpiX, textGraphicsOptions.TextOptions.DpiY)
                {
                    HorizontalAlignment = textGraphicsOptions.TextOptions.HorizontalAlignment,
                    TabWidth = textGraphicsOptions.TextOptions.TabWidth,
                    VerticalAlignment = textGraphicsOptions.TextOptions.VerticalAlignment,
                    WrappingWidth = textGraphicsOptions.TextOptions.WrapTextWidth,
                    ApplyKerning = textGraphicsOptions.TextOptions.ApplyKerning
                });

                img.Mutate(ctx => ctx
                    .Fill(Color.White) // white background image
                    .Draw(Color.Gray, 3, path) // draw the path so we can see what the text is supposed to be following
                    .Fill(Color.Black, glyphs));

                img.Save("output/wordart.png");
            }
        }
    }
}
