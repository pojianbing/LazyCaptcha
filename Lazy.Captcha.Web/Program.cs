using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCacheCaptcha(builder.Configuration, option =>
 {
     option.CaptchaType = CaptchaType.WORD; // 验证码类型
     option.CodeLength = 4; // 验证码长度, 要放在CaptchaType设置后
     option.ExpirySeconds = 60; // 验证码过期时间
     option.IgnoreCase = true; // 比较时是否忽略大小写
     option.ImageOption.Animation = false; // 是否启用动画

     option.ImageOption.Width = 130; // 验证码宽度
     option.ImageOption.Height = 48; // 验证码高度
     option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White; // 验证码背景色

     option.ImageOption.BubbleCount = 2; // 气泡数量
     option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
     option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
     option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

     option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

     option.ImageOption.FontSize = 28; // 字体大小
     option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Scandal; // 字体，中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
 });

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();