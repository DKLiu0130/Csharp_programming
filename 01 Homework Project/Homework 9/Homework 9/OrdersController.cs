using Microsoft.AspNetCore.Mvc;
using Homework_9.Models;
using Homework_9.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Homework_9.Controllers
{
    // 这个类处理所有和订单相关的HTTP请求
    // 访问地址统一是 /api/Orders 开头
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        // 订单服务对象，用来处理具体的业务逻辑
        // 比如添加订单、删除订单这些操作
        private readonly OrderService _orderService;

        // 构造函数：系统会自动注入订单服务实例
        // 不需要我们手动创建这个对象
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 获取所有订单的清单
        // 访问方式：GET /api/Orders
        [HttpGet]
        [SwaggerOperation(
            OperationId = "GETALL",
            Summary = "获取所有订单",
            Description = "返回系统中所有订单的完整列表")]
        public IActionResult GetAll()
        {
            // 调用服务层获取数据
            var orders = _orderService.GetOrders().ToList();

            // 返回200状态码和订单列表
            return Ok(orders);
        }

        // 根据订单编号查询单个订单
        // 访问方式：GET /api/Orders/1 （最后的数字是订单ID）
        [HttpGet("{id}")]
        [SwaggerOperation(
            OperationId = "GETBYID",
            Summary = "通过ID获取订单",
            Description = "根据订单ID获取单个订单的详细信息")]
        public IActionResult GetById(int id)
        {
            // 在服务层的数据中查找匹配的订单
            var order = _orderService.GetOrders()
                .FirstOrDefault(o => o.OrderId == id);

            // 如果找不到返回404错误，找到则返回订单详情
            return order == null ? NotFound() : Ok(order);
        }

        // 创建新订单
        // 访问方式：POST /api/Orders + 请求体中带订单JSON数据
        [HttpPost]
        [SwaggerOperation(
            OperationId = "CREATE",
            Summary = "创建新订单",
            Description = "添加一个新的订单到系统")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            // 调用服务层添加订单，并等待操作完成
            var createdOrder = await _orderService.AddOrder(order);

            // 返回201创建成功状态码，并在响应头中携带新订单的访问地址
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.OrderId }, createdOrder);
        }

        // 更新已有订单
        // 访问方式：PUT /api/Orders/1 + 请求体中带更新后的订单数据
        [HttpPut("{id}")]
        [SwaggerOperation(
            OperationId = "UPDATE",
            Summary = "更新订单",
            Description = "根据ID更新现有订单信息")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            // 调用服务层更新订单
            var updatedOrder = await _orderService.UpdateOrder(id, order);

            // 如果更新失败（比如ID不存在）返回404，成功则返回更新后的数据
            return updatedOrder == null ? NotFound() : Ok(updatedOrder);
        }

        // 删除订单
        // 访问方式：DELETE /api/Orders/1 
        [HttpDelete("{id}")]
        [SwaggerOperation(
            OperationId = "DELETE",
            Summary = "删除订单",
            Description = "根据ID从系统中删除订单")]
        public async Task<IActionResult> Delete(int id)
        {
            // 调用服务层尝试删除订单
            var success = await _orderService.DeleteOrder(id);

            // 删除成功返回204（无内容），失败返回404
            return success ? NoContent() : NotFound();
        }

        // 高级搜索
        // 访问方式：GET /api/Orders/search?customer=张&product=笔&minAmount=100
        // 参数都是可选的，可以自由组合
        [HttpGet("search")]
        [SwaggerOperation(
            OperationId = "SEARCH",
            Summary = "搜索订单",
            Description = "根据客户名称、商品名称和最小金额筛选订单")]
        public IActionResult Search(
            [FromQuery] string? customer,   // 客户名称包含的关键词
            [FromQuery] string? product,    // 商品名称包含的关键词
            [FromQuery] decimal? minAmount) // 订单总金额最小值
        {
            // 调用服务层获取经过筛选的订单列表
            var orders = _orderService.GetOrders(customer, product, minAmount).ToList();

            // 返回筛选结果
            return Ok(orders);
        }
    }
}