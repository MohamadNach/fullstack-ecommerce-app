using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ShipmentService
{
    public class ShipmentReadDto : BaseReadDto<Shipment>
    {
        public DateTime ShipmentDate { get; set; }

        public Guid OrderId { get; set; }
        public Guid AddressId { get; set; }

        public override void FromEntity(Shipment entity)
        {
            base.FromEntity(entity);
            ShipmentDate = entity.ShipmentDate;
            OrderId = entity.Order.Id;
            AddressId = entity.Address.Id;
        }
    }
    public class ShipmentCreateDto : ICreateDto<Shipment>
    {
        public DateTime ShipmentDate { get; set; }
        public Shipment CreateEntity()
        {
            return new Shipment
            {
                ShipmentDate = ShipmentDate,
            };
        }
    }
    public class ShipmentUpdateDto : IUpdateDto<Shipment>
    {
        public Guid Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public Shipment UpdateEntity(Shipment entity)
        {
            entity.ShipmentDate = ShipmentDate;
            return entity;
        }
    }
}