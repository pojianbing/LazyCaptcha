# LazyCaptcha

#### 介绍
仿[EasyCaptcha](https://gitee.com/ele-admin/EasyCaptcha)和[SimpleCaptcha](https://github.com/1992w/SimpleCaptcha),基于.Net Standard 2.0的图形验证码模块。

#### 效果展示

|  CaptchaType |  字体 |静态图 |  动图|
|---|---|---|---|
|  DEFAULT |Actionj| ![输入图片说明](Images/DEFAULT_N.gif)  | ![输入图片说明](Images/DEFAULT_G.gif) |
|  WORD |Actionj| ![输入图片说明](Images/WORD_N.gif)  | ![输入图片说明](Images/WORD_G.gif) |
|  WORD_LOWER|Actionj| ![输入图片说明](Images/WORD_LOWER_N.gif)  | ![输入图片说明](Images/WORD_LOWER_G.gif) |
|  WORD_UPPER|Actionj| ![输入图片说明](Images/WORD_UPPER_N.gif)  | ![输入图片说明](Images/WORD_UPPER_G.gif) |
|  WORD_NUMBER_LOWER|Actionj| ![输入图片说明](Images/WORD_NUMBER_LOWER_N.gif)  | ![输入图片说明](Images/WORD_NUMBER_LOWER_G.gif) |
|  WORD_NUMBER_UPPER|Actionj| ![输入图片说明](Images/WORD_NUMBER_UPPER_N.gif)  | ![输入图片说明](Images/WORD_NUMBER_UPPER_G.gif) |
|  NUMBER|Fresnel| ![输入图片说明](Images/NUMBER_N.gif)  | ![输入图片说明](Images/NUMBER_G.gif) |
|  NUMBER_ZH_CN|kaiti| ![输入图片说明](Images/NUMBER_ZH_CN.gif)  | ![输入图片说明](Images/NUMBER_ZH_CN_G.gif) |
|  NUMBER_ZH_HK|kaiti| ![输入图片说明](Images/NUMBER_ZH_HK_N.gif)  | ![输入图片说明](Images/NUMBER_ZH_HK_G.gif) |
|  ARITHMETIC|Actionj| ![输入图片说明](Images/ARITHMETIC_N.gif)  | ![输入图片说明](Images/ARITHMETIC_G.gif) |
|  ARITHMETIC_ZH|Actionj| ![输入图片说明](Images/ARITHMETIC_ZH_N.gif)  | ![输入图片说明](Images/ARITHMETIC_ZH_G.gif) |


|  字体 | 图片  |
|---|---|
|  Actionj |  ![输入图片说明](Images/Font_Actionj.gif) |
|  Epilog|  ![输入图片说明](Images/Font_Epilog.gif) |
|  Fresnel|  ![输入图片说明](Images/Font_Fresnel.gif) |
|  Headache|  ![输入图片说明](Images/Font_Headache.gif) |
|  Kaiti|  ![输入图片说明](Images/Font_Kaiti.gif) |
|  Lexo|  ![输入图片说明](Images/Font_Lexo.gif) |
|  Prefix|  ![输入图片说明](Images/Font_Prefix.gif) |
|  Progbot|  ![输入图片说明](Images/Font_Progbot.gif) |
|  Ransom|  ![输入图片说明](Images/Font_Ransom.gif) |
|  Robot|  ![输入图片说明](Images/Font_Robot.gif) |
|  Scandal|  ![输入图片说明](Images/Font_Scandal.gif) |

#### 安装教程
Install-Package Lazy.Captcha.Core

#### 使用说明

1. 注册服务

默认设置
```
builder.Services.AddDistributedMemoryCache().AddCaptcha();
```

详细设置

```
builder.Services.AddDistributedMemoryCache().AddCaptcha(option =>
{
    option.CaptchaType = CaptchaType.DEFAULT; // 验证码类型
    option.CodeLength = 4; // 验证码长度, 要放在CaptchaType设置后
    option.ExpiryTime = TimeSpan.FromSeconds(30); // 验证码过期时间
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
    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj; // 字体，中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
});
```


2. Controller

```
public class CaptchaController : Controller
{
    private readonly ILogger<CaptchaController> _logger;
    private readonly ICaptcha _captcha;
    
    public CaptchaController(ILogger<CaptchaController> logger, ICaptcha captcha)
    {
        _logger = logger;
        _captcha = captcha;
    }
    
    [HttpGet]
    [Route("/captcha")]
    public IActionResult Captcha(string id)
    {
        var info = _captcha.Generate(id);
        var stream = new MemoryStream(info.Bytes);
        return File(stream, "image/gif");
    }
    
    [HttpGet]
    [Route("/captcha/validate")]
    public bool Validate(string id, string code)
    {
        if (!_captcha.Validate(id, code))
        {
            throw new Exception("无效验证码");
        }
    
        // 具体业务
    
        // 为了演示，这里仅做返回处理
        return true;
    }
}
```
    

    
    
    