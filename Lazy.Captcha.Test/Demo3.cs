using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Test
{
    public static class Demo3
    {
        const string LongText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec aliquet lorem at magna mollis, non semper erat aliquet. In leo tellus, sollicitudin non eleifend et, luctus vel magna. Proin at lacinia tortor, malesuada molestie nisl. Quisque mattis dui quis eros ultricies, quis faucibus turpis dapibus. Donec urna ipsum, dignissim eget condimentum at, condimentum non magna. Donec non urna sit amet lectus tincidunt interdum vitae vitae leo. Aliquam in nisl accumsan, feugiat ipsum condimentum, scelerisque diam. Vivamus quam diam, rhoncus ut semper eget, gravida in metus.
Nullam quis malesuada metus. In hac habitasse platea dictumst. Aliquam faucibus eget eros nec vulputate. Quisque sed dolor lacus. Proin non dolor vitae massa rhoncus vestibulum non a arcu. Morbi mollis, arcu id pretium dictum, augue dui cursus eros, eu pharetra arcu ante non lectus. Integer quis tellus ipsum. Integer feugiat augue id tempus rutrum. Ut eget interdum leo, id fermentum lacus. Morbi euismod, mi at tempus finibus, ante risus ornare eros, eu ultrices ipsum dolor vitae risus. Mauris molestie pretium massa vitae maximus. Fusce ut egestas ex, vitae semper nulla. Proin pretium elit libero, et interdum enim molestie ac.
Pellentesque fermentum vitae lacus non aliquet. Sed nulla ipsum, hendrerit sit amet vulputate varius, volutpat eget est. Pellentesque eget ante erat. Vestibulum venenatis ex quis pretium sagittis. Etiam vel nibh sit amet leo gravida efficitur. In hac habitasse platea dictumst. Nullam lobortis euismod sem dapibus aliquam. Proin accumsan velit a magna gravida condimentum. Nam non massa ac nibh viverra rutrum. Phasellus elit tortor, malesuada et purus nec, placerat mattis neque. Proin auctor risus vel libero ultrices, id fringilla erat facilisis. Donec rutrum, enim sit amet faucibus viverra, velit tellus aliquam tellus, et tempus tellus diam sed dui. Integer fringilla convallis nisl venenatis elementum. Sed volutpat massa ut mauris accumsan, mollis finibus tortor pretium.";
        const string LongText1 = "abc";


        public static void Run()
        {
            System.IO.Directory.CreateDirectory("output");
            using (var img = Image.Load("inputs/fb.jpg"))
            {
                // For production application we would recommend you create a FontCollection
                // singleton and manually install the ttf fonts yourself as using SystemFonts
                // can be expensive and you risk font existing or not existing on a deployment
                // by deployment basis.
                Font font = SystemFonts.CreateFont("Arial", 10); // for scaling water mark size is largely ignored.

                using (var img2 = img.Clone(ctx => ctx.ApplyScalingWaterMark(font, "A short piece of text", Color.HotPink, 5, false)))
                {
                    img2.Save("output/simple.png");
                }


                using (var img2 = img.Clone(ctx => ctx.ApplyScalingWaterMark(font, LongText, Color.HotPink, 5, true)))
                {
                    img2.Save("output/wrapped.png");
                }

                // the original `img` object has not been altered at all.
            }
        }

        private static IImageProcessingContext ApplyScalingWaterMark(this IImageProcessingContext processingContext,
            Font font,
            string text,
            Color color,
            float padding,
            bool wordwrap)
        {
            if (wordwrap)
            {
                return processingContext.ApplyScalingWaterMarkWordWrap(font, text, color, padding);
            }
            else
            {
                return processingContext.ApplyScalingWaterMarkSimple(font, text, color, padding);
            }
        }

        private static IImageProcessingContext ApplyScalingWaterMarkSimple(this IImageProcessingContext processingContext,
            Font font,
            string text,
            Color color,
            float padding)
        {
            Size imgSize = processingContext.GetCurrentSize();

            float targetWidth = imgSize.Width - (padding * 2);
            float targetHeight = imgSize.Height - (padding * 2);

            // measure the text size
            FontRectangle size = TextMeasurer.Measure(text, new RendererOptions(font));

            //find out how much we need to scale the text to fill the space (up or down)
            float scalingFactor = Math.Min(imgSize.Width / size.Width, imgSize.Height / size.Height);

            //create a new font
            Font scaledFont = new Font(font, scalingFactor * font.Size);

            var center = new PointF(imgSize.Width / 2, imgSize.Height / 2);
            var textGraphicOptions = new DrawingOptions()
            {
                TextOptions = {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            return processingContext.DrawText(textGraphicOptions, text, scaledFont, color, center);
        }

        private static IImageProcessingContext ApplyScalingWaterMarkWordWrap(this IImageProcessingContext processingContext,
            Font font,
            string text,
            Color color,
            float padding)
        {
            Size imgSize = processingContext.GetCurrentSize();
            float targetWidth = imgSize.Width - (padding * 2);
            float targetHeight = imgSize.Height - (padding * 2);

            float targetMinHeight = imgSize.Height - (padding * 3); // must be with in a margin width of the target height

            // now we are working i 2 dimensions at once and can't just scale because it will cause the text to
            // reflow we need to just try multiple times

            var scaledFont = font;
            FontRectangle s = new FontRectangle(0, 0, float.MaxValue, float.MaxValue);

            float scaleFactor = (scaledFont.Size / 2); // every time we change direction we half this size
            int trapCount = (int)scaledFont.Size * 2;
            if (trapCount < 10)
            {
                trapCount = 10;
            }

            bool isTooSmall = false;

            while ((s.Height > targetHeight || s.Height < targetMinHeight) && trapCount > 0)
            {
                if (s.Height > targetHeight)
                {
                    if (isTooSmall)
                    {
                        scaleFactor = scaleFactor / 2;
                    }

                    scaledFont = new Font(scaledFont, scaledFont.Size - scaleFactor);
                    isTooSmall = false;
                }

                if (s.Height < targetMinHeight)
                {
                    if (!isTooSmall)
                    {
                        scaleFactor = scaleFactor / 2;
                    }
                    scaledFont = new Font(scaledFont, scaledFont.Size + scaleFactor);
                    isTooSmall = true;
                }
                trapCount--;

                s = TextMeasurer.Measure(text, new RendererOptions(scaledFont)
                {
                    WrappingWidth = targetWidth
                });
            }

            var center = new PointF(padding, imgSize.Height / 2);
            var textGraphicOptions = new DrawingOptions()
            {
                TextOptions = {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    WrapTextWidth = targetWidth
                }
            };
            return processingContext.DrawText(textGraphicOptions, text, scaledFont, color, center);
        }
    }
}
