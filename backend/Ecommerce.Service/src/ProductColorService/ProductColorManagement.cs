using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductColorService
{
    public class ProductColorManagement : BaseService<ProductColor, ProductColorReadDto, ProductColorCreateDto, ProductColorUpdateDto>, IProductColorManagement
    {
        private readonly IProductColorRepository _productColorRepository;
        private readonly IProductRepository _productRepository;

        public ProductColorManagement(IProductColorRepository productColorRepository, IProductRepository productRepository) : base(productColorRepository)
        {
            _productColorRepository = productColorRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductColorReadDto>> GetColorsByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(p => p.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid product.");

                var productColors = await _productColorRepository.GetColorsByProductIdAsync(productId);
                var productColorsDtos = productColors.Select(Color =>
                {
                    var dto = new ProductColorReadDto();
                    dto.FromEntity(Color);
                    return dto;
                });

                return productColorsDtos;
            }
            catch
            {
                throw new Exception("Error Retrieving Product Colors!.");
            }
        }
        public async Task<ProductColorReadDto> GetColorByNameAsync(string colorName)
        {
            try
            {
                if (!Enum.TryParse<ColorName>(colorName, true, out var colorEnum))
                {
                    throw new ArgumentException("Invalid color name.");
                }

                var productColor = await _productColorRepository.GetAsync(pc => pc.ColorName == colorEnum);
                if (productColor == null)
                {
                    throw new ArgumentException("Product color not found.");
                }

                var productColorReadDto = new ProductColorReadDto();
                productColorReadDto.FromEntity(productColor);

                return productColorReadDto;

            }
            catch
            {
                throw new Exception("Error Retrieving Product Color!.");
            }
        }
    }
}