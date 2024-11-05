using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class ShipmentRepository : BaseRepository<Shipment>, IShipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByOrderIdAsync(Guid orderId)
        {
            return await _context.Shipments.AsNoTracking().Where(s => s.Order != null && s.Order.Id == orderId).ToListAsync();
        }

    }
}