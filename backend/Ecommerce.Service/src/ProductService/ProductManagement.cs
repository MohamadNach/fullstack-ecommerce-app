using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductService
{
    public class ProductManagement : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductManagement
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public ProductManagement(IProductRepository productRepository, ICategoryRepository categoryRepository, IOrderItemRepository orderItemRepository) : base(productRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(c => c.Id == categoryId);
                if (category == null)
                    throw new ArgumentException("Invalid category.");

                var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
                var productDtos = products.Select(product =>
                {
                    var dto = new ProductReadDto();
                    dto.FromEntity(product);
                    return dto;
                });

                return productDtos;
            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Products!.");
            }

        }

        public async Task<IEnumerable<ProductReadDto>> SearchProductsByTitleAsync(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Product title is required.");

                var products = await _productRepository.GetProductsByTitleAsync(title);
                var productDtos = products.Select(product =>
                {
                    var dto = new ProductReadDto();
                    dto.FromEntity(product);
                    return dto;
                });

                return productDtos;
            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Products!.");
            }
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            try
            {
                if (minPrice < 0)
                    throw new ArgumentException("Minimum price cannot be negative.", nameof(minPrice));

                if (maxPrice < 0)
                    throw new ArgumentException("Maximum price cannot be negative.", nameof(maxPrice));

                if (minPrice > maxPrice)
                    throw new ArgumentException("Minimum price cannot be greater than maximum price.");

                var products = await _productRepository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
                var productDtos = products.Select(product =>
                {
                    var dto = new ProductReadDto();
                    dto.FromEntity(product);
                    return dto;
                });

                return productDtos;
            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Products!.");
            }
        }

        public async Task<IEnumerable<ProductReadDto>> GetTopSellingProductsAsync(int count)
        {
            try
            {
                if (count <= 0)
                    throw new ArgumentException("Count must be a positive integer.", nameof(count));

                var products = await _productRepository.GetTopSellingProductsAsync(count);
                var productDtos = products.Select(product =>
                {
                    var dto = new ProductReadDto();
                    dto.FromEntity(product);
                    return dto;
                });

                return productDtos;
            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Products!.");
            }
        }

        public async Task<IEnumerable<Product>> GetInStockProductsAsync()
        {
            try
            {
                return await _productRepository.GetInStockProductsAsync();

            }
            catch (Exception)
            {
                throw new Exception("Error Retrieving Stock count!.");
            }
        }
    }
}