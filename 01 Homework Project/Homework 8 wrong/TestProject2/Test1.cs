using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework_5;

namespace TestProject1
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService orderService = null!;  // 防止 null 相关的警告

        [TestInitialize]
        public void Setup()
        {
            orderService = new OrderService();  // 正确初始化 orderService
        }

        [TestMethod]
        public void Test_AddOrder_ShouldSucceed_WhenOrderIsNew()
        {
            var order = new Order(1, "张三");
            order.OrderDetails.Add(new OrderDetails("苹果", 100));

            orderService.AddOrder(order);
            var allOrders = orderService.GetAllOrders();

            Assert.AreEqual(1, allOrders.Count);
            Assert.AreEqual("张三", allOrders[0].CustomerName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_AddOrder_ShouldThrowException_WhenOrderExists()
        {
            var order = new Order(1, "张三");
            orderService.AddOrder(order);
            orderService.AddOrder(order);  // 第二次添加相同的订单应抛出异常
        }

        [TestMethod]
        public void Test_RemoveOrder_ShouldSucceed_WhenOrderExists()
        {
            var order = new Order(1, "张三");
            orderService.AddOrder(order);
            orderService.RemoveOrder(1);

            var allOrders = orderService.GetAllOrders();
            Assert.AreEqual(0, allOrders.Count);  // 删除后订单数应为0
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_RemoveOrder_ShouldThrowException_WhenOrderNotExists()
        {
            orderService.RemoveOrder(1);  // 尝试删除不存在的订单应抛出异常
        }

        [TestMethod]
        public void Test_UpdateOrder_ShouldModifyCustomerName()
        {
            var order = new Order(1, "张三");
            orderService.AddOrder(order);
            orderService.UpdateOrder(1, "李四");

            var updatedOrder = orderService.GetAllOrders().First();
            Assert.AreEqual("李四", updatedOrder.CustomerName);  // 更新后的客户名应为 "李四"
        }

        [TestMethod]
        public void Test_SortOrders_ShouldSortByTotalAmount()
        {
            var order1 = new Order(1, "张三");
            order1.OrderDetails.Add(new OrderDetails("商品A", 200));

            var order2 = new Order(2, "李四");
            order2.OrderDetails.Add(new OrderDetails("商品B", 100));

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            orderService.SortOrders(o => o.TotalAmount);
            var sortedOrders = orderService.GetAllOrders();

            Assert.AreEqual(order2, sortedOrders[0]);  // 订单2的总金额小，应排在前面
        }
    }
}
