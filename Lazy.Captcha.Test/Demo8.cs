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
    public class Demo8
    {
        public static void Run()
        {
            System.IO.Directory.CreateDirectory("output");

            using (var image = Image.Load("inputs/fb.jpg"))
            {
                var outerRadii = Math.Min(image.Width, image.Height) / 2;
                var star = new Star(new PointF(image.Width / 2, image.Height / 2), 5, outerRadii / 2, outerRadii);

                image.Mutate(x => x.DrawPolygon(Pens.Solid(Color.White, 2), star.Points.ToArray()));
                image.Mutate(x => x.FillPolygon(Color.ParseHex("#00000088"), star.Points.ToArray()));
                image.Save("cc2.png");

                //using (var clone = image.Clone(p =>
                //{
                //    p.GaussianBlur(15);
                //}))
                //{
                //    //  裁剪区域
                //    var rect = new RectangularPolygon(-0.0f, -0.0f, 200, 200);
                //    clone.Mutate(c => c.Crop((Rectangle)star.Bounds));

                //    clone.Save("cc1.png");

                //    var brush = new ImageBrush(clone);

                //    // now fill the shape with the image brush containing the portion of 
                //    // cloned image with the effects applied
                //    image.Mutate(c => c.Fill(brush, star));

                //    var options = new DrawingOptions()
                //    {
                //        GraphicsOptions = new GraphicsOptions
                //        {
                //            BlendPercentage = 0.5f
                //        }
                //    };
                //    image.Mutate(x => x.Draw(options, Color.White, 1, star));
                //}

                //DrawingOptions options = new()
                //{
                //    GraphicsOptions = new()
                //    {
                //        ColorBlendingMode = PixelColorBlendingMode.Multiply,
                //    }
                //};

                //IBrush brush = Brushes.Horizontal(Color.Red, Color.Blue);
                //IPen pen = Pens.DashDot(Color.Green, 5);
                //IPath yourPolygon = new Star(x: 100.0f, y: 100.0f, prongs: 5, innerRadii: 20.0f, outerRadii: 30.0f);

                //// Draws a star with horizontal red and blue hatching with a dash dot pattern outline.
                //image.Mutate(x => x.Fill(options, brush, yourPolygon)
                //                   .Draw(options, pen, yourPolygon));

                //image.Save("cc.png");
            }
        }
    }
}
