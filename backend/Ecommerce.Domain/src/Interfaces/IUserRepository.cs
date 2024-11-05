using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Entities.UserAggregate;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);
    }
}