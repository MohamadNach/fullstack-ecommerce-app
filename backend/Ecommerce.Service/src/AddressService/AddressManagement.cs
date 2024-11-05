using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.AddressService
{
    public class AddressManagement : BaseService<Address, AddressReadDto, AddressCreateDto, AddressUpdateDto>, IAddressManagement
    {
        public AddressManagement(IAddressRepository addressRepository) : base(addressRepository)
        {
        }
    }
}
