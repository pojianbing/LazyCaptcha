using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator.Image.Option;
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
        private string CaptchaId = string.Empty;
        private CaptchaService CaptchaService1;
        private CaptchaService CaptchaService2;

        public MainForm()
        {
            InitializeComponent();

            // 初始化验证码服务
            CaptchaService1 = CaptchaServiceBuilder
                .New()
                .Width(98)
                .Height(35)
                .Animation(true)
                .Build();

            CaptchaService2 = CaptchaServiceBuilder
                .New()
                .FontSize(20)
                .Build();

            GenerateCaptcha();
        }

        /// <summary>
        /// 生成验证按
        /// </summary>
        private void GenerateCaptcha()
        {
            // 第一次比较慢，之后会很快
            CaptchaId = Guid.NewGuid().ToString();
            var data = CaptchaService1.Generate(CaptchaId, 10);
            CaptchaPbx.Image = Image.FromStream(new MemoryStream(data.Bytes));
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateBtn_Click(object sender, EventArgs e)
        {
            if (CaptchaService1.Validate(CaptchaId, CaptchaCodeTbx.Text))
            {
                MessageBox.Show("验证通过");
            }
            else
            {
                MessageBox.Show("验证失败");
            }

            GenerateCaptcha();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var data = CaptchaService1.Generate(Guid.NewGuid().ToString(), 10);
            CaptchaPbx2.Image = Image.FromStream(new MemoryStream(data.Bytes));
        }

        private void CaptchaPbx_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
