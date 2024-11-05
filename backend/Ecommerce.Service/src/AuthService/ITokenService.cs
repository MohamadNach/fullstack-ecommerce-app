using Ecommerce.Domain.src.Auth;

namespace Ecommerce.Service.src.AuthService
{
    public interface ITokenService
    {
        string GenerateToken(Token token);
    }
}