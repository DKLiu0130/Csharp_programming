using Homework_8;
using System;
using System.Windows.Forms;

namespace Homework_8
{
    public partial class OrderForm : Form
    {
        // 公开属性保持不变
        public string CustomerName { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }

        // 新增支持直接传入Order对象的构造函数
        public OrderForm(Order order = null) : this(
            customer: order?.CustomerName ?? "",
            product: (order?.OrderDetails.Count > 0) ? order.OrderDetails[0].ProductName : "",
            price: (order?.OrderDetails.Count > 0) ? order.OrderDetails[0].Amount : 0
        )
        {
        }

        // 保留原有构造函数
        public OrderForm(string customer = "", string product = "", decimal price = 0)
        {
            InitializeComponent();
            InitializeData(customer, product, price);
        }

        private void InitializeData(string customer, string product, decimal price)
        {
            tbcustomer.Text = customer;
            tbproduct.Text = product;
            tbprice.Text = price.ToString("F2");  // 格式化为两位小数
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            // 将 price 提前声明
            decimal price = 0;

            if (string.IsNullOrWhiteSpace(tbcustomer.Text) ||
                string.IsNullOrWhiteSpace(tbproduct.Text) ||
                !decimal.TryParse(tbprice.Text, out price)) // 此处改为 out 已声明的变量
            {
                MessageBox.Show("请填写所有字段，并确保价格是有效的数字。",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 现在可以安全使用 price
            CustomerName = tbcustomer.Text.Trim();
            ProductName = tbproduct.Text.Trim();
            Price = price;

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}