using Lazy.Captcha.Core;
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
    public class Chinese
    {

        public static void Run()
        {
            int width = 130;
            int height = 48;

            using Image<Rgba32> img = new(width, height, Color.White);

            img.Mutate(ctx =>
            {
                ctx.SetGraphicsOptions(new GraphicsOptions()
                {
                    Antialias = true
                });

                // 绘制气泡
                //DrawBubble(ctx, width, height, 3);
                //// 绘制干扰线
                //DrawInterferenceLine(ctx, width, height);
                // 绘制文字
                DrawChinese(ctx, width, height, "a");
            });

            img.Save("a.png");
        }

        static void DrawBubble(IImageProcessingContext ctx, int width, int height, int count)
        {
            for (var i = 0; i < count; i++)
            {
                DrawBubble(ctx, width, height);
            }
        }

        static void DrawBubble(IImageProcessingContext ctx, int width, int height)
        {
            var random = new Random();
            var color = ColorManager.instance.GetRandomColor();
            var w = 5 + random.Next(10);
            var point = new PointF(random.Next(width - 25) + w, random.Next(height - 15) + w);
            var size = new SizeF(w, w);
            var circle = new EllipsePolygon(point, size);
            ctx.Draw(color, 1, circle.Clip());
        }

        static void DrawInterferenceLine(IImageProcessingContext ctx, int width, int height)
        {
            var random = new Random();
            var color = ColorManager.instance.GetRandomColor();
            int x1 = 5, y1 = random.Next(5, height / 2);
            int x2 = width - 5, y2 = random.Next(height / 2, height - 5);
            int ctrlx1 = random.Next(width / 4, width / 4 * 3), ctrly1 = random.Next(5, height - 5);
            int ctrlx2 = random.Next(width / 4, width / 4 * 3), ctrly2 = random.Next(5, height - 5);
            ctx.DrawBeziers(color, 1, new PointF(x1, y1), new PointF(ctrlx1, ctrly1), new PointF(ctrlx2, ctrly2), new PointF(x2, y2));
        }

        static void DrawChinese(IImageProcessingContext ctx, int width, int height, string text)
        {
            Font font = new Font(SystemFonts.Families.Last(), 38, FontStyle.Bold);

            int fW = width / text.Count(); // 每一个字符宽度
            int fSp = (int)(fW - TextMeasurer.Measure("王", new RendererOptions(font)).Width) / 2;
            for (var i = 0; i < text.Count(); i++)
            {
                var fontHeight = (int)TextMeasurer.Measure(text[i].ToString(), new RendererOptions(font)).Height;
                int fY = (height - fontHeight) / 2;  // 文字的纵坐标
                var color = ColorManager.instance.GetRandomColor();

                ctx.DrawText(text[i].ToString(), font, color, new PointF(i * fW + fSp + 3, fY + 3));
            }
        }
    }
}
