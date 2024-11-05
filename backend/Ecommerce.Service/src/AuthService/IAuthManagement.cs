using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Auth;

namespace Ecommerce.Service.src.AuthService
{
    public interface IAuthManagement
    {
        Task<AuthResponseDto> LoginAsync(UserCredentials userCredentials);
        Task LogoutAsync();

    }
}