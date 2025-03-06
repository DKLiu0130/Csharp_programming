using classSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OrderManagementWinForms
{
    public partial class Form1 : Form
    {
        private OrderService orderService = new OrderService();

        public Form1()
        {
            InitializeComponent();
            RefreshOrderList();
        }

        // 新增订单
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using (var form = new OrderForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newOrder = new Order
                    {
                        CustomerName = form.CustomerName,
                        OrderDetails = new List<OrderDetails>
                        {
                            new OrderDetails { ProductName = form.ProductName, Amount = form.Price }
                        }
                    };

                    try
                    {
                        orderService.AddOrder(newOrder);
                        RefreshOrderList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 编辑订单
        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一个订单进行编辑", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedOrder = dataGridViewOrders.SelectedRows[0].Tag as Order;
            if (selectedOrder != null)
            {
                var orderDetail = selectedOrder.OrderDetails.FirstOrDefault();
                using (var form = new OrderForm(selectedOrder.CustomerName, orderDetail?.ProductName, orderDetail?.Amount ?? 0))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            orderService.UpdateOrder(selectedOrder.OrderId, form.CustomerName);

                            // 清除原订单详情，添加新详情
                            selectedOrder.OrderDetails.Clear();
                            selectedOrder.OrderDetails.Add(new OrderDetails { ProductName = form.ProductName, Amount = form.Price });

                            orderService.SaveChanges(); // 确保更新数据库
                            RefreshOrderList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // 删除订单
        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一个订单进行删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedOrder = dataGridViewOrders.SelectedRows[0].Tag as Order;
            if (selectedOrder != null)
            {
                var confirmResult = MessageBox.Show($"确定要删除订单 {selectedOrder.OrderId} 吗？",
                    "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        orderService.RemoveOrder(selectedOrder.OrderId);
                        RefreshOrderList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 查询订单
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                RefreshOrderList();
            }
            else
            {
                var results = orderService.QueryOrders(o => o.CustomerName.Contains(searchText));
                UpdateOrderGrid(results);
            }
        }

        // 更新 DataGridView 显示
        private void UpdateOrderGrid(List<Order> orders)
        {
            if (dataGridViewOrders.Columns.Count == 0)
            {
                dataGridViewOrders.Columns.Add("OrderId", "编号");
                dataGridViewOrders.Columns.Add("CustomerName", "姓名");
                dataGridViewOrders.Columns.Add("Product", "产品");
                dataGridViewOrders.Columns.Add("Amount", "价格");
            }

            dataGridViewOrders.Rows.Clear();
            foreach (var order in orders)
            {
                int rowIndex = dataGridViewOrders.Rows.Add(
                    order.OrderId,
                    order.CustomerName,
                    order.OrderDetails.FirstOrDefault()?.ProductName ?? "",
                    order.OrderDetails.FirstOrDefault()?.Amount ?? 0
                );

                // 存储 Order 对象，便于编辑或删除
                dataGridViewOrders.Rows[rowIndex].Tag = order;
            }
        }

        // 刷新订单列表
        private void RefreshOrderList()
        {
            UpdateOrderGrid(orderService.GetAllOrders());
        }
    }
}
