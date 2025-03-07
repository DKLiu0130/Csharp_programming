using Microsoft.EntityFrameworkCore;
using Homework_9.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Homework_9.Data
{
    // 数据库上下文类：负责和MySQL数据库的交互
    // 这里主要做两件事：
    // 1. 告诉程序如何连接数据库
    // 2. 定义数据库表之间的关系
    public class OrderDbContext : DbContext
    {
        // 构造函数：接收数据库连接配置参数
       
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        // 对应数据库里的订单表（orders表）
        // 通过这个可以查询、添加、修改订单数据
        public DbSet<Order> Orders { get; set; }

        // 对应数据库里的订单明细表（orderdetails表）
        // 用这个操作商品明细数据
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 设置订单和明细之间的关系：
            // 一个订单（Order）可以有多个明细（OrderDetails）
            // 每个明细属于一个订单
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)  // 一个订单有很多明细
                .WithOne(d => d.Order)         // 每个明细属于一个订单
                .HasForeignKey(d => d.OrderId) // 明细表用OrderId字段关联订单表
                .OnDelete(DeleteBehavior.Cascade); // 重要：删除订单时，自动删除关联的所有明细
                                                   // （注意：实际项目中要谨慎使用级联删除）
        }
    }
}