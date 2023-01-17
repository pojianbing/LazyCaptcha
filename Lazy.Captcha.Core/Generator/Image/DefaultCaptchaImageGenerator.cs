using Lazy.Captcha.Core.Generator.Image.Gif;
using Lazy.Captcha.Core.Generator.Image.Models;
using Lazy.Captcha.Core.Generator.Image.Option;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Image
{
    /// <summary>
    /// 验证码生成器基类
    /// </summary>
    public class DefaultCaptchaImageGenerator : ICaptchaImageGenerator
    {
        private static Random Random = new Random();

        private SKColor RandomColor(IEnumerable<SKColor> foregroundColors)
        {
            var index = Random.Next(foregroundColors.Count());
            return foregroundColors.ElementAt(index);
        }

        /// <summary>
        /// 生成气泡图形描述
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="count"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        protected virtual List<BubbleGraphicDescription> GenerateBubbleGraphicDescriptions(int width, int height, int count, int minRadius, int maxRadius, float thickness, IEnumerable<SKColor> foregroundColors)
        {
            var result = new List<BubbleGraphicDescription>();

            for (var i = 0; i < count; i++)
            {
                var radius = Random.Next(minRadius, maxRadius + 1);
                var cx = Random.Next(width - 25) + radius;
                var cy = Random.Next(height - 15) + radius;
                result.Add(new BubbleGraphicDescription
                {
                    Cx = cx,
                    Cy = cy,
                    Radius = radius,
                    Thickness = thickness,
                    Color = RandomColor(foregroundColors)
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="canvas">canvas</param>
        /// <param name="bubbleGraphicDescriptions">气泡图形描述</param>
        protected virtual void DrawBubbles(SKCanvas canvas, List<BubbleGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                using (var paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    paint.StrokeWidth = gd.Thickness;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = gd.Color.WithAlpha((byte)(255 * gd.BlendPercentage));
                    canvas.DrawCircle(gd.Cx, gd.Cy, gd.Radius, paint);
                }
            });
        }

        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="canvas">canvas</param>
        /// <param name="option">选项</param>
        protected virtual void DrawBubbles(SKCanvas canvas, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateBubbleGraphicDescriptions(option.Width, option.Height, option.BubbleCount, option.BubbleMinRadius, option.BubbleMaxRadius, option.BubbleThickness, option.ForegroundColors);
            DrawBubbles(canvas, graphicDescriptions);
        }

        /// <summary>
        /// 生成干扰线图形描述
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="count">数量</param>
        /// <returns>干扰线图形描述</returns>
        protected virtual List<InterferenceLineGraphicDescription> GenerateInterferenceLineGraphicDescriptions(int width, int height, int count, IEnumerable<SKColor> foregroundColors)
        {
            var result = new List<InterferenceLineGraphicDescription>();

            for (var i = 0; i < count; i++)
            {
                bool leftInBottom = Random.Next(2) == 0;
                int x1 = 5, y1 = leftInBottom ? Random.Next(height / 2, height - 5) : Random.Next(5, height / 2);
                int x2 = width - 5, y2 = leftInBottom ? Random.Next(5, height / 2) : Random.Next(height / 2, height - 5);
                int ctrlx1 = Random.Next(width / 4, width / 4 * 3), ctrly1 = Random.Next(5, height - 5);
                int ctrlx2 = Random.Next(width / 4, width / 4 * 3), ctrly2 = Random.Next(5, height - 5);
                result.Add(new InterferenceLineGraphicDescription
                {
                    Color = RandomColor(foregroundColors),
                    Start = new SKPoint(x1, y1),
                    Ctrl1 = new SKPoint(ctrlx1, ctrly1),
                    Ctrl2 = new SKPoint(ctrlx2, ctrly2),
                    End = new SKPoint(x2, y2)
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="canvas">canvas</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        protected virtual void DrawInterferenceLines(SKCanvas canvas, List<InterferenceLineGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                using (var paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    paint.StrokeWidth = 1;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = gd.Color.WithAlpha((byte)(255 * gd.BlendPercentage));

                    using (SKPath path = new SKPath())
                    {
                        path.MoveTo(gd.Start);
                        path.CubicTo(gd.Ctrl1, gd.Ctrl2, gd.End);
                        canvas.DrawPath(path, paint);
                    }
                }
            });
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="canvas">canvas</param>
        /// <param name="width">option</param>
        protected virtual void DrawInterferenceLines(SKCanvas canvas, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateInterferenceLineGraphicDescriptions(option.Width, option.Height, option.InterferenceLineCount, option.ForegroundColors);
            DrawInterferenceLines(canvas, graphicDescriptions);
        }

        /// <summary>
        /// 生成干扰线图形描述
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>文本图形描述</returns>
        protected virtual List<TextGraphicDescription> GenerateTextGraphicDescriptions(int width, int height, string text, SKTypeface font, float fontSize, IEnumerable<SKColor> foregroundColors)
        {
            var result = new List<TextGraphicDescription>();
            var textPositions = MeasureTextPositions(width, height, text, font, fontSize);

            for (var i = 0; i < text.Count(); i++)
            {
                result.Add(new TextGraphicDescription
                {
                    Text = text[i].ToString(),
                    Font = font,
                    Color = RandomColor(foregroundColors),
                    Location = textPositions[i],
                    FontSize = fontSize
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="graphicDescriptions">图形描述</param>
        protected virtual void DrawTexts(SKCanvas canvas, List<TextGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                using (var paint = new SKPaint())
                {
                    paint.StrokeWidth = 1;
                    paint.TextSize = gd.FontSize;
                    paint.IsAntialias = true;
                    paint.Typeface = gd.Font;
                    paint.Color = gd.Color.WithAlpha((byte)(255 * gd.BlendPercentage));
                    canvas.DrawText(gd.Text, gd.Location.X, gd.Location.Y, paint);
                }
            });
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="text"></param>
        /// <param name="option"></param>
        protected virtual void DrawTexts(SKCanvas canvas, string text, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateTextGraphicDescriptions(option.Width, option.Height, text, option.FontFamily, option.FontSize, option.ForegroundColors);
            DrawTexts(canvas, graphicDescriptions);
        }

        /// <summary>
        /// 测算文本绘制位置
        /// </summary>
        /// <param name="width">验证码宽度</param>
        /// <param name="height">验证码高度</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="paint">画笔</param>
        /// <returns>返回每个字符的位置</returns>
        public virtual List<PointF> MeasureTextPositions(int width, int height, string text, SKTypeface font, float fontSize)
        {
            using (var paint = new SKPaint())
            {
                paint.StrokeWidth = 1;
                paint.TextSize = fontSize;
                paint.IsAntialias = true;
                paint.Typeface = font;

                var result = new List<PointF>();
                if (string.IsNullOrWhiteSpace(text)) return result;

                // 计算每个字符宽度
                var charWidths = new List<float>();
                foreach (var s in text)
                {
                    var charWidth = paint.MeasureText(s.ToString());
                    charWidths.Add(charWidth);
                }

                // 计算每个字符x坐标
                var currentX = 0.0f;
                var charTotalWidth = charWidths.Sum(x => x);
                var charXs = new List<float>();

                // 计算字体高度（取最高的）
                SKRect textBounds = new SKRect();
                paint.MeasureText(text, ref textBounds);
                var fontHeight = (int)textBounds.Height;

                for (var i = 0; i < text.Count(); i++)
                {
                    // 根据文字宽度比例，计算文字包含框宽度
                    var wrapperWidth = charWidths[i] * 1.0f / charTotalWidth * width;
                    // 文字在包含框内的padding
                    var padding = (wrapperWidth - charWidths[i]) / 2;
                    var textX = currentX + padding;
                    int textY = (height + fontHeight) / 2;  // 文字的纵坐标
                    result.Add(new PointF(textX, textY));
                    currentX += wrapperWidth;
                }

                return result;
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="text"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual byte[] Generate(string text, CaptchaImageGeneratorOption option)
        {
            if (option.Animation)
            {
                return GenerateAnimation(text, option);
            }
            else
            {
                return GenerateNormal(text, option);
            }
        }

        private byte[] GenerateNormal(string text, CaptchaImageGeneratorOption option)
        {
            using (var bitmap = new SKBitmap(option.Width, option.Height, false))
            {
                using (var canvas = new SKCanvas(bitmap))
                {
                    // 绘制背景色
                    canvas.DrawColor(option.BackgroundColor);
                    // 绘制气泡
                    DrawBubbles(canvas, option);
                    // 绘制干扰线
                    DrawInterferenceLines(canvas, option);
                    // 绘制文字
                    DrawTexts(canvas, text, option);

                    using (SKData p = bitmap.Encode(SKEncodedImageFormat.Jpeg, option.Quality))
                    {
                        return p.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 计算透明度
        /// </summary>
        /// <param name="frameIndex">帧索引</param>
        /// <param name="textIndex">文字索引</param>
        /// <param name="len">验证码长度</param>
        /// <returns>文字的透明度</returns>
        private float GenerateBlendPercentage(int frameIndex, int textIndex, int len)
        {
            int num = frameIndex + textIndex;
            float r = (float)1 / (len - 1);
            float s = len * r;
            return num >= len ? (num * r - s) : num * r;
        }

        /// <summary>
        /// 生成动图
        /// </summary>
        /// <param name="text"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        private byte[] GenerateAnimation(string text, CaptchaImageGeneratorOption option)
        {
            var texGraphicDescriptions = GenerateTextGraphicDescriptions(option.Width, option.Height, text, option.FontFamily, option.FontSize, option.ForegroundColors);
            var bubbleGraphicDescriptions = option.BubbleCount != 0 ?
                GenerateBubbleGraphicDescriptions(option.Width, option.Height, option.BubbleCount, option.BubbleMinRadius, option.BubbleMaxRadius, option.BubbleThickness, option.ForegroundColors)
                :
                new List<BubbleGraphicDescription>();
            var interferenceLineGraphicDescriptions = option.BubbleCount != 0 ?
                GenerateInterferenceLineGraphicDescriptions(option.Width, option.Height, option.InterferenceLineCount, option.ForegroundColors)
                :
                new List<InterferenceLineGraphicDescription>();

            AnimatedGifEncoder gifEncoder = new AnimatedGifEncoder();
            gifEncoder.Start();
            gifEncoder.SetDelay(option.FrameDelay);
            //-1:no repeat,0:always repeat
            gifEncoder.SetRepeat(0);

            for (var i = 0; i < text.Length; i++)
            {
                // 调整透明度
                for (var j = 0; j < texGraphicDescriptions.Count; j++)
                {
                    texGraphicDescriptions[j].BlendPercentage = GenerateBlendPercentage(i, j, text.Length);
                }
                for (var j = 0; j < interferenceLineGraphicDescriptions.Count; j++)
                {
                    interferenceLineGraphicDescriptions[j].BlendPercentage = 0.7f;
                }
                for (var j = 0; j < bubbleGraphicDescriptions.Count; j++)
                {
                    bubbleGraphicDescriptions[j].BlendPercentage = Random.Next(10) * 0.1f;
                }

                using (var bitmap = new SKBitmap(option.Width, option.Height, false))
                {
                    using (var canvas = new SKCanvas(bitmap))
                    {
                        // 绘制背景色
                        canvas.DrawColor(option.BackgroundColor);
                        // 绘制气泡
                        DrawBubbles(canvas, bubbleGraphicDescriptions);
                        // 绘制干扰线
                        DrawInterferenceLines(canvas, interferenceLineGraphicDescriptions);
                        // 绘制文字
                        DrawTexts(canvas, texGraphicDescriptions);

                        using (SKData skData = bitmap.Encode(SKEncodedImageFormat.Jpeg, option.Quality))
                        {
                            using (var image = SKImage.FromEncodedData(skData))
                            {
                                gifEncoder.AddFrame(image);
                            }
                        }
                    }
                }
            }

            gifEncoder.Finish();
            var stream = gifEncoder.Output();
            return stream.ToArray();
        }
    }
}
