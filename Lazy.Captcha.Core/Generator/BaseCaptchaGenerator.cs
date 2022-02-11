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

namespace Lazy.Captcha.Core.Generator
{
    /// <summary>
    /// 验证码生成器基类
    /// </summary>
    public abstract class BaseCaptchaGenerator : ICaptchaGenerator
    {
        /// <summary>
        /// 数字列表
        /// </summary>
        protected readonly static List<char> NUMBER_LETTERS = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// 小写英文字符
        /// </summary>
        protected readonly static List<char> EN_LOWER_LETTERS = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        /// <summary>
        /// 大写英文字符
        /// </summary>
        protected readonly static List<char> EN_UPPER_LETTERS = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        /// <summary>
        /// 中文数字小写
        /// </summary>
        protected readonly static List<char> NUMBER_ZH_CN_LETTERS = new List<char> { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
        /// <summary>
        /// 中文数字大写
        /// </summary>
        protected readonly static List<char> NUMBER_ZH_HK_LETTERS = new List<char> { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' };

        /// <summary>
        /// 字体
        /// </summary>
        protected CaptchaGeneratorOption Option;

        public BaseCaptchaGenerator(CaptchaGeneratorOption option)
        {
            this.Option = option;

            if (this.Option.FontFamily == null)
            {
                this.Option.Font = this.IsChineseIn() ?  DefaultFonts.instance.Kaiti() : DefaultFonts.instance.Epilog();
            }
        }

        private bool IsChineseIn()
        {
            return this.Option.CaptchaType == CaptchaType.CHINESE ||
                   this.Option.CaptchaType == CaptchaType.NUMBER_ZH_CN ||
                   this.Option.CaptchaType == CaptchaType.NUMBER_ZH_HK ||
                   this.Option.CaptchaType == CaptchaType.NUMBER_ZH_HK;
        }

        /// <summary>
        /// 绘制多个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        /// <param name="count">气泡数量</param>
        /// <param name="thickness">气泡边厚度</param>
        protected virtual void DrawBubble(IImageProcessingContext ctx, int width, int height, int count, float thickness = 1)
        {
            for (var i = 0; i < count; i++)
            {
                DrawBubble(ctx, width, height, thickness);
            }
        }

        /// <summary>
        /// 绘制单个气泡
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="width">验证码的宽</param>
        /// <param name="height">验证码的高</param>
        /// <param name="thickness">气泡边厚度</param>
        protected virtual void DrawBubble(IImageProcessingContext ctx, int width, int height, float thickness = 1)
        {
            var random = new Random();
            var color = ColorManager.instance.GetRandomColor();
            var w = 5 + random.Next(10);
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
                var color = ColorManager.instance.GetRandomColor();
                int x1 = 5, y1 = random.Next(5, height / 2);
                int x2 = width - 5, y2 = random.Next(height / 2, height - 5);
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
        protected virtual void DrawText(IImageProcessingContext ctx, int width, int height, string text)
        {
            var textPositions = MeasureTextPositions(width, height, text, Option.Font);

            for (var i = 0; i < text.Count(); i++)
            {
                var color = ColorManager.instance.GetRandomColor();
                ctx.DrawText(text[i].ToString(), Option.Font, color, textPositions[i]);
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

        public virtual byte[] Generate()
        {
            using Image<Rgba32> img = new(Option.Width, Option.Height, this.Option.BackgroundColor);
            img.Mutate(ctx =>
            {
                // 绘制气泡
                if (this.Option.DrawBubble)
                {
                    DrawBubble(ctx, Option.Width, Option.Height, this.Option.BubbleCount, this.Option.BubbleThickness);
                }

                // 绘制干扰线
                if (this.Option.DrawInterferenceLine)
                {
                    DrawInterferenceLine(ctx, Option.Width, Option.Height, this.Option.InterferenceLineCount);
                }

                // 绘制文字
                DrawText(ctx, Option.Width, Option.Height, this.GenerateText());
            });

            img.Save("b.png");

            using (var ms = new MemoryStream())
            {
                img.Save(ms, PngFormat.Instance);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 随机挑选字符
        /// </summary>
        /// <param name="letters">字符列表</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        protected string Pick(List<char> letters, int count)
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                result.Append(letters[random.Next(letters.Count)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 生成字符列表
        /// </summary>
        /// <returns></returns>
        private List<char> GenerateLetters()
        {
            var letters = new List<char>();

            switch (this.Option.CaptchaType)
            {
                case CaptchaType.CHINESE:
                    letters.AddRange(this.Option.ChineseTexts);
                    break;
                case CaptchaType.NUMBER:
                    letters.AddRange(NUMBER_LETTERS);
                    break;
                case CaptchaType.NUMBER_ZH_CN:
                    letters.AddRange(NUMBER_ZH_CN_LETTERS);
                    break;
                case CaptchaType.NUMBER_ZH_HK:
                    letters.AddRange(NUMBER_ZH_HK_LETTERS);
                    break;
                case CaptchaType.DEFAULT:
                    letters.AddRange(NUMBER_LETTERS);
                    letters.AddRange(EN_LOWER_LETTERS);
                    letters.AddRange(EN_UPPER_LETTERS);
                    break;
                case CaptchaType.WORD:
                    letters.AddRange(EN_LOWER_LETTERS);
                    letters.AddRange(EN_UPPER_LETTERS);
                    break;
                case CaptchaType.WORD_LOWER:
                    letters.AddRange(EN_LOWER_LETTERS);
                    break;
                case CaptchaType.WORD_UPPER:
                    letters.AddRange(EN_UPPER_LETTERS);
                    break;
                case CaptchaType.WORD_NUMBER_LOWER:
                    letters.AddRange(NUMBER_LETTERS);
                    letters.AddRange(EN_LOWER_LETTERS);
                    break;
                case CaptchaType.WORD_NUMBER_UPPER:
                    letters.AddRange(NUMBER_LETTERS);
                    letters.AddRange(EN_UPPER_LETTERS);
                    break;
            }

            return letters;
        }

        /// <summary>
        /// 生成验证码文本
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateText()
        {
            return Pick(GenerateLetters(), this.Option.Length);
        }
    }
}
