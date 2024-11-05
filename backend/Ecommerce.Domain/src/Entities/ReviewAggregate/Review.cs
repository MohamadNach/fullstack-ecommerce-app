using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.ReviewAggregate
{
    public class Review : BaseEntity
    {
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ReviewDate { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(255)]
        public string ReviewText { get; set; } = string.Empty;

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }

    }
}