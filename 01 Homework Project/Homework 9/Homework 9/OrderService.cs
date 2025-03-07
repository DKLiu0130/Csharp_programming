using Homework_9.Data;
using Homework_9.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework_9.Services
{
    public class OrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        // 添加订单
        public async Task<Order> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // 删除订单（连带明细）
        public async Task<bool> DeleteOrder(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return false;

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        // 更新订单
        public async Task<Order?> UpdateOrder(int orderId, Order updatedOrder)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (existingOrder == null) return null;

            existingOrder.CustomerName = updatedOrder.CustomerName;
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;

            _context.OrderDetails.RemoveRange(existingOrder.OrderDetails);
            existingOrder.OrderDetails = updatedOrder.OrderDetails;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        // 查询订单（支持多种条件）
        public IQueryable<Order> GetOrders(string? customer = null, string? product = null, decimal? minAmount = null)
        {
            var query = _context.Orders
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customer))
                query = query.Where(o => o.CustomerName.Contains(customer));

            if (!string.IsNullOrEmpty(product))
                query = query.Where(o => o.OrderDetails.Any(d => d.ProductName.Contains(product)));

            if (minAmount.HasValue)
                query = query.Where(o => o.TotalAmount >= minAmount.Value);

            return query;
        }
    }
}
