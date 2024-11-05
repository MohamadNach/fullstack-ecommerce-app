using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IUserAddressRepository : IBaseRepository<UserAddress>
    {
        Task<IEnumerable<UserAddress>> GetUserAddressesByUserIdAsync(Guid userId);
        Task<UserAddress> GetDefaultUserAddressAsync(Guid userId);
    }
}