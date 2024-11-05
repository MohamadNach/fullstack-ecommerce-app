
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.AddressService;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserAddressService
{
    public interface IUserAddressManagement : IBaseService<UserAddress, UserAddressReadDto, UserAddressCreateDto, UserAddressUpdateDto>
    {
        Task<IEnumerable<UserAddressReadDto>> GetUserAddressesByUserIdAsync(Guid userId);
        Task<UserAddressReadDto> GetDefaultUserAddressAsync(Guid userId);
    }
}