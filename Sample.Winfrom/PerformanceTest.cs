using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core;
using Microsoft.Extensions.Options;
using Sample.Winfrom.Models;
using Sample.Winfrom.OptionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Sockets;

namespace Sample.Winfrom
{
    public class PerformanceTest
    {
        private CaptchaOptionsJsonModel options;
        private int loopCount;

        public event Action<int> Progress;
        public event Action<int, long, long> Complete;

        public PerformanceTest(CaptchaOptionsJsonModel options, int loopCount)
        {
            this.options = options;
            this.loopCount = loopCount;
        }

        public void Start()
        {
            var taskFactory = new TaskFactory();
            taskFactory.StartNew(() =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                long size = 0;

                for (var i = 0; i < loopCount; i++)
                {
                    var fontFamily = FontFamilyOptionProvider.Provide().First(e => e.Text == options.ImageOption.FontFamily).Value;
                    var service = CaptchaServiceBuilder
                       .New()
                       .CodeLength(options.CodeLength)
                       .CaptchaType((CaptchaType)options.CaptchaType)
                       .FontFamily(fontFamily)
                    .FontSize(options.ImageOption.FontSize)
                    .BubbleCount(options.ImageOption.BubbleCount)
                       .BubbleThickness(options.ImageOption.BubbleThickness)
                       .BubbleMinRadius(options.ImageOption.BubbleMinRadius)
                       .BubbleMaxRadius(options.ImageOption.BubbleMaxRadius)
                       .InterferenceLineCount(options.ImageOption.InterferenceLineCount)
                       .Animation(options.ImageOption.Animation)
                       .FrameDelay(options.ImageOption.FrameDelay)
                       .Width(options.ImageOption.Width)
                       .Height(options.ImageOption.Height)
                       .Quality(options.ImageOption.Quality)
                       .Build();
                    var captchaData = service.Generate(Guid.NewGuid().ToString());
                    if (Progress != null) Progress((int)((i + 1.0) / loopCount * 100.0));

                    size += captchaData.Bytes.Count();
                }
                if (Complete != null) Complete(loopCount, stopwatch.ElapsedMilliseconds, size);

            }, TaskCreationOptions.LongRunning);
        }
    }
}
