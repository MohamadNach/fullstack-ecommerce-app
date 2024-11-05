using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.AddressService
{
    public interface IAddressManagement : IBaseService<Address, AddressReadDto, AddressCreateDto, AddressUpdateDto>
    {
    }
}
