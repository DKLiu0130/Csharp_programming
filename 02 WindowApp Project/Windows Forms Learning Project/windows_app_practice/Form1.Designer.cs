namespace windows_app_practice
{
    partial class Form1
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
            this.lbInfo = new System.Windows.Forms.Label();
            this.rbRegister = new System.Windows.Forms.RadioButton();
            this.rbLogin = new System.Windows.Forms.RadioButton();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbInfo.Location = new System.Drawing.Point(44, 47);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(236, 27);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "Hello, register or login?";
            // 
            // rbRegister
            // 
            this.rbRegister.AutoSize = true;
            this.rbRegister.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbRegister.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.rbRegister.Location = new System.Drawing.Point(105, 123);
            this.rbRegister.Name = "rbRegister";
            this.rbRegister.Size = new System.Drawing.Size(131, 32);
            this.rbRegister.TabIndex = 1;
            this.rbRegister.TabStop = true;
            this.rbRegister.Text = "register";
            this.rbRegister.UseVisualStyleBackColor = true;
            // 
            // rbLogin
            // 
            this.rbLogin.AutoSize = true;
            this.rbLogin.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.rbLogin.Location = new System.Drawing.Point(383, 123);
            this.rbLogin.Name = "rbLogin";
            this.rbLogin.Size = new System.Drawing.Size(90, 31);
            this.rbLogin.TabIndex = 2;
            this.rbLogin.TabStop = true;
            this.rbLogin.Text = "login";
            this.rbLogin.UseVisualStyleBackColor = true;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.btnDone.Location = new System.Drawing.Point(237, 190);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(141, 43);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 270);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.rbLogin);
            this.Controls.Add(this.rbRegister);
            this.Controls.Add(this.lbInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.RadioButton rbRegister;
        private System.Windows.Forms.RadioButton rbLogin;
        private System.Windows.Forms.Button btnDone;
    }
}

