using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository

{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderItems.AsNoTracking()
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(Guid productId)
        {
            return await _context.OrderItems.AsNoTracking()
                .Where(oi => oi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<int> GetTotalQuantityByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderItems.AsNoTracking()
                .Where(oi => oi.OrderId == orderId)
                .SumAsync(oi => oi.Quantity);
        }
    }
}
