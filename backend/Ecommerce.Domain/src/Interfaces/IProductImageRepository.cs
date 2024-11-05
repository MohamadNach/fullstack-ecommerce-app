using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Entities.ProductAggregate;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(Guid productId);
        Task<ProductImage?> GetMainImageForProductAsync(Guid productId);
        Task<int> GetImageCountByProductIdAsync(Guid productId);
        Task<bool> DeleteImagesByProductIdAsync(Guid productId);
    }
}