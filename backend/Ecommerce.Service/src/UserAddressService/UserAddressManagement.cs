using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserAddressService
{
    public class UserAddressManagement : BaseService<UserAddress, UserAddressReadDto, UserAddressCreateDto, UserAddressUpdateDto>, IUserAddressManagement
    {
        private readonly IUserAddressRepository _userAddressRepository;
        public UserAddressManagement(IUserAddressRepository userAddressRepository) : base(userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public async Task<IEnumerable<UserAddressReadDto>> GetUserAddressesByUserIdAsync(Guid userId)
        {
            var userAddresses = await _userAddressRepository.GetUserAddressesByUserIdAsync(userId);

            if (userAddresses == null)
            {
                return Enumerable.Empty<UserAddressReadDto>();
            }

            var userAddressesDto = new List<UserAddressReadDto>();
            foreach (var userAddress in userAddresses)
            {
                var userAddressDto = new UserAddressReadDto();
                userAddressDto.FromEntity(userAddress);
                userAddressesDto.Add(userAddressDto);
            }

            return userAddressesDto;
        }

        public async Task<UserAddressReadDto> GetDefaultUserAddressAsync(Guid userId)
        {
            var userAddress = await _userAddressRepository.GetDefaultUserAddressAsync(userId);
            if (userAddress == null)
            {
                // return empty Dto
                return new UserAddressReadDto();
            }
            var userAddressDto = new UserAddressReadDto();
            userAddressDto.FromEntity(userAddress);

            return userAddressDto;
        }
    }
}