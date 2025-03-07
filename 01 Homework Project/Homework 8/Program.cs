// OrderDbContext.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Homework_8
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = null!;

        public List<OrderDetail> OrderDetails { get; set; } = new();

        [NotMapped]
        public decimal TotalAmount => OrderDetails.Sum(od => od.Amount);
    }

    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }

    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;port=3306;database=Orderdb_new;user=root;password=112233aabb;";
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure(3)
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(o => o.OrderDetails)
                    .WithOne(d => d.Order)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }

    public class OrderService : IDisposable
    {
        private readonly OrderDbContext _context;

        public OrderService()
        {
            _context = new OrderDbContext();
            _context.Database.EnsureCreated();
        }

        public List<Order> GetAllOrders() =>
            _context.Orders.Include(o => o.OrderDetails).AsNoTracking().ToList();

        public List<Order> GetOrdersByCustomerName(string customerName) =>
            _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.CustomerName.Contains(customerName))
                .AsNoTracking()
                .ToList();

        public void AddOrder(Order order)
        {
            ValidateOrder(order);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public bool UpdateOrder(int id, Action<Order> updateAction)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return false;

            updateAction(order);
            ValidateOrder(order);

            _context.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }

        private void ValidateOrder(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.CustomerName))
                throw new ArgumentException("客户名称不能为空");

            if (!order.OrderDetails.Any())
                throw new ArgumentException("订单必须包含至少一个明细");
        }

        public void Dispose() => _context.Dispose();
    }

    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 初始化应用程序配置
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 配置高DPI设置（仅限 .NET Core 3.1+/ .NET 5+）
            //ApplicationConfiguration.Initialize();

            // 创建并运行主窗体
            Application.Run(new Form1());
        }
    }

}