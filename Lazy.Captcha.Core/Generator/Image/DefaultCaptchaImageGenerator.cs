using Lazy.Captcha.Core.Generator.Image.Option;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Image
{
    /// <summary>
    /// 验证码生成器基类
    /// </summary>
    public class DefaultCaptchaImageGenerator : ICaptchaImageGenerator
    {
        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        /// <param name="count">气泡数量</param>
        /// <param name="minRadius">气泡最小半径</param>
        /// <param name="maxRadius">气泡最大半径</param>
        /// <param name="thickness">气泡边厚度</param>
        protected virtual void DrawBubble(IImageProcessingContext ctx, int width, int height, int count, int minRadius, int maxRadius, float thickness = 1)
        {
            for (var i = 0; i < count; i++)
            {
                DrawBubble(ctx, width, height, minRadius, maxRadius, thickness);
            }
        }

        /// <summary>
        /// 绘制单个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        /// <param name="minRadius">气泡最小半径</param>
        /// <param name="maxRadius">气泡最大半径</param>
        /// <param name="thickness">气泡边厚度</param>
        protected virtual void DrawBubble(IImageProcessingContext ctx, int width, int height, int minRadius, int maxRadius, float thickness = 1)
        {
            var random = new Random();
            var color = DefaultColors.instance.GetRandomColor();
            var w = minRadius + random.Next(maxRadius - 10);
            var point = new PointF(random.Next(width - 25) + w, random.Next(height - 15) + w);
            var size = new SizeF(w, w);
            var circle = new EllipsePolygon(point, size);
            ctx.Draw(color, thickness, circle.Clip());
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        protected virtual void DrawInterferenceLine(IImageProcessingContext ctx, int width, int height, int count)
        {
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                var color = DefaultColors.instance.GetRandomColor();
                int x1 = 5, y1 = random.Next(height / 2, height - 5);
                int x2 = width - 5, y2 = random.Next(5, height / 2);
                int ctrlx1 = random.Next(width / 4, width / 4 * 3), ctrly1 = random.Next(5, height - 5);
                int ctrlx2 = random.Next(width / 4, width / 4 * 3), ctrly2 = random.Next(5, height - 5);
                ctx.DrawBeziers(color, 1, new PointF(x1, y1), new PointF(ctrlx1, ctrly1), new PointF(ctrlx2, ctrly2), new PointF(x2, y2));
            }
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        /// <param name="text"></param>
        protected virtual void DrawText(IImageProcessingContext ctx, int width, int height, string text, Font font)
        {
            var textPositions = MeasureTextPositions(width, height, text, font);

            for (var i = 0; i < text.Count(); i++)
            {
                var color = DefaultColors.instance.GetRandomColor();
                ctx.DrawText(text[i].ToString(), font, color, textPositions[i]);
            }
        }

        /// <summary>
        /// 测算文本绘制位置
        /// </summary>
        /// <param name="width">验证码宽度</param>
        /// <param name="height">验证码高度</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="font">文本所应用的字体</param>
        /// <returns>返回每个字符的位置</returns>
        public virtual List<PointF> MeasureTextPositions(int width, int height, string text, Font font)
        {
            var result = new List<PointF>();
            if (string.IsNullOrWhiteSpace(text)) return result;

            // 计算每个字符宽度
            var charWidths = new List<float>();
            foreach (var s in text)
            {
                var charWidth = TextMeasurer.Measure(s.ToString(), new RendererOptions(font)).Width;
                charWidths.Add(charWidth);
            }

            // 计算每个字符x坐标
            var charTotalWidth = charWidths.Sum(x => x);
            var wrapperX = 0.0f;
            var charXs = new List<float>();
            for (var i = 0; i < text.Count(); i++)
            {
                var wrapperWidth = charWidths[i] * 1.0f / charTotalWidth * width;
                var padding = (wrapperWidth - charWidths[i]) / 2;
                var fX = wrapperX + padding;

                var fontHeight = (int)TextMeasurer.Measure(text[i].ToString(), new RendererOptions(font)).Height;
                int fY = (height - fontHeight) / 2 + 3;  // 文字的纵坐标
                result.Add(new PointF(fX, fY));

                wrapperX += wrapperWidth;
            }

            return result;
        }


        public virtual byte[] Generate(string text, CaptchaImageGeneratorOption option)
        {
            using Image<Rgba32> img = new(option.Width, option.Height, option.BackgroundColor);
            img.Mutate(ctx =>
            {
                // 绘制气泡
                if (option.DrawBubble)
                {
                    DrawBubble(ctx, option.Width, option.Height, option.BubbleCount, option.BubbleMinRadius, option.BubbleMaxRadius, option.BubbleThickness);
                }

                // 绘制干扰线
                if (option.DrawInterferenceLine)
                {
                    DrawInterferenceLine(ctx, option.Width, option.Height, option.InterferenceLineCount);
                }

                // 绘制文字
                DrawText(ctx, option.Width, option.Height, text, option.Font);
            });

            using (var ms = new MemoryStream())
            {
                img.Save(ms, PngFormat.Instance);
                return ms.ToArray();
            }
        }
    }
}
