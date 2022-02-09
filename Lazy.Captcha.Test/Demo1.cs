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
    public static class Demo1
    {
        public static void Run()
        {
            System.IO.Directory.CreateDirectory("output");

            using (var img = Image.Load("inputs/fb.jpg"))
            {
                using (var destRound = img.Clone(x => x.ConvertToAvatar(new Size(200, 200), 20)))
                {
                    destRound.Save("output/fb.png");
                }
            }
        }

        private static IImageProcessingContext ConvertToAvatar(this IImageProcessingContext processingContext, Size size, float cornerRadius)
        {
            return processingContext.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Crop
            }).ApplyRoundedCorners(cornerRadius);
        }

        // This method can be seen as an inline implementation of an `IImageProcessor`:
        // (The combination of `IImageOperations.Apply()` + this could be replaced with an `IImageProcessor`)
        private static IImageProcessingContext ApplyRoundedCorners(this IImageProcessingContext ctx, float cornerRadius)
        {
            Size size = ctx.GetCurrentSize();
            IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);

            //clear Both the color and the alpha of the destination are cleared. Neither the source nor the destination are used(except for destinations size and other meta - data which is always preserved.
            //src The source is copied to the destination.The destination is not used as input, though it is cleared.
            //dst The destination is left untouched. The source image is completely ignored.
            //src - over    The source is composited over the destination. this is the default alpha blending compose method, when neither the compose setting is set, nor is set in the image meta - data.
            //dst - over    The destination is composited over the source and the result replaces the destination.
            //src -in	The part of the source lying inside of the destination replaces the destination.
            //dst -in	The part of the destination lying inside of the source replaces the destination.Areas not overlaid are cleared.
            //src -out	The part of the source lying outside of the destination replaces the destination.
            //dst -out	The part of the destination lying outside of the source replaces the destination.
            //src - atop    The part of the source lying inside of the destination is composited onto the destination.
            //dst - atop    The part of the destination lying inside of the source is composited over the source and replaces the destination.Areas not overlaid are cleared.
            //xor The part of the source that lies outside of the destination is combined with the part of the destination that lies outside of the source.Source or Destination, but not both.
            // 1. 二选一 src, dst
            // 2. 二选一，但只取部分 src - in(src内部)，dst -in，src -out，dst -out
            // 3. 二选一，但只取重叠部分 取出的部分覆盖在Dst src - atop dst - atop
            // 4. 两者叠加 src - over dst - over
            ctx.SetGraphicsOptions(new GraphicsOptions()
            {
                Antialias = true,
                AlphaCompositionMode = PixelAlphaCompositionMode.DestOut // enforces that any part of this shape that has color is punched out of the background
            });

            // mutating in here as we already have a cloned original
            // use any color(not Transparent), so the corners will be clipped
            foreach (var c in corners)
            {
                ctx = ctx.Fill(Color.Red, c);
            }

            //ctx.Fill(Color.Red, new Star(x: 100.0f, y: 100.0f, prongs: 5, innerRadii: 20.0f, outerRadii: 30.0f));

            return ctx;
        }

        private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
        {
            // first create a square
            var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);

            // then cut out of the square a circle so we are left with a corner
            IPath cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

            // corner is now a corner shape positions top left
            //lets make 3 more positioned correctly, we can do that by translating the original around the center of the image

            float rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
            float bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

            // move it across the width of the image - the width of the shape
            IPath cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
            IPath cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
            IPath cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

            return new PathCollection(cornerTopLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
        }
    }
}
