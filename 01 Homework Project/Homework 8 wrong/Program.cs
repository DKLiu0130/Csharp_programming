using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using System.Linq.Expressions;


namespace Homework_5
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        // EF6 需要使用字符串名称作为连接字符串，而不是 DbContextOptions<T>
        public OrderDbContext() : base("new_one") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EF6 中使用 DbModelBuilder 而不是 ModelBuilder
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId);
        }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        public decimal TotalAmount => OrderDetails.Sum(od => od.Amount);

        public Order(int orderId, string customerName)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDetails = new List<OrderDetails>();
        }

        public override bool Equals(object obj)
        {
            return obj is Order order && OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + OrderId.GetHashCode();
                hash = hash * 23 + (CustomerName?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"订单号: {OrderId}, 客户: {CustomerName}, 总金额: {TotalAmount:C}";
        }
    }

    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public OrderDetails(string productName, decimal amount)
        {
            ProductName = productName;
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details && ProductName == details.ProductName && Amount == details.Amount;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (ProductName?.GetHashCode() ?? 0);
                hash = hash * 23 + Amount.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{ProductName} - {Amount:C}";
        }
    }

    public class OrderService
    {
        private readonly OrderDbContext _context;
        public OrderService()
        {
            _context = new OrderDbContext(); // 使用默认构造函数
        }
        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            if (_context.Orders.Any(o => o.OrderId == order.OrderId))
            {
                throw new Exception("订单已存在");
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            var order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("订单不存在");
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        // 修改订单
        public void UpdateOrder(int orderId, string customerName)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("订单不存在");
            }
            order.CustomerName = customerName;
            _context.SaveChanges();
        }

        // 查询订单
        public List<Order> QueryOrders(Func<Order, bool> predicate)
        {
            return _context.Orders.Where(predicate).OrderBy(o => o.TotalAmount).ToList();
        }

        // 排序订单（默认按订单号排序，也可自定义排序）
        public List<Order> SortOrders(Expression<Func<Order, object>> orderBy = null)
        {
            var query = _context.Orders.AsQueryable();
            query = orderBy == null ? query.OrderBy(o => o.OrderId) : query.OrderBy(orderBy);
            return query.ToList(); // EF6 不能直接更新排序，需返回结果
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            
        }
    }
}
