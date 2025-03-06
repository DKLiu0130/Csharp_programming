using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;

namespace classSpace
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext()
            : base("OrderDbContext")  // 从 App.config 读取连接字符串
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public decimal TotalAmount => OrderDetails?.Sum(od => od.Amount) ?? 0;

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public override string ToString()
        {
            return $"订单号: {OrderId}, 客户: {CustomerName}, 总金额: {TotalAmount:C}";
        }
    }

    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
    public class OrderService
    {
        public event Action OrdersUpdated;  // 事件通知 UI 更新

        // 添加订单
        public void AddOrder(Order order)
        {
            using (var context = new OrderDbContext())
            {
                context.Orders.Add(order);
                context.SaveChanges(); // 保存更改
            }
            OrdersUpdated?.Invoke();  // 触发 UI 更新事件
        }

        // 更新订单
        public void UpdateOrder(int orderId, string newCustomerName)
        {
            using (var context = new OrderDbContext())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.CustomerName = newCustomerName;
                    context.SaveChanges(); // 保存更改
                }
            }
            OrdersUpdated?.Invoke();
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            using (var context = new OrderDbContext())
            {
                var order = context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    context.OrderDetails.RemoveRange(order.OrderDetails);
                    context.Orders.Remove(order);
                    context.SaveChanges(); // 保存更改
                }
            }
            OrdersUpdated?.Invoke();
        }

        // 查询所有订单
        public List<Order> GetAllOrders()
        {
            using (var context = new OrderDbContext())
            {
                return context.Orders.Include(o => o.OrderDetails).ToList();
            }
        }

        // 按条件查询订单
        public List<Order> QueryOrders(Func<Order, bool> predicate)
        {
            using (var context = new OrderDbContext())
            {
                return context.Orders.Include(o => o.OrderDetails).Where(predicate).ToList();
            }
        }

        // ✅ 新增 SaveChanges 方法
        public void SaveChanges()
        {
            using (var context = new OrderDbContext())
            {
                context.SaveChanges(); // 确保所有更改都提交到数据库
            }
        }
    }

}
