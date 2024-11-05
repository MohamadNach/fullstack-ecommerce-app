using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders.AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders.AsNoTracking()
                .Where(o => o.OrderStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders.AsNoTracking()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalPriceByOrderIdAsync(Guid orderId)
        {
            var order = await _context.Orders.AsNoTracking()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) throw new InvalidOperationException("Order not found");

            return order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
        }

        public async Task<int> GetTotalOrdersAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Order> query = _context.Orders;

            if (startDate.HasValue) query = query.Where(o => o.OrderDate >= startDate.Value);
            if (endDate.HasValue) query = query.Where(o => o.OrderDate <= endDate.Value);

            return await query.AsNoTracking().CountAsync();
        }
    }
}
