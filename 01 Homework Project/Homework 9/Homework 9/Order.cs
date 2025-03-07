using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Homework_9.Models
{
    // 订单主表：记录订单的核心信息
    public class Order
    {
        // 订单的唯一编号，数据库会自动生成
        [Key]
        public int OrderId { get; set; }

        // 客户姓名（必填项，最多100个字）
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        // 下单时间，记录到具体时分秒
        public DateTime OrderDate { get; set; }

        // 订单总金额（例如：100.50元）
        // 这里特别指定了数据库存储格式：总共10位数字，包含2位小数
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        // 当前订单包含的所有商品明细
        // 初始化时会自动创建一个空列表，避免出现null
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }

    // 订单明细表：记录订单中的具体商品信息
    public class OrderDetail
    {
        // 明细项的唯一编号
        [Key]
        public int DetailId { get; set; }

        // 关联的订单编号（对应Order表的OrderId）
        public int OrderId { get; set; }

        // 商品名称（必填项，最多100个字）
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        // 购买数量（比如：买了3件钢笔）
        public int Quantity { get; set; }

        // 商品单价
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        // 关联的订单对象（注！！EF Core用这个属性建立表关联）
        // 加了JsonIgnore因为debug时返回数据时出现循环引用问题
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}