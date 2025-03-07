// Form1.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Homework_8
{
    public partial class Form1 : Form
    {
        private readonly OrderService _orderService = new OrderService();
        private DataGridViewTextBoxColumn? colOrderId, colCustomer, colProduct, colAmount;

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            RefreshOrderList();
        }

        private void InitializeDataGridView()
        {
            dataGridViewOrders.AutoGenerateColumns = false;
            dataGridViewOrders.Columns.Clear();

            colOrderId = new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "Id",
                HeaderText = "������"
            };
            colCustomer = new DataGridViewTextBoxColumn
            {
                Name = "colCustomer",
                DataPropertyName = "CustomerName",
                HeaderText = "�ͻ�"
            };
            colProduct = new DataGridViewTextBoxColumn
            {
                Name = "colProduct",
                HeaderText = "��Ʒ",
                Width = 150
            };
            colAmount = new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "�ܽ��",
                ValueType = typeof(decimal)
            };

            dataGridViewOrders.Columns.AddRange(
                colOrderId, colCustomer, colProduct, colAmount);
        }

        private void UpdateOrderGrid(IEnumerable<Order> orders)
        {
            dataGridViewOrders.Rows.Clear();

            foreach (var order in orders)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridViewOrders);

                row.Cells[colOrderId!.Index].Value = order.Id;
                row.Cells[colCustomer!.Index].Value = order.CustomerName;
                row.Cells[colProduct!.Index].Value = GetProductsString(order.OrderDetails);
                row.Cells[colAmount!.Index].Value = order.TotalAmount;

                row.Tag = order.Id;  // �洢ID���ں�������
                dataGridViewOrders.Rows.Add(row);
            }
        }

        private string GetProductsString(IEnumerable<OrderDetail> details) =>
            string.Join(", ", details.Select(d => $"{d.ProductName}({d.Amount:C})"));

        private void RefreshOrderList() =>
            UpdateOrderGrid(_orderService.GetAllOrders());

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using var form = new OrderForm("","",0);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newOrder = new Order
                    {
                        CustomerName = form.CustomerName,
                        OrderDetails = new List<OrderDetail>
                        {
                            new OrderDetail
                            {
                                ProductName = form.ProductName,
                                Amount = form.Price
                            }
                        }
                    };

                    _orderService.AddOrder(newOrder);
                    RefreshOrderList();
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            var orderId = (int)dataGridViewOrders.SelectedRows[0].Tag!;
            var order = _orderService.GetAllOrders().First(o => o.Id == orderId);

            // ��ȫ���ʵ�һ����ϸ������������һ����ϸ��
            using var form = new OrderForm(order);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _orderService.UpdateOrder(orderId, updatedOrder =>
                    {
                        updatedOrder.CustomerName = form.CustomerName;
                        updatedOrder.OrderDetails.Clear();
                        updatedOrder.OrderDetails.Add(new OrderDetail
                        {
                            ProductName = form.ProductName,
                            Amount = form.Price
                        });
                    });

                    RefreshOrderList();
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            var orderId = (int)dataGridViewOrders.SelectedRows[0].Tag!;
            if (MessageBox.Show("ȷ��ɾ���ö�����", "ɾ��ȷ��",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _orderService.DeleteOrder(orderId);
                    RefreshOrderList();
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            var results = string.IsNullOrEmpty(keyword)
                ? _orderService.GetAllOrders()
                : _orderService.GetOrdersByCustomerName(keyword);

            UpdateOrderGrid(results);
        }

        private void ShowError(Exception ex) =>
            MessageBox.Show(ex.Message, "����ʧ��",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _orderService.Dispose();
            base.OnFormClosing(e);
        }
    }
}