using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        // Navigation Property
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}