using System.Diagnostics;
using Xunit.Abstractions;

namespace Lazy.Captcha.xUnit
{
    public class Demo
    {
        private readonly Stopwatch _stopWatch;
        private ITestOutputHelper _outputHelper { get; }

        private const string OutputPath = "output";

        public Demo(ITestOutputHelper outputHelper)
        {
            _stopWatch = new Stopwatch();
            _outputHelper = outputHelper;

            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
            else
            {
                foreach (var f in Directory.EnumerateFiles(OutputPath))
                {
                    File.Delete(f);
                }
            }
        }

        [Theory]
        [InlineData("#00000088")]
        [InlineData("#FF000088")]
        [InlineData("#00FF0088")]
        [InlineData("#0000FF88")]
        [Trait("SixLabors工具", "SixLabors")]
        public void GenerateCaptcha_Test(string color)
        {
            //var outputFileName = System.IO.Path.Combine(OutputPath, $"cc2_{color}.png");
            //using (var image = Image.Load("inputs/fb.jpg"))
            //{
            //    var outerRadii = Math.Min(image.Width, image.Height) / 2;
            //    var star = new Star(new PointF(image.Width / 2, image.Height / 2), 5, outerRadii / 2, outerRadii);

            //    image.Mutate(x => x.DrawPolygon(Pens.Solid(Color.White, 2), star.Points.ToArray()));
            //    image.Mutate(x => x.FillPolygon(Color.ParseHex(color), star.Points.ToArray()));
            //    image.Save(outputFileName);
            //}

            //var fileExists = File.Exists(outputFileName);
            //_outputHelper.WriteLine(fileExists ? "文件存在" : "文件不存在");
            //Assert.True(fileExists);
        }
    }
}