using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ShipmentService
{
    public class ShipmentManagement : BaseService<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>, IShipmentManagement
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentManagement(IShipmentRepository shipmentRepository) : base(shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<IEnumerable<ShipmentReadDto>> GetShipmentsByOrderIdAsync(Guid orderId)
        {
            try
            {
                var shipments = await _shipmentRepository.GetShipmentsByOrderIdAsync(orderId);
                return shipments.Select(s =>
                {
                    var shipmentDto = new ShipmentReadDto();
                    shipmentDto.FromEntity(s);
                    return shipmentDto;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}