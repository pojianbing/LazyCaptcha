using Lazy.Captcha.Core;
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
            int width = 130;
            int height = 48;

            var random = new Random();

            using Image<Rgba32> img = new(width, height, Color.White);

            for (int i = 0; i < 3; i++)
            {
                //g.setColor(color == null ? color() : color);
                //int w = 5 + num(10);
                //g.drawOval(num(width - 25), num(height - 15), w, w);
                img.Mutate(ctx =>
                {
                    ctx.SetGraphicsOptions(new GraphicsOptions()
                    {
                        Antialias = true
                    });

                    var color = ColorManager.GetRandomColor();
                    var w = 5 + random.Next(10);
                    var point = new PointF(random.Next(width - 25) + w, random.Next(height - 15) + w);
                    var size = new SizeF(w, w);
                    var circle = new EllipsePolygon(point, size);

                    ctx.Draw(color, 1, circle.Clip());
                });
            }

            img.Save(DateTime.Now.Ticks + ".png");
        }
    }
}
