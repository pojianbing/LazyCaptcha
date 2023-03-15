using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Lazy.Captcha.Core.Generator.Image.Option;
using Newtonsoft.Json;
using Sample.Winfrom.Helpers;
using Sample.Winfrom.Models;
using Sample.Winfrom.OptionProviders;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Winfrom
{
    public partial class MainForm : Form
    {
        private string captchaId = string.Empty;
        private CaptchaService captchaService;
        private CaptchaOptionsJsonModel captchaOptions;
        private string tpl_File = Application.StartupPath + @"\templates\tpl.html";
        private string config_File = Application.StartupPath + @"\templates\config.html";
        private byte[] currentCaptchaBytes;

        private Dictionary<string, PictureBox> fontPictureBoxMap;

        public MainForm()
        {
            InitializeComponent();
            InitFontPictureBoxMap();
            BindDataSource();
            GenerateCaptcha();
        }

        private void InitFontPictureBoxMap()
        {
            fontPictureBoxMap = new Dictionary<string, PictureBox>
            {
                { "Actionj", this.Actionj_Pbx },
                { "Kaiti", this.Kaiti_Pbx },
                { "Fresnel", this.Fresnel_Pbx },
                { "Prefix", this.Prefix_Pbx },
                { "Ransom", this.Ransom_Pbx },
                { "Scandal", this.Scandal_Pbx },
                { "Epilog", this.Epilog_Pbx },
                { "Headache", this.Headache_Pbx },
                { "Lexo", this.Lexo_Pbx },
                { "Progbot", this.Progbot_Pbx },
                { "Robot", this.Robot_Pbx }
            };
        }

        private void BindDataSource()
        {
            this.CpatchaType_Cbx.DataSource = CaptchaTypeOptionProvider.Provide();
            this.FontFamily_Cbx.DataSource = FontFamilyOptionProvider.Provide();
        }

        private CaptchaOptionsJsonModel GenerateCaptchaOptions()
        {
            var fontFamilyOption = (FontFamilyOption)this.FontFamily_Cbx.SelectedItem;

            return new CaptchaOptionsJsonModel()
            {
                CaptchaType = (int)this.CpatchaType_Cbx.SelectedValue,
                CodeLength = (int)this.Length_Nud.Value,
                ExpirySeconds = 60,
                IgnoreCase = true,
                StoreageKeyPrefix = "",
                ImageOption = new CaptchaImageGeneratorOptionJsonModel
                {
                    Animation = this.Gif_Cbx.Checked,
                    FrameDelay = (int)this.FrameDelay_Ndp.Value,
                    FontSize = (int)this.FontSize_Nud.Value,
                    Width = (int)this.Width_Nud.Value,
                    Height = (int)this.Height_Nud.Value,
                    BubbleMinRadius = (int)this.BubbleMinRadius_Nud.Value,
                    BubbleMaxRadius = (int)this.BubbleMaxRadius_Nud.Value,
                    BubbleCount = (int)this.BubbleCount_Nud.Value,
                    BubbleThickness = (int)this.BubbleThickness_Nud.Value,
                    InterferenceLineCount = (int)this.InterferenceLineCount_Nud.Value,
                    FontFamily = fontFamilyOption == null ? "Actionj" : fontFamilyOption.Text,
                    Quality = (int)this.Quality_Nud.Value,
                    TextBold = TextBold_Cbx.Checked
                }
            };
        }

        private CaptchaService GenerateCaptchaService(CaptchaOptionsJsonModel options)
        {
            var fontFamily = FontFamilyOptionProvider.Provide().First(e => e.Text == options.ImageOption.FontFamily).Value;
            return CaptchaServiceBuilder
               .New()
               .CodeLength(options.CodeLength)
               .CaptchaType((CaptchaType)options.CaptchaType)
               .FontFamily(fontFamily)
               .FontSize(options.ImageOption.FontSize)
               .BubbleCount(options.ImageOption.BubbleCount)
               .BubbleThickness(options.ImageOption.BubbleThickness)
               .BubbleMinRadius(options.ImageOption.BubbleMinRadius)
               .BubbleMaxRadius(options.ImageOption.BubbleMaxRadius)
               .InterferenceLineCount(options.ImageOption.InterferenceLineCount)
               .Animation(options.ImageOption.Animation)
               .FrameDelay(options.ImageOption.FrameDelay)
               .Width(options.ImageOption.Width)
               .Height(options.ImageOption.Height)
               .Quality(options.ImageOption.Quality)
               .TextBold(options.ImageOption.TextBold)
               .Build();
        }

        private CaptchaService GenerateCaptchaService()
        {
            this.captchaOptions = GenerateCaptchaOptions();
            return GenerateCaptchaService(this.captchaOptions);
        }

        private void GenerateConfig()
        {
            // 生成配置
            var wrapper = new CaptchaOptionsWrapper { CaptchaOptions = this.captchaOptions };
            var json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);
            var jsonHtml = File.ReadAllText(tpl_File);
            jsonHtml = jsonHtml.Replace("{{data}}", json);
            File.WriteAllText(config_File, jsonHtml);
            // 加载页面
            WebBrowser.ScriptErrorsSuppressed = false; //禁用错误脚本提示  
            WebBrowser.IsWebBrowserContextMenuEnabled = true; // 禁用右键菜单  
            WebBrowser.WebBrowserShortcutsEnabled = true; //禁用快捷键  
            WebBrowser.AllowWebBrowserDrop = false; // 禁止文件拖动  
            WebBrowser.Navigate(config_File);
        }

        private void RenderCaptcha()
        {
            // 设定宽高
            this.CaptchaPbx.Width = this.captchaOptions.ImageOption.Width;
            this.CaptchaPbx.Height = this.captchaOptions.ImageOption.Height;

            // 第一次比较慢，之后会很快
            captchaId = Guid.NewGuid().ToString();
            var data = captchaService.Generate(captchaId, 10);
            CaptchaPbx.Image = Image.FromStream(new MemoryStream(data.Bytes));
            currentCaptchaBytes = data.Bytes;
            Code_Lbl.Text = data.Code;

            // 生成全字体
            foreach (var fontFamily in FontFamilyOptionProvider.Provide())
            {
                var option = this.GenerateCaptchaOptions();
                option.ImageOption.Width = 98;
                option.ImageOption.Height = 35;
                option.ImageOption.FontFamily = fontFamily.Text;
                var service = this.GenerateCaptchaService(option);

                var id = Guid.NewGuid().ToString();
                data = service.Generate(id, 10);
                var pictureBox = this.fontPictureBoxMap[fontFamily.Text];
                pictureBox.Image = Image.FromStream(new MemoryStream(data.Bytes));
            }
        }

        /// <summary>
        /// 生成验证码图
        /// </summary>
        private void GenerateCaptcha()
        {
            this.captchaService = GenerateCaptchaService();
            this.RenderCaptcha();
            this.GenerateConfig();
        }

        private void CaptchaPbx_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void CpatchaType_Cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void FontFamily_Cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void FontSize_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void Length_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void BubbleCount_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void BubbleThickness_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void BubbleMinRadius_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void BubbleMaxRadius_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void InterferenceLineCount_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void Gif_Cbx_CheckedChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
            this.FrameDelay_Ndp.Enabled = this.Gif_Cbx.Checked;
        }

        private void FrameDelay_Ndp_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void Width_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void Height_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void Quality_Nud_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void TextBold_Cbx_CheckedChanged(object sender, EventArgs e)
        {
            this.GenerateCaptcha();
        }

        private void FetchSize_Btn_Click(object sender, EventArgs e)
        {
            var bytesCount = currentCaptchaBytes.Count();
            var size = (bytesCount / 1000.0);
            MessageBox.Show($"{size}kb");
        }

        private void Test_Btn_Click(object sender, EventArgs e)
        {
            this.Test_Btn.Enabled = false;
            var test = new PerformanceTest(this.captchaOptions, 1000);
            test.Complete += Test_Complete;
            test.Progress += Test_Progress;
            test.Start();
        }

        private void Test_Progress(int obj)
        {
            UIHelper.Invoke(this, () =>
            {
                this.Progress_Lbl.Text = $"%{obj}";
            });
        }

        private void Test_Complete(int loopCount, long elapsedMilliseconds, long dataSize)
        {
            var perConsum = Math.Round(elapsedMilliseconds * 1.0 / loopCount, 1);
            var oneSecondsCount = (int)(1000 / perConsum);

            UIHelper.Invoke(this, () =>
            {
                this.Test_Btn.Enabled = true;
            });
            UIHelper.ShowMessageBox(this, $" 总计生成{loopCount}个\r\n 总计耗时{elapsedMilliseconds}毫秒\r\n 平均每个耗时{perConsum}毫秒\r\n 每秒可生成{oneSecondsCount}个\r\n 图片总计大小{UnitHelper.FormatFileSize(dataSize)}");
        }
    }
}
