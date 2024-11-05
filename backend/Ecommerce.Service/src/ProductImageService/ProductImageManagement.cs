using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductImageService
{
    public class ProductImageManagement : BaseService<ProductImage, ProductImageReadDto, ProductImageCreateDto, ProductImageUpdateDto>, IProductImageManagement
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductRepository _productRepository;

        public ProductImageManagement(IProductImageRepository productImageRepository, IProductRepository productRepository) : base(productImageRepository)
        {
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductImageReadDto>> GetImagesByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                var productImages = await _productImageRepository.GetImagesByProductIdAsync(productId);
                var productImageDtos = productImages.Select(Image =>
                {
                    var dto = new ProductImageReadDto();
                    dto.FromEntity(Image);
                    return dto;
                });

                return productImageDtos;
            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Product Images!.");
            }
        }

        public async Task<ProductImageReadDto> GetMainImageForProductAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                var productMainImage = await _productImageRepository.GetMainImageForProductAsync(productId);

                var productImageReadDto = new ProductImageReadDto();
                productImageReadDto.FromEntity(productMainImage);

                return productImageReadDto;
            }
            catch
            {
                throw new Exception("Error Retrieving Product Image!.");
            }
        }

        public async Task<int> GetImageCountByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                return await _productImageRepository.GetImageCountByProductIdAsync(productId);
            }
            catch
            {
                throw new Exception("Error Retrieving the Image count!.");
            }
        }

        public async Task<bool> DeleteImagesByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                return await _productImageRepository.DeleteImagesByProductIdAsync(productId);
            }
            catch
            {
                throw new Exception("Error Deleting images!.");
            }
        }
    }
}