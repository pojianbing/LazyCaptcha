// See https://aka.ms/new-console-template for more information
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Test;
using SixLabors.ImageSharp;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


for (var i = 0; i < 100; i++)
{

    Stopwatch st = new Stopwatch();
    st.Start();

    var generator = DefaultCaptchaImageBuilder
    .Create()
    .DrawBubble(false)
    .Build();

    generator.Generate();
    st.Stop();

    Console.WriteLine(st.ElapsedMilliseconds);
}

