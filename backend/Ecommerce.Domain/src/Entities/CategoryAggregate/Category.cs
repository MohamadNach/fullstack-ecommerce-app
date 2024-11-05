using System.ComponentModel.DataAnnotations;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.CategoryAggregate
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        public Guid? ParentCategoryId { get; set; }

        // Navigation properties
        public ICollection<Product>? Products { get; set; }

        public Category? ParentCategory { get; set; }

        // Navigation property for children
        public List<Category> SubCategories { get; set; } = [];

    }
}