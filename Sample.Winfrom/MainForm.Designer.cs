namespace Sample.Winfrom
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CaptchaCodeTbx = new System.Windows.Forms.TextBox();
            this.CaptchaPbx = new System.Windows.Forms.PictureBox();
            this.ValidateBtn = new System.Windows.Forms.Button();
            this.CaptchaPbx2 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPbx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPbx2)).BeginInit();
            this.SuspendLayout();
            // 
            // CaptchaCodeTbx
            // 
            this.CaptchaCodeTbx.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaptchaCodeTbx.Location = new System.Drawing.Point(49, 38);
            this.CaptchaCodeTbx.Name = "CaptchaCodeTbx";
            this.CaptchaCodeTbx.Size = new System.Drawing.Size(257, 35);
            this.CaptchaCodeTbx.TabIndex = 0;
            // 
            // CaptchaPbx
            // 
            this.CaptchaPbx.Location = new System.Drawing.Point(325, 38);
            this.CaptchaPbx.Name = "CaptchaPbx";
            this.CaptchaPbx.Size = new System.Drawing.Size(98, 35);
            this.CaptchaPbx.TabIndex = 1;
            this.CaptchaPbx.TabStop = false;
            // 
            // ValidateBtn
            // 
            this.ValidateBtn.Location = new System.Drawing.Point(211, 92);
            this.ValidateBtn.Name = "ValidateBtn";
            this.ValidateBtn.Size = new System.Drawing.Size(95, 35);
            this.ValidateBtn.TabIndex = 2;
            this.ValidateBtn.Text = "校验";
            this.ValidateBtn.UseVisualStyleBackColor = true;
            this.ValidateBtn.Click += new System.EventHandler(this.ValidateBtn_Click);
            // 
            // CaptchaPbx2
            // 
            this.CaptchaPbx2.Location = new System.Drawing.Point(325, 165);
            this.CaptchaPbx2.Name = "CaptchaPbx2";
            this.CaptchaPbx2.Size = new System.Drawing.Size(98, 35);
            this.CaptchaPbx2.TabIndex = 3;
            this.CaptchaPbx2.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "循环演示";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 212);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CaptchaPbx2);
            this.Controls.Add(this.ValidateBtn);
            this.Controls.Add(this.CaptchaPbx);
            this.Controls.Add(this.CaptchaCodeTbx);
            this.Name = "MainForm";
            this.Text = "验证码演示";
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPbx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPbx2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CaptchaCodeTbx;
        private System.Windows.Forms.PictureBox CaptchaPbx;
        private System.Windows.Forms.Button ValidateBtn;
        private System.Windows.Forms.PictureBox CaptchaPbx2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}

