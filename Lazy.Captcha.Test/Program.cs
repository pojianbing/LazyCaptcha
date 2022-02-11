// See https://aka.ms/new-console-template for more information
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Test;
using SixLabors.ImageSharp;

Console.WriteLine("Hello, World!");

Chinese.Run();

DefaultFonts.instance.GetFont("");

ICaptchaGenerator g = DefaultCaptchaBuilder.NewBuilder().DrawBubble(false).Build();
g.Generate();