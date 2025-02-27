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

namespace windows_app_practice
{
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please input valid name and email!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DatabaseHelper dbHelper = new DatabaseHelper();
            bool symbol = dbHelper.CheckUsers(name, email);
            if (symbol)
            {
                Mainpage mainpage1 = new Mainpage();
                mainpage1.Show();
                this.Hide();
            }
            else
            {

            }
        }
    }
}

