using Asp.Versioning;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Service.src.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class OrderController : AppController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {
        private readonly IOrderManagement _orderManagement;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderManagement orderManagement, ILogger<OrderController> logger) : base(orderManagement, logger)
        {
            _orderManagement = orderManagement;
            _logger = logger;
        }

        // GET: api/v1/order/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId(Guid userId)
        {
            try
            {
                var orders = await _orderManagement.GetOrdersByUserIdAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting orders for user {userId}");
                return StatusCode(500, "Error retrieving orders.");
            }
        }

        // GET: api/v1/order/status/{status}
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatus status)
        {
            try
            {
                var orders = await _orderManagement.GetOrdersByStatusAsync(status);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting orders by status {status}");
                return StatusCode(500, "Error retrieving orders.");
            }
        }

        // GET: api/v1/order/date-range
        [HttpGet("date-range")]
        public async Task<IActionResult> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var orders = await _orderManagement.GetOrdersByDateRangeAsync(startDate, endDate);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting orders by date range: {startDate} to {endDate}");
                return StatusCode(500, "Error retrieving orders.");
            }
        }

        // GET: api/v1/order/total-price/{orderId}
        [HttpGet("total-price/{orderId}")]
        public async Task<IActionResult> GetTotalPriceByOrderId(Guid orderId)
        {
            try
            {
                var totalPrice = await _orderManagement.GetTotalPriceByOrderIdAsync(orderId);
                return Ok(new { OrderId = orderId, TotalPrice = totalPrice });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Order not found: {orderId}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting total price for order {orderId}");
                return StatusCode(500, "Error retrieving order total price.");
            }
        }
    }
}
