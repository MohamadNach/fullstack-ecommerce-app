using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.OrderService
{
    public interface IOrderManagement : IBaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {
        Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<IEnumerable<OrderReadDto>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<OrderReadDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalPriceByOrderIdAsync(Guid orderId);
    }
}
