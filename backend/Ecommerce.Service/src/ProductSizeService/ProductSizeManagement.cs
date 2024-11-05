using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductSizeService
{
    public class ProductSizeManagement : BaseService<ProductSize, ProductSizeReadDto, ProductSizeCreateDto, ProductSizeUpdateDto>, IProductSizeManagement
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IProductRepository _productRepository;

        public ProductSizeManagement(IProductSizeRepository productSizeRepository, IProductRepository productRepository) : base(productSizeRepository)
        {
            _productSizeRepository = productSizeRepository;
            _productRepository = productRepository;
        }


        public async Task<IEnumerable<ProductSizeReadDto>> GetSizesByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                var productSizes = await _productSizeRepository.GetSizesByProductIdAsync(productId);

                var productSizeDtos = productSizes.Select(Size =>
                {
                    var dto = new ProductSizeReadDto();
                    dto.FromEntity(Size);
                    return dto;
                });

                return productSizeDtos;
            }
            catch (Exception)
            {
                throw new Exception($"Error retrieving Product Sizes!.");
            }
        }

        public async Task<ProductSize> GetSizeByValueAsync(SizeValue sizeValue)
        {
            try
            {
                var size = await _productSizeRepository.GetSizeByValueAsync(sizeValue);
                return size;
            }
            catch (Exception)
            {
                throw new Exception($"Error retrieving Product Size!.");
            }
        }
    }
}