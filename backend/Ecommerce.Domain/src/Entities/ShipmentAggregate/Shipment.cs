using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.ShipmentAggregate
{
    public class Shipment : BaseEntity
    {
        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ShipmentDate { get; set; }

        [Required]
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        [Required]
        public ShippingStatus ShipmentStatus { get; set; }

        // Navigation properties
        public Order? Order { get; set; }
        public Address? Address { get; set; }
    }
}