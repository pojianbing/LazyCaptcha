using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Test
{
    public class Demo4
    {
        public static void Run()
        {
            System.IO.Directory.CreateDirectory("output");

            using (Image image = Image.Load("inputs/fb.jpg"))
            {
                image.Mutate(x => x
                     .Resize(image.Width / 2, image.Height / 2)
                     .Grayscale()
                     .BlackWhite());

                image.Save("output/fb.png"); // Automatic encoder selected based on extension.
            }
        }
    }
}
