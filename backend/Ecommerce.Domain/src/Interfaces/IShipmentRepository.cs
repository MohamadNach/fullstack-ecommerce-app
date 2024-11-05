using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IShipmentRepository : IBaseRepository<Shipment>
    {
        Task<IEnumerable<Shipment>> GetShipmentsByOrderIdAsync(Guid orderId);
    }
}