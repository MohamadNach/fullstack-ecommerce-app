
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.ProductAggregate
{
    public class ProductColor : BaseEntity
    {
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        public ColorName ColorName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be equal or greater than 0.")]
        public int Quantity { get; set; }


        // Navigation property
        public virtual Product? Product { get; set; }
    }
}