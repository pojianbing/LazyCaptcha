using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Test
{
    public class Demo7
    {
        public static void Run()
        {
            const int width = 100;
            const int height = 100;

            // Delay between frames in (1/100) of a second.
            const int frameDelay = 100;

            // For demonstration: use images with different colors.
            Color[] colors = {
                Color.Green,
                Color.Red
            };

            // Create empty image.
            using Image<Rgba32> gif = new(width, height, Color.Blue);
            gif.Metadata.GetGifMetadata().RepeatCount = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                // Create a color image, which will be added to the gif.
                using Image<Rgba32> image = new(width, height, colors[i]);

                // Set the duration of the frame delay.
                GifFrameMetadata metadata1 = image.Frames.RootFrame.Metadata.GetGifMetadata();
                metadata1.FrameDelay = frameDelay;

                // Add the color image to the gif.
                gif.Frames.AddFrame(image.Frames.RootFrame);
            }

            // Save the final result.
            gif.SaveAsGif("a.gif");
        }
    }
}
