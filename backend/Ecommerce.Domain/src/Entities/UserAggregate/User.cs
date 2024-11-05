using System.ComponentModel.DataAnnotations;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.PaymentAggregate;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Entities.UserAggregate
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "First name must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Last name must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        public byte[] Salt { get; set; } = new byte[100];

        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        // Navigation Properties
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<PaymentMethod>? PaymentMethods { get; set; }

    }
}