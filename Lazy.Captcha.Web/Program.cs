using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Code;
using Lazy.Captcha.Core.Generator.Image.Option;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache().AddCaptcha(option =>
{
    option.CaptchaType = CaptchaType.ARITHMETIC_ZH;
    option.ImageOption.Width = 300;
    option.ImageOption.Height = 150;
    option.ImageOption.FontStyle = SixLabors.Fonts.FontStyle.Regular;
    option.ImageOption.FontSize = 80;
    //option.ImageOption.FontFamily = DefaultFontFamilys.instance.Actionj;
    option.ImageOption.BubbleCount = 10;
    option.ImageOption.BubbleMinRadius = 10;
    option.ImageOption.BubbleMaxRadius = 20;
    option.ImageOption.BubbleThickness = 3;
    option.ImageOption.InterferenceLineCount = 10;
    //option.CodeLength = 3; // 放在CaptchaType设置后
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
