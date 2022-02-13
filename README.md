# LazyCaptcha

#### 介绍
仿[EasyCaptcha](https://gitee.com/ele-admin/EasyCaptcha)和[SimpleCaptcha](https://github.com/1992w/SimpleCaptcha),基于.Net Standard 2.0的图形验证码模块。

#### 安装教程
Install-Package Lazy.Captcha.Core

#### 使用说明

##### 默认使用

1. 注册服务
```
builder.Services.AddDistributedMemoryCache().AddCaptcha();
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
    
    
    
    
    