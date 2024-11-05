using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.src.Auth
{
    public class Token
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}