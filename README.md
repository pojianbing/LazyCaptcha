# LazyCaptcha

## 介绍
仿[EasyCaptcha](https://gitee.com/ele-admin/EasyCaptcha)和[SimpleCaptcha](https://github.com/1992w/SimpleCaptcha),基于.Net Standard 2.1的图形验证码模块。

[ **码云地址** ](https://gitee.com/pojianbing/lazy-captcha)
[ **Github地址** ](https://github.com/pojianbing/LazyCaptcha)

## 效果展示

|  CaptchaType |  字体 |静态图 |  动图|
|---|---|---|---|
|  DEFAULT |Actionj| ![输入图片说明](Images/DEFAULT_N.gif)  | ![输入图片说明](Images/DEFAULT_G.gif) |
|  WORD |Epilog| ![输入图片说明](Images/WORD_N.gif)  | ![输入图片说明](Images/WORD_G.gif) |
|  WORD_LOWER|Epilog| ![输入图片说明](Images/WORD_LOWER_N.gif)  | ![输入图片说明](Images/WORD_LOWER_G.gif) |
|  WORD_UPPER|Epilog| ![输入图片说明](Images/WORD_UPPER_G.gif)  | ![输入图片说明](Images/WORD_UPPER_N.gif) |
|  WORD_NUMBER_LOWER|Epilog| ![输入图片说明](Images/WORD_NUMBER_LOWER_N.gif)  | ![输入图片说明](Images/WORD_NUMBER_LOWER_G.gif) |
|  WORD_NUMBER_UPPER|Epilog| ![输入图片说明](Images/WORD_NUMBER_UPPER_N.gif)  | ![输入图片说明](Images/WORD_NUMBER_UPPER_G.gif) |
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

## 安装教程
``` shell
# 这里需要调整，发布后调整为对应的具体实现
Install-Package Lazy.Captcha.Core

```



## 使用说明

1. 注册服务（选择其一即可）

```
// 内存缓存
builder.Services.AddMemoryCacheCaptcha(builder.Configuration); 

// Redis缓存
builder.Services.AddRedisCacheCaptcha(builder.Configuration);

```

2. appsettings.json （不提供配置时，使用默认配置）

``` json
{
    "ConnectionStrings": {
        // 使用Redis缓存时，需要配置此项
        // 使用格式参考 Microsoft.Extensions.Caching.StackExchangeRedis
        "RedisCache": "localhost,password=Aa123456." 
    },
    "CaptchaOptions": {
        "CaptchaType": 5,  // 验证码类型
        "CodeLength": 4, // 验证码长度, 要放在CaptchaType设置后
        "ExpirySeconds": 60, // 验证码过期秒数
        "IgnoreCase": true, // 比较时是否忽略大小写
        "ImageOption": {
            "Animation": false, // 是否启用动画
            "FontSize": 32, // 字体大小
            "Width": 100, // 验证码宽度
            "Height": 40, // 验证码高度
            "BubbleMinRadius": 5, // 气泡最小半径
            "BubbleMaxRadius": 10, // 气泡最大半径
            "BubbleCount": 3, // 气泡数量
            "BubbleThickness": 1.0, // 气泡边沿厚度
            "InterferenceLineCount": 4 // 干扰线数量
        }
    }
}
```



2. 其他详细设置

```csharp
// 全部配置
builder.Services.AddMemoryCacheCaptcha(builder.Configuration, option =>
{
    option.CaptchaType = CaptchaType.WORD; // 验证码类型
    option.CodeLength = 6; // 验证码长度, 要放在CaptchaType设置后
    option.ExpirySeconds = 30; // 验证码过期时间
    option.IgnoreCase = true; // 比较时是否忽略大小写
    option.ImageOption.Animation = true; // 是否启用动画

    option.ImageOption.Width = 150; // 验证码宽度
    option.ImageOption.Height = 50; // 验证码高度
    option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White; // 验证码背景色

    option.ImageOption.BubbleCount = 2; // 气泡数量
    option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
    option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
    option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

    option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

    option.ImageOption.FontSize = 36; // 字体大小
    option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Scandal; // 字体，中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
});
```



3. Controller

```csharp

    [ApiController]
    [Route("api/captcha")]
    public class CaptchaController : ControllerBase
    {
        public ICaptcha Captcha { get; }

        public CaptchaController(ICaptcha captcha)
        {
            Captcha=captcha;
        }

        /// <summary>
        /// 生成
        /// </summary>
        [HttpGet]
        public IActionResult GenerateCaptcha(string id)
        {
            var info = Captcha.Generate(id);
            var stream = new MemoryStream(info.Bytes);
            return File(stream, "image/gif");
        }

        /// <summary>
        /// 校验
        /// </summary>
        [HttpPost]
        public IActionResult Validate(string id, string code)
        {
            var pass = Captcha.Validate(id, code);
            if (pass)
            {
                // 业务
                return Ok("验证码校验通过");
            }
            else
            {
                return Unauthorized("验证码校验未通过");
            }
        }
    }
```


 

