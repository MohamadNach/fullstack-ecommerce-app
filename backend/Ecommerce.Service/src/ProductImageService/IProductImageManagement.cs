using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductImageService
{
    public interface IProductImageManagement : IBaseService<ProductImage, ProductImageReadDto, ProductImageCreateDto, ProductImageUpdateDto>
    {
        Task<IEnumerable<ProductImageReadDto>> GetImagesByProductIdAsync(Guid productId);
        Task<int> GetImageCountByProductIdAsync(Guid productId);
        Task<bool> DeleteImagesByProductIdAsync(Guid productId);
        Task<ProductImageReadDto> GetMainImageForProductAsync(Guid productId);
    }
}