using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(Guid productId);
        Task<int> GetTotalQuantityByOrderIdAsync(Guid orderId);
    }
}