namespace windows_app_practice
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbName = new System.Windows.Forms.Label();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbRegisterAccount = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lbGender = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lbImage = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbName.Location = new System.Drawing.Point(55, 106);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(75, 27);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "Name:";
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbEmail.Location = new System.Drawing.Point(55, 156);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(80, 27);
            this.lbEmail.TabIndex = 1;
            this.lbEmail.Text = "Email: ";
            // 
            // lbRegisterAccount
            // 
            this.lbRegisterAccount.AutoSize = true;
            this.lbRegisterAccount.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.lbRegisterAccount.Location = new System.Drawing.Point(97, 27);
            this.lbRegisterAccount.Name = "lbRegisterAccount";
            this.lbRegisterAccount.Size = new System.Drawing.Size(293, 46);
            this.lbRegisterAccount.TabIndex = 2;
            this.lbRegisterAccount.Text = "Register Account";
            // 
            // cmbGender
            // 
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(138, 207);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(150, 32);
            this.cmbGender.TabIndex = 3;
            
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(138, 98);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 35);
            this.txtName.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(138, 153);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 35);
            this.txtEmail.TabIndex = 5;
            // 
            // lbGender
            // 
            this.lbGender.AutoSize = true;
            this.lbGender.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbGender.Location = new System.Drawing.Point(35, 210);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(95, 27);
            this.lbGender.TabIndex = 6;
            this.lbGender.Text = "Gender: ";
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(138, 269);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(127, 147);
            this.pbImage.TabIndex = 7;
            this.pbImage.TabStop = false;
            // 
            // lbImage
            // 
            this.lbImage.AutoSize = true;
            this.lbImage.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbImage.Location = new System.Drawing.Point(46, 275);
            this.lbImage.Name = "lbImage";
            this.lbImage.Size = new System.Drawing.Size(77, 27);
            this.lbImage.TabIndex = 8;
            this.lbImage.Text = "Image:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.btnBrowse.Location = new System.Drawing.Point(283, 306);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(107, 56);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.btnRegister.Location = new System.Drawing.Point(169, 434);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(131, 56);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 514);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lbImage);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.lbRegisterAccount);
            this.Controls.Add(this.lbEmail);
            this.Controls.Add(this.lbName);
            this.Name = "Register";
            this.Text = "Register";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.Label lbRegisterAccount;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lbImage;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnRegister;
    }
}