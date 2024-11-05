using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.PaymentAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.PaymentAggregate
{
    public class Payment : BaseEntity
    {
        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [ForeignKey("PaymentMethod")]
        public Guid PaymentMethodId { get; set; }

        [Required]
        [Column(TypeName = "decimal(20,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public PaymentStatus PaymentStatus { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Order? Order { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    }
}