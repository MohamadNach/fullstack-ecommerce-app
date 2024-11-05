using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.ProductAggregate
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ProductTitle { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be equal or greater than 0.")]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string BrandName { get; set; } = string.Empty;



        // Navigation property
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<ProductSize>? ProductSizes { get; set; }
        public virtual ICollection<ProductColor>? ProductColors { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }

        // public Product()
        // {

        // }
        public bool IsInStock()
        {
            return Quantity > 0;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Quantity + quantity < 0)
            {
                throw new ArgumentException("Insufficient stock.");
            }
            Quantity += quantity;
        }
    }
}