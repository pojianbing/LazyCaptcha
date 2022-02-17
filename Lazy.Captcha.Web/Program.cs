using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;

var builder = WebApplication.CreateBuilder(args);

//// redis存储，基于appsettings.json配置
//builder.Services.AddRedisCacheCaptcha(builder.Configuration, option =>
//{
//    // 背景色，字体要在这里配置
//    option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White;
//    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Epilog;
//});

// 内存存储， 基于appsettings.json配置
builder.Services.AddMemoryCacheCaptcha(builder.Configuration, option =>
{
    // 背景色，字体要在这里配置
    option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White;
    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Epilog;
});



//// 全部配置参数，基于代码配置
//builder.Services.AddMemoryCacheCaptcha(builder.Configuration, option =>
//{
//    option.CaptchaType = CaptchaType.WORD; // 验证码类型
//    option.CodeLength = 6; // 验证码长度, 要放在CaptchaType设置后.  当类型为算术表达式时，长度代表操作的个数
//    option.ExpirySeconds = 30; // 验证码过期时间
//    option.IgnoreCase = true; // 比较时是否忽略大小写
//    option.StoreageKeyPrefix = ""; // 存储键前缀
//    option.ImageOption.Animation = true; // 是否启用动画

//    option.ImageOption.Width = 150; // 验证码宽度
//    option.ImageOption.Height = 50; // 验证码高度
//    option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White; // 验证码背景色

//    option.ImageOption.BubbleCount = 2; // 气泡数量
//    option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
//    option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
//    option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

//    option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

//    option.ImageOption.FontSize = 36; // 字体大小
//    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Scandal; // 字体，中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
//});



builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();