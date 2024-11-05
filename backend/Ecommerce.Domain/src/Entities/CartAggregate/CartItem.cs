using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.CartAggregate
{
    public class CartItem : BaseEntity
    {
        [Required]
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be equal or greater than 0.")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        // Navigation Property
        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }

    }
}