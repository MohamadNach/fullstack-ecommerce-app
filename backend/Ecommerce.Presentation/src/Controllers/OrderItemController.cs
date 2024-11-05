using Asp.Versioning;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Service.src.OrderItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class OrderItemController : AppController<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>
    {
        private readonly IOrderItemManagement _orderItemManagement;

        public OrderItemController(IOrderItemManagement orderItemManagement, ILogger<OrderItemController> logger) : base(orderItemManagement, logger)
        {
            _orderItemManagement = orderItemManagement;
        }

        // GET: api/v1/orderitems/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(Guid orderId)
        {
            var orderItems = await _orderItemManagement.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        // GET: api/v1/orderitems/product/{productId}
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetOrderItemsByProductId(Guid productId)
        {
            var orderItems = await _orderItemManagement.GetOrderItemsByProductIdAsync(productId);
            return Ok(orderItems);
        }

        // GET: api/v1/orderitems/quantity/{orderId}
        [HttpGet("quantity/{orderId}")]
        public async Task<IActionResult> GetTotalQuantityByOrderId(Guid orderId)
        {
            var totalQuantity = await _orderItemManagement.GetTotalQuantityByOrderIdAsync(orderId);
            return Ok(new { OrderId = orderId, TotalQuantity = totalQuantity });
        }
    }
}
