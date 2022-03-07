using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Test
{
    public class Demo5
    {
        const int QrCodeSize = 25;

        static bool[,] GetQrPattern()
        {
            const bool o = true;
            const bool _ = false;
            return new[,]
            {
                { _, _, _, _, _, _, _, o, _, _, o, o, o, _, o, _, _, o, _, _, _, _, _, _, _ },
                { _, o, o, o, o, o, _, o, o, o, o, o, o, o, o, o, o, o, _, o, o, o, o, o, _ },
                { _, o, _, _, _, o, _, o, _, o, o, _, _, _, o, _, _, o, _, o, _, _, _, o, _ },
                { _, o, _, _, _, o, _, o, _, _, o, _, o, _, _, _, o, o, _, o, _, _, _, o, _ },
                { _, o, _, _, _, o, _, o, _, _, _, _, o, o, _, o, _, o, _, o, _, _, _, o, _ },
                { _, o, o, o, o, o, _, o, o, _, o, o, _, o, _, o, o, o, _, o, o, o, o, o, _ },
                { _, _, _, _, _, _, _, o, _, o, _, o, _, o, _, o, _, o, _, _, _, _, _, _, _ },
                { o, o, o, o, o, o, o, o, o, _, o, _, o, o, _, _, o, o, o, o, o, o, o, o, o },
                { _, _, _, _, o, o, _, o, _, o, o, o, o, _, _, o, o, _, o, o, _, _, _, o, _ },
                { _, _, o, _, o, o, o, _, _, _, o, o, o, o, _, o, o, o, o, _, o, o, o, _, o },
                { o, o, o, o, _, o, _, _, _, o, o, o, o, _, _, o, _, _, _, o, o, o, o, o, o },
                { o, _, o, o, o, _, o, o, o, o, o, _, _, _, o, _, o, _, o, _, o, _, _, o, o },
                { o, _, o, o, o, o, _, _, o, _, o, _, o, _, _, o, o, _, _, o, _, o, _, _, _ },
                { o, _, o, _, o, o, o, o, _, o, _, _, o, o, o, _, o, o, _, _, _, o, o, o, _ },
                { o, _, o, o, o, _, _, _, o, _, _, o, _, o, o, o, o, _, o, o, _, o, _, _, o },
                { _, o, _, o, _, o, o, o, _, o, o, o, _, o, o, _, o, o, _, _, _, o, o, o, _ },
                { o, o, _, o, o, o, _, o, o, o, _, _, o, o, o, _, _, _, _, _, _, _, _, _, _ },
                { o, o, o, o, o, o, o, o, _, o, _, o, _, o, _, _, _, o, o, o, _, o, _, o, _ },
                { _, _, _, _, _, _, _, o, o, o, o, _, _, _, o, o, _, o, _, o, _, o, _, _, _ },
                { _, o, o, o, o, o, _, o, o, o, _, o, _, o, _, _, _, o, o, o, _, o, o, _, _ },
                { _, o, _, _, _, o, _, o, o, _, _, _, _, _, o, o, _, _, _, _, _, _, o, _, o },
                { _, o, _, _, _, o, _, o, _, o, o, o, _, _, o, _, o, o, _, o, _, _, _, _, _ },
                { _, o, _, _, _, o, _, o, _, _, _, _, _, _, o, _, _, _, _, o, _, o, _, _, o },
                { _, o, o, o, o, o, _, o, _, o, _, o, o, o, o, o, o, _, _, o, _, o, _, o, o },
                { _, _, _, _, _, _, _, o, _, _, o, o, _, o, _, o, o, o, o, _, _, _, _, _, _ },
            };
        }

        public static void Run()
        {

            bool[,] pattern = GetQrPattern();

            // L8 is a grayscale pixel format storing a single 8-bit (1 byte) channel of luminance per pixel.
            // Make sure to Dispose() the image in your app! We do it by a using statement in this example:
            using Image<L8> image = RenderQrCodeToImage(pattern, 10);

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

            // Store the result as a grayscale 1 bit per pixel png for maximum compression:
            image.SaveAsPng(fileName, new PngEncoder()
            {
                BitDepth = PngBitDepth.Bit1,
                ColorType = PngColorType.Grayscale
            });
            Console.WriteLine($"Saved to: {fileName}");
        }

        // ImageSharp 2.0 will break this method: https://github.com/SixLabors/ImageSharp/issues/1739
        // We will update the example after the release. See ImageSharp 2.0 variant in comments.
        static Image<L8> RenderQrCodeToImage(bool[,] pattern, int pixelSize)
        {
            int imageSize = pixelSize * QrCodeSize;
            Image<L8> image = new Image<L8>(imageSize, imageSize);

            L8 black = new L8(0);
            L8 white = new L8(255);

            // Scan the QR pattern row-by-row
            for (int yQr = 0; yQr < QrCodeSize; yQr++)
            {
                // Fill 'pixelSize' number image rows that correspond to the current QR pattern row:
                for (int y = yQr * pixelSize; y < (yQr + 1) * pixelSize; y++)
                {
                    // Get a Span<L8> of pixels for the current image row:
                    //Span<L8> pixelRow = image.GetPixelRowSpan(y);
                    Span<L8> pixelRow = image.GetPixelRowSpan(y);

                    // Loop through the values for the current QR pattern row:
                    for (int xQr = 0; xQr < QrCodeSize; xQr++)
                    {
                        L8 color = pattern[xQr, yQr] ? white : black;

                        // Fill 'pixelSize' number of image pixels corresponding to the current QR pattern value:
                        for (int x = xQr * pixelSize; x < (xQr + 1) * pixelSize; x++)
                        {
                            pixelRow[x] = color;
                        }
                    }
                }
            }

            // ImageSharp 2.0 variant:
            // image.ProcessPixelRows(pixelAccessor =>
            // {
            //     // Scan the QR pattern row-by-row
            //     for (int yQr = 0; yQr < QrCodeSize; yQr++)
            //     {
            //         // Fill 'pixelSize' number image rows that correspond to the current QR pattern row:
            //         for (int y = yQr * pixelSize; y < (yQr + 1) * pixelSize; y++)
            //         {
            //             // Get a Span<L8> of pixels for the current image row:
            //             Span<L8> pixelRow = pixelAccessor.GetRowSpan(y);
            //
            //             // Loop through the values for the current QR pattern row:
            //             for (int xQr = 0; xQr < QrCodeSize; xQr++)
            //             {
            //                 L8 color = pattern[xQr, yQr] ? white : black;
            //
            //                 // Fill 'pixelSize' number of image pixels corresponding to the current QR pattern value:
            //                 for (int x = xQr * pixelSize; x < (xQr + 1) * pixelSize; x++)
            //                 {
            //                     pixelRow[x] = color;
            //                 }
            //             }
            //         }
            //     }
            // });

            return image;
        }
    }
}
