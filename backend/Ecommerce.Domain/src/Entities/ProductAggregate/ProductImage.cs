using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.ProductAggregate
{
    public class ProductImage : BaseEntity
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageURL { get; set; } = string.Empty;

        [Required]
        public bool IsDefault { get; set; }

        [MaxLength(200)]
        public string ImageText { get; set; } = string.Empty;

        // Navigation property
        public virtual Product? Product { get; set; }
    }
}