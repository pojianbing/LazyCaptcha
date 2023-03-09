# LazyCaptcha v2(基于SkiaSharp)

## 介绍

仿[EasyCaptcha](https://gitee.com/ele-admin/EasyCaptcha)和[SimpleCaptcha](https://github.com/1992w/SimpleCaptcha),基于.Net Standard 2.0 的图形验证码模块。
**v2是指版本号>=2.0.0的版本，<2.0.0则称为v1。 v1基于ImageSharp，v2基于SkiaSharp**。SkiaSharp性能更好，但发布到linux时需要安装对应NativeAssets（ImageSharp则不需要）。 [v1文档地址](README_V1.md)


 **滑动验证码请移步[lazy-slide-captcha](https://gitee.com/pojianbing/lazy-slide-captcha)。**   
[码云地址](https://gitee.com/pojianbing/lazy-captcha)
[Github 地址](https://github.com/pojianbing/LazyCaptcha)

### [在线演示](http://captcha.sunseeyou.com/index.html)

### 效果展示

| CaptchaType           | 字体    | 静态图                                          | 动图                                            |
| --------------------- | ------- | ----------------------------------------------- | ----------------------------------------------- |
| DEFAULT (0)           | Actionj | ![输入图片说明](Images/DEFAULT_N.gif)           | ![输入图片说明](Images/DEFAULT_G.gif)           |
| CHINESE (1)           | kaiti   | ![输入图片说明](Images/CHINESE_N.gif)           | ![输入图片说明](Images/CHINESE_G.gif)           |
| NUMBER (2)            | Fresnel | ![输入图片说明](Images/NUMBER_N.gif)            | ![输入图片说明](Images/NUMBER_G.gif)            |
| NUMBER_ZH_CN (3)      | kaiti   | ![输入图片说明](Images/NUMBER_ZH_CN.gif)        | ![输入图片说明](Images/NUMBER_ZH_CN_G.gif)      |
| NUMBER_ZH_HK (4)      | kaiti   | ![输入图片说明](Images/NUMBER_ZH_HK_N.gif)      | ![输入图片说明](Images/NUMBER_ZH_HK_G.gif)      |
| WORD (5)              | Epilog  | ![输入图片说明](Images/WORD_N.gif)              | ![输入图片说明](Images/WORD_G.gif)              |
| WORD_LOWER (6)        | Epilog  | ![输入图片说明](Images/WORD_LOWER_N.gif)        | ![输入图片说明](Images/WORD_LOWER_G.gif)        |
| WORD_UPPER (7)        | Epilog  | ![输入图片说明](Images/WORD_UPPER_G.gif)        | ![输入图片说明](Images/WORD_UPPER_N.gif)        |
| WORD_NUMBER_LOWER (8) | Epilog  | ![输入图片说明](Images/WORD_NUMBER_LOWER_N.gif) | ![输入图片说明](Images/WORD_NUMBER_LOWER_G.gif) |
| WORD_NUMBER_UPPER (9) | Epilog  | ![输入图片说明](Images/WORD_NUMBER_UPPER_N.gif) | ![输入图片说明](Images/WORD_NUMBER_UPPER_G.gif) |
| ARITHMETIC (10)       | Epilog  | ![输入图片说明](Images/ARITHMETIC_N.gif)        | ![输入图片说明](Images/ARITHMETIC_G.gif)        |
| ARITHMETIC_ZH (11)    | kaiti   | ![输入图片说明](Images/ARITHMETIC_ZH_N.gif)     | ![输入图片说明](Images/ARITHMETIC_ZH_G.gif)     |

| 字体    | 图片                                     | 字体     | 图片                                      |
| ------- | ---------------------------------------- | -------- | ----------------------------------------- |
| Actionj | ![输入图片说明](Images/Font_Actionj.gif) | Epilog   | ![输入图片说明](Images/Font_Epilog.gif)   |
| Fresnel | ![输入图片说明](Images/Font_Fresnel.gif) | Headache | ![输入图片说明](Images/Font_Headache.gif) |
| Kaiti   | ![输入图片说明](Images/Font_Kaiti.gif)   | Lexo     | ![输入图片说明](Images/Font_Lexo.gif)     |
| Prefix  | ![输入图片说明](Images/Font_Prefix.gif)  | Progbot  | ![输入图片说明](Images/Font_Progbot.gif)  |
| Ransom  | ![输入图片说明](Images/Font_Ransom.gif)  | Robot    | ![输入图片说明](Images/Font_Robot.gif)    |
| Scandal | ![输入图片说明](Images/Font_Scandal.gif) |


### 安装

- [Package Manager](https://www.nuget.org/packages/Lazy.Captcha.Core)

```powershell
Install-Package Lazy.Captcha.Core
```

- [.NET CLI](https://www.nuget.org/packages/Lazy.Captcha.Core)

```powershell
dotnet add package Lazy.Captcha.Core
```
> linux环境下运行，请安装[SkiaSharp.NativeAssets.Linux](https://www.nuget.org/packages/SkiaSharp.NativeAssets.Linux)包，更多细节请查看[SkiaSharp](https://github.com/mono/SkiaSharp)官方文档。

### 使用说明

#### 1. 注册服务

```csharp
// 默认使用内存存储（AddDistributedMemoryCache）
builder.Services.AddCaptcha(builder.Configuration);

// 如果使用redis分布式缓存
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
//    options.InstanceName = "captcha:";
//});

```

#### 2. 配置

##### appsettings.json （不提供配置时，使用默认配置）

```csharp
{
  "ConnectionStrings": {
    // 使用Redis缓存时，需要配置此项
    // 使用格式参考 Microsoft.Extensions.Caching.StackExchangeRedis
    "RedisCache": "localhost,password=Aa123456."
  },
  "CaptchaOptions": {
    "CaptchaType": 5, // 验证码类型
    "CodeLength": 4, // 验证码长度, 要放在CaptchaType设置后  当类型为算术表达式时，长度代表操作的个数
    "ExpirySeconds": 60, // 验证码过期秒数
    "IgnoreCase": true, // 比较时是否忽略大小写
    "StoreageKeyPrefix": "", // 存储键前缀
    "ImageOption": {
      "Animation": false, // 是否启用动画
      "FontSize": 32, // 字体大小
      "Width": 100, // 验证码宽度
      "Height": 40, // 验证码高度
      "BubbleMinRadius": 5, // 气泡最小半径
      "BubbleMaxRadius": 10, // 气泡最大半径
      "BubbleCount": 3, // 气泡数量
      "BubbleThickness": 1.0, // 气泡边沿厚度
      "InterferenceLineCount": 4, // 干扰线数量
      "FontFamily": "kaiti", // 包含actionj,epilog,fresnel,headache,lexo,prefix,progbot,ransom,robot,scandal,kaiti
      "FrameDelay": 15, // 每帧延迟,Animation=true时有效, 默认30
      "BackgroundColor": "#ffff00", //  格式: rgb, rgba, rrggbb, or rrggbbaa format to match web syntax, 默认#fff
      "ForegroundColors": "", //  颜色格式同BackgroundColor,多个颜色逗号分割，随机选取。不填，空值，则使用默认颜色集
      "Quality": 100 // 图片质量（质量越高图片越大，gif调整无效可能会更大）
    }
  }
}
```
配置可以通过运行[Sample.Winfrom](Sample.Winfrom)生成或直接[下载Release](ConfigTool/Release.zip)运行。
![输入图片说明](Images/Config.png)

##### 代码配置

``` c#
// 全部配置
builder.Services.AddCaptcha(builder.Configuration, option =>
{
    option.CaptchaType = CaptchaType.WORD; // 验证码类型
    option.CodeLength = 6; // 验证码长度, 要放在CaptchaType设置后.  当类型为算术表达式时，长度代表操作的个数
    option.ExpirySeconds = 30; // 验证码过期时间
    option.IgnoreCase = true; // 比较时是否忽略大小写
    option.StoreageKeyPrefix = ""; // 存储键前缀

    option.ImageOption.Animation = true; // 是否启用动画
    option.ImageOption.FrameDelay = 30; // 每帧延迟,Animation=true时有效, 默认30

    option.ImageOption.Width = 150; // 验证码宽度
    option.ImageOption.Height = 50; // 验证码高度
    option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White; // 验证码背景色

    option.ImageOption.BubbleCount = 2; // 气泡数量
    option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
    option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
    option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

    option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

    option.ImageOption.FontSize = 36; // 字体大小
    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj; // 字体
    
    /* 
     * 中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
     * 当验证码类型为“ARITHMETIC”时，不要使用“Ransom”字体。（运算符和等号绘制不出来）
     */
});
```

#### 3. Controller

```csharp

[Route("captcha")]
[ApiController]
public class CaptchaController : Controller
{
    private readonly ICaptcha _captcha;

    public CaptchaController(ICaptcha captcha)
    {
        _captcha = captcha;
    }

    [HttpGet]
    public IActionResult Captcha(string id)
    {
        var info = _captcha.Generate(id);
        // 有多处验证码且过期时间不一样，可传第二个参数覆盖默认配置。
        //var info = _captcha.Generate(id,120);
        var stream = new MemoryStream(info.Bytes);
        return File(stream, "image/gif");
    }

    /// <summary>
    /// 演示时使用HttpGet传参方便，这里仅做返回处理
    /// </summary>
    [HttpGet("validate")]
    public bool Validate(string id, string code)
    {
        return _captcha.Validate(id, code);
    }

    /// <summary>
    /// 多次校验（https://gitee.com/pojianbing/lazy-captcha/issues/I4XHGM）
    /// 演示时使用HttpGet传参方便，这里仅做返回处理
    /// </summary>
    [HttpGet("validate2")]
    public bool Validate2(string id, string code)
    {
        return _captcha.Validate(id, code, false);
    }
}
```

### 自定义随机验证码
动图和静态图随机出现， CaptchaType随机。
#### 1. 自定义RandomCaptcha
```csharp
/// <summary>
/// 随机验证码
/// </summary>
public class RandomCaptcha : DefaultCaptcha
{
    private static readonly Random random = new();
    private static readonly CaptchaType[] captchaTypes = Enum.GetValues<CaptchaType>();

    public RandomCaptcha(IOptionsSnapshot<CaptchaOptions> options, IStorage storage) : base(options, storage)
    {
    }

    /// <summary>
    /// 更新选项
    /// </summary>
    /// <param name="options"></param>
    protected override void ChangeOptions(CaptchaOptions options)
    {
        // 随机验证码类型
        options.CaptchaType = captchaTypes[random.Next(0, captchaTypes.Length)];

        // 当是算数运算时，CodeLength是指运算数个数
        if (options.CaptchaType.IsArithmetic())
        {
            options.CodeLength = 2;
        }
        else
        {
            options.CodeLength = 4;
        }

        // 如果包含中文时，使用kaiti字体，否则文字乱码
        if (options.CaptchaType.ContainsChinese())
        {
            options.ImageOption.FontFamily = DefaultFontFamilys.Instance.Kaiti;
            options.ImageOption.FontSize = 24;
        }
        else
        {
            options.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj;
        }

        // 动静随机
        options.ImageOption.Animation = random.Next(2) == 0;

        // 干扰线随机
        options.ImageOption.InterferenceLineCount = random.Next(1, 4);

        // 气泡随机
        options.ImageOption.BubbleCount = random.Next(1, 4);

        // 其他选项...
    }
}
```

#### 2. 注入RandomCaptcha
```csharp
// 内存存储， 基于appsettings.json配置
builder.Services.AddCaptcha(builder.Configuration);
// 如果开启随机验证码，请打开下面的注释即可。
// builder.Services.Add(ServiceDescriptor.Scoped<ICaptcha, RandomCaptcha>());
```
> RandomCaptcha不包含在类库内部，仅做自定义演示，您可以根据自己的喜好，随机所有的CaptchaOptions值。

### 自定义字体

#### 1. 寻找字体
你可以通过[fontspace](https://www.fontspace.com/new/fonts)找到自己喜爱的字体。

#### 2. 将字体放入项目，并设置为嵌入资源。
> 当然也可以不作为嵌入资源，放到特定目录也是可以的，只要对下边ResourceFontFamilysFinder稍作修改即可。  

![输入图片说明](Images/font_demo.png)

#### 3. 定义查找字体帮助类，示例使用ResourceFontFamilysFinder
```csharp
public class ResourceFontFamilysFinder
{
    private static Lazy<List<SKTypeface>> _fontFamilies = new Lazy<List<SKTypeface>>(() =>
    {
        var fontFamilies = new List<SKTypeface>();
        var assembly = Assembly.GetExecutingAssembly();
        var names = assembly.GetManifestResourceNames();

        if (names?.Length > 0 == true)
        {
            foreach (var name in names)
            {
                if (!name.EndsWith("ttf")) continue;
                fontFamilies.Add(SKTypeface.FromStream(assembly.GetManifestResourceStream(name)));
            }
        }

        return fontFamilies;
    });


    public static SKTypeface Find(string name)
    {
        return _fontFamilies.Value.First(e => e.FamilyName == name);
    }
}
```

#### 4. 设置option
``` c#
// 内存存储， 基于appsettings.json配置
builder.Services.AddCaptcha(builder.Configuration, options =>
{
    // 自定义字体
    options.ImageOption.FontSize = 28;
    options.ImageOption.FontFamily = ResourceFontFamilysFinder.Find("KG HAPPY"); // 字体的名字在打开ttf文件时会显示
});
```

### .Net Framework下使用 <a id="framework"></a> 
新建mvc项目，.Net Framework选择4.6.2。

#### 1. Nuget安装
先安装SkiaSharp, 再安装Lazy.Captcha.Core

#### 2. Global.asax增加
``` c#
public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        CaptchaConfig();
    }

    private void CaptchaConfig()
    {
        var captchaService = CaptchaServiceBuilder
            .New()
            .Width(98)
            .Height(35)
            .FontSize(20)
            .CaptchaType(CaptchaType.ARITHMETIC)
            .FontFamily(DefaultFontFamilys.Instance.Ransom)
            .InterferenceLineCount(3)
            .Animation(false)
            .Build();
        CaptchaHelper.Initialization(captchaService);
    }
}
```

#### 3. Controller使用
``` c#
public class CaptchaController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        var id = Guid.NewGuid().ToString().Replace("_", "").Replace("-", "");
        var captchaData = CaptchaHelper.Generate(id);
        var output = new CaptchaResponse
        {
            Id = id,
            Base64 = captchaData.Base64
        };
        return Json(output, JsonRequestBehavior.AllowGet);
        
    }

    /// <summary>
    /// 演示时使用HttpGet传参方便，这里仅做返回处理
    /// </summary>
    [HttpGet()]
    public bool Validate(string id, string code)
    {
        return CaptchaHelper.Validate(id, code);
    }
}

public class CaptchaResponse
{
    public string Id { get; set; }
    public string Base64 { get; set; }
}
```

具体示例请参照 [Sample.MvcFramework](Sample.MvcFramework)项目。

### 常见问题

#### 1. linux下如何运行
除安装Lazy.Captcha.Core外，还需要安装[SkiaSharp.NativeAssets.Linux](https://www.nuget.org/packages/SkiaSharp.NativeAssets.Linux)，更多细节请查看[SkiaSharp](https://github.com/mono/SkiaSharp)官方文档。

如果运行时出现如下类似错误([相关issue](https://gitee.com/pojianbing/lazy-captcha/issues/I6CYGA))：
``` 
System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation.
---> System.TypeInitializationException: The type initializer for 'Lazy.Captcha.Core.DefaultFontFamilys' threw an exception.
---> System.TypeInitializationException: The type initializer for 'SkiaSharp.SKTypeface' threw an exception.
---> System.DllNotFoundException: Unable to load shared library 'libSkiaSharp' or one of its dependencies. 
```
可以尝试安装fontconfig

```
yum install fontconfig  // centos
apt-get install fontconfig // ubuntu
```

#### 2. docker发布注意事项
需要安装fontconfig, 具体参考Sample.NetCore示例项目[Dockerfile]( Sample.NetCore/Dockerfile)

#### 3. 仅生成验证码图片，不需要LazyCaptcha存储验证，怎么做？([相关issue](https://gitee.com/pojianbing/lazy-captcha/issues/I6KXBL))
```
var imageGenerator = new DefaultCaptchaImageGenerator();
var imageGeneratorOption = new CaptchaImageGeneratorOption()
{
    // 必须设置
    ForegroundColors = DefaultColors.Instance.Colors
};
var bytes = imageGenerator.Generate("hello", imageGeneratorOption);
```



### 版本历史

#### v2.0.2
-  去除启动时冗余调试信息

#### v2.0.1
-  优化减法算术表达式，避免结果负数（目前仅限两个操作数）。
-  优化验证码绘图显示。

#### v2.0.0
-  绘图改为SkiaSharp.

