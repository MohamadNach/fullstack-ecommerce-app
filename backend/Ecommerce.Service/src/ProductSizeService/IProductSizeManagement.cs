using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductSizeService
{
    public interface IProductSizeManagement : IBaseService<ProductSize, ProductSizeReadDto, ProductSizeCreateDto, ProductSizeUpdateDto>
    {
        Task<IEnumerable<ProductSizeReadDto>> GetSizesByProductIdAsync(Guid productId);
        Task<ProductSize> GetSizeByValueAsync(SizeValue sizeValue);

    }
}