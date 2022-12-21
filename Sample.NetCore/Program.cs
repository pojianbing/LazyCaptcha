using Sample.NetCore;

var builder = WebApplication.CreateBuilder(args);

// 内存存储， 基于appsettings.json配置
builder.Services.AddCaptcha(builder.Configuration, options =>
{
    // 自定义字体
    //options.ImageOption.FontSize = 28;
    //options.ImageOption.FontFamily = ResourceFontFamilysFinder.Find("KG HAPPY");
});
// 如果开启随机验码，请打开注释即可。
//builder.Services.Add(ServiceDescriptor.Scoped<ICaptcha, RandomCaptcha>());

// Core项目使用的是IDistributedCache，
// 如果使用redis缓存，需要安装包 Microsoft.Extensions.Caching.StackExchangeRedis
// 参考：https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/distributed
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
//    options.InstanceName = "captcha:";
//});

// Core项目使用的是IDistributedCache，
// 如果使用SQLServer缓存，则安装 Microsoft.Extensions.Caching.SqlServer
//builder.Services.AddDistributedSqlServerCache(options =>
//{
//    options.ConnectionString = builder.Configuration.GetConnectionString(
//        "DistCache_ConnectionString");
//    options.SchemaName = "dbo";
//    options.TableName = "TestCache";
//});

// -----------------------------------------------------------------------------
// 全部配置参数，基于代码配置
//builder.Services.AddCaptcha(builder.Configuration, option =>
//{
//    option.CaptchaType = CaptchaType.WORD; // 验证码类型
//    option.CodeLength = 6; // 验证码长度, 要放在CaptchaType设置后.  当类型为算术表达式时，长度代表操作的个数
//    option.ExpirySeconds = 30; // 验证码过期时间
//    option.IgnoreCase = true; // 比较时是否忽略大小写
//    option.StoreageKeyPrefix = ""; // 存储键前缀

//    option.ImageOption.Animation = true; // 是否启用动画
//    option.ImageOption.FrameDelay = 30; // 每帧延迟,Animation=true时有效, 默认30

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

// 注意： appsettings.json配置和代码配置同时设置时，代码配置会覆盖appsettings.json配置。

builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
