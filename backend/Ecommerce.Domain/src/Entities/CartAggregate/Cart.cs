using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.CartAggregate
{
    public class Cart : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }


        // Navigation Property
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }
}