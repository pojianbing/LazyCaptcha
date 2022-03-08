using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddRedisCacheCaptcha(builder.Configuration);


// 内存存储， 基于appsettings.json配置
builder.Services.AddCaptcha(builder.Configuration);


// 如果使用redis缓存，则添加这个
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
//    options.InstanceName = "captcha:";
//});


// -----------------------------------------------------------------------------
// 全部配置参数，基于代码配置
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
//    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj; // 字体，中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
//});

// 注意： appsettings.json配置和手动代码配置两者选其一, 同时配置时代码配置是无法覆盖appsettings.json配置。另外，appsettings.json配置无法设置所有配置项(例如FontFamily )。

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();