using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserService
{
    public interface IUserManagement : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        Task<UserReadDto?> GetUserByEmailAsync(string email);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);
    }
}