using Lazy.Captcha.Core.Generator.Image.Models;
using Lazy.Captcha.Core.Generator.Image.Option;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// 生成气泡图形描述
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="count"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        protected virtual List<BubbleGraphicDescription> GenerateBubbleGraphicDescriptions(int width, int height, int count, int minRadius, int maxRadius, float thickness)
        {
            var result = new List<BubbleGraphicDescription>();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var radius = random.Next(minRadius, maxRadius + 1);
                var point = new PointF(random.Next(width - 25) + radius, random.Next(height - 15) + radius);
                var size = new SizeF(radius, radius);
                var circle = new EllipsePolygon(point, size);
                result.Add(new BubbleGraphicDescription
                {
                    Color = DefaultColors.Instance.RandomColor(),
                    Path = circle,
                    Thickness = thickness
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="bubbleGraphicDescriptions">气泡图形描述</param>
        protected virtual void DrawBubbles(IImageProcessingContext ctx, List<BubbleGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                var drawingOptions = new DrawingOptions
                {
                    GraphicsOptions = new GraphicsOptions
                    {
                        BlendPercentage = gd.BlendPercentage
                    }
                };
                ctx.Draw(drawingOptions, gd.Color, gd.Thickness, gd.Path);
            });
        }

        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="option">选项</param>
        protected virtual void DrawBubbles(IImageProcessingContext ctx, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateBubbleGraphicDescriptions(option.Width, option.Height, option.BubbleCount, option.BubbleMinRadius, option.BubbleMaxRadius, option.BubbleThickness);
            DrawBubbles(ctx, graphicDescriptions);
        }

        /// <summary>
        /// 生成干扰线图形描述
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="count">数量</param>
        /// <returns>干扰线图形描述</returns>
        protected virtual List<InterferenceLineGraphicDescription> GenerateInterferenceLineGraphicDescriptions(int width, int height, int count)
        {
            var result = new List<InterferenceLineGraphicDescription>();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                int x1 = 5, y1 = random.Next(height / 2, height - 5);
                int x2 = width - 5, y2 = random.Next(5, height / 2);
                int ctrlx1 = random.Next(width / 4, width / 4 * 3), ctrly1 = random.Next(5, height - 5);
                int ctrlx2 = random.Next(width / 4, width / 4 * 3), ctrly2 = random.Next(5, height - 5);
                result.Add(new InterferenceLineGraphicDescription
                {
                    Color = DefaultColors.Instance.RandomColor(),
                    Start = new PointF(x1, y1),
                    Ctrl1 = new PointF(ctrlx1, ctrly1),
                    Ctrl2 = new PointF(ctrlx2, ctrly2),
                    End = new PointF(x2, y2)
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        protected virtual void DrawInterferenceLines(IImageProcessingContext ctx, List<InterferenceLineGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                var drawingOptions = new DrawingOptions
                {
                    GraphicsOptions = new GraphicsOptions
                    {
                        BlendPercentage = gd.BlendPercentage
                    }
                };
                ctx.DrawBeziers(drawingOptions, gd.Color, 1, gd.Start, gd.Ctrl1, gd.Ctrl2, gd.End);
            });
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">option</param>
        protected virtual void DrawInterferenceLines(IImageProcessingContext ctx, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateInterferenceLineGraphicDescriptions(option.Width, option.Height, option.InterferenceLineCount);
            DrawInterferenceLines(ctx, graphicDescriptions);
        }

        /// <summary>
        /// 生成干扰线图形描述
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <returns>文本图形描述</returns>
        protected virtual List<TextGraphicDescription> GenerateTextGraphicDescriptions(int width, int height, string text, Font font)
        {
            var result = new List<TextGraphicDescription>();
            var textPositions = MeasureTextPositions(width, height, text, font);

            for (var i = 0; i < text.Count(); i++)
            {
                var color = DefaultColors.Instance.RandomColor();
                result.Add(new TextGraphicDescription
                {
                    Text = text[i].ToString(),
                    Font = font,
                    Color = color,
                    Location = textPositions[i]
                });
            }

            return result;
        }

        /// <summary>
        /// 绘制干扰线
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="graphicDescriptions">图形描述</param>
        protected virtual void DrawTexts(IImageProcessingContext ctx, List<TextGraphicDescription> graphicDescriptions)
        {
            graphicDescriptions.ForEach(gd =>
            {
                var drawingOptions = new DrawingOptions
                {
                    GraphicsOptions = new GraphicsOptions
                    {
                        BlendPercentage = gd.BlendPercentage
                    }
                };
                ctx.DrawText(drawingOptions, gd.Text, gd.Font, gd.Color, gd.Location);
            });
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="text"></param>
        /// <param name="option"></param>
        protected virtual void DrawTexts(IImageProcessingContext ctx, string text, CaptchaImageGeneratorOption option)
        {
            var graphicDescriptions = GenerateTextGraphicDescriptions(option.Width, option.Height, text, option.Font);
            DrawTexts(ctx, graphicDescriptions);
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
            var currentX = 0.0f;
            var charTotalWidth = charWidths.Sum(x => x);
            var charXs = new List<float>();

            for (var i = 0; i < text.Count(); i++)
            {
                // 根据文字宽度比例，计算文字包含框宽度
                var wrapperWidth = charWidths[i] * 1.0f / charTotalWidth * width;
                // 文字在包含框内的padding
                var padding = (wrapperWidth - charWidths[i]) / 2;
                var textX = currentX + padding;

                var fontHeight = (int)TextMeasurer.Measure(text[i].ToString(), new RendererOptions(font)).Height;
                int textY = (height - fontHeight) / 2 + 3;  // 文字的纵坐标

                result.Add(new PointF(textX, textY));

                currentX += wrapperWidth;
            }

            return result;
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
            using (Image<Rgba32> img = new Image<Rgba32>(option.Width, option.Height, option.BackgroundColor))
            {
                img.Mutate(ctx =>
                {
                    // 绘制气泡
                    DrawBubbles(ctx, option);
                    // 绘制干扰线
                    DrawInterferenceLines(ctx, option);
                    // 绘制文字
                    DrawTexts(ctx, text, option);
                });

                using (var ms = new MemoryStream())
                {
                    img.SaveAsPng(ms);
                    return ms.ToArray();
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
            var texGraphicDescriptions = GenerateTextGraphicDescriptions(option.Width, option.Height, text, option.Font);
            var bubbleGraphicDescriptions = option.BubbleCount != 0 ?
                GenerateBubbleGraphicDescriptions(option.Width, option.Height, option.BubbleCount, option.BubbleMinRadius, option.BubbleMaxRadius, option.BubbleThickness)
                :
                new List<BubbleGraphicDescription>();
            var interferenceLineGraphicDescriptions = option.BubbleCount != 0 ?
                GenerateInterferenceLineGraphicDescriptions(option.Width, option.Height, option.InterferenceLineCount)
                :
                new List<InterferenceLineGraphicDescription>();

            using (Image<Rgba32> gif = new Image<Rgba32>(option.Width, option.Height, option.BackgroundColor))
            {
                // gif 循环播放
                gif.Metadata.GetGifMetadata().RepeatCount = 0;

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
                        bubbleGraphicDescriptions[j].BlendPercentage = new Random().Next(10) * 0.1f;
                    }

                    using (Image<Rgba32> frame = new Image<Rgba32>(option.Width, option.Height, option.BackgroundColor))
                    {
                        frame.Mutate(ctx =>
                        {
                            // 绘制气泡
                            DrawBubbles(ctx, bubbleGraphicDescriptions);
                            // 绘制干扰线
                            DrawInterferenceLines(ctx, interferenceLineGraphicDescriptions);
                            // 绘制文字
                            DrawTexts(ctx, texGraphicDescriptions);
                        });

                        var metadata = frame.Frames.RootFrame.Metadata.GetGifMetadata();
                        metadata.FrameDelay = 30;
                        gif.Frames.AddFrame(frame.Frames.RootFrame);
                    }
                }

                // 移除空白帧，否则动画会闪烁
                gif.Frames.RemoveFrame(0);

                using (var ms = new MemoryStream())
                {
                    gif.SaveAsGif(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
