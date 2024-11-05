using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.UserAggregate
{
    public class UserAddress : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        [Required]
        public bool IsDefault { get; set; } = false;

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Address? Address { get; set; }
    }
}