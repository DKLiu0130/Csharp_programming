using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            comboBox1.Items.Add("/");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取 textBox1 和 textBox2 中的数字输入
            double num1, num2;

            // 检查输入是否有效
            if (!double.TryParse(textBox1.Text, out num1) || !double.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("请输入有效的数字！");
                return;
            }

            // 获取 comboBox1 中选择的运算符
            string operation = comboBox1.SelectedItem.ToString();

            double result = 0;

            // 根据选择的运算符进行计算
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        MessageBox.Show("除数不能为零！");
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("请选择一个有效的运算符！");
                    return;
            }

            // 显示计算结果
            textBox3.Text = result.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
