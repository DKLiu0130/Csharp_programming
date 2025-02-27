using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_app_practice;

//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.DnnSuperres;

namespace windows_app_practice
{
    public partial class Register: Form
    {
        private string imagePath = "";

        public Register()
        {
            InitializeComponent();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.SelectedIndex = 0;
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string gender = cmbGender.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("请填写所有信息并上传图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.InsertUser(name, email);
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;

                pbImage.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
