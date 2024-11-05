using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ProductService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.ProductAggregate;

namespace Ecommerce.Tests.Service
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IOrderItemRepository> _mockOrderItemRepo;
        private readonly ProductManagement _service;

        public ProductServiceTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockOrderItemRepo = new Mock<IOrderItemRepository>();
            _service = new ProductManagement(_mockProductRepo.Object, _mockCategoryRepo.Object, _mockOrderItemRepo.Object);
        }

        [Fact]
        public async Task GetProductsByCategoryAsync_ShouldReturnProductReadDtos_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId };
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product1" },
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product2" }
            };

            _mockCategoryRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            _mockProductRepo.Setup(r => r.GetProductsByCategoryAsync(categoryId)).ReturnsAsync(products);

            // Act
            var result = await _service.GetProductsByCategoryAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ProductTitle == "Product1");
            Assert.Contains(result, dto => dto.ProductTitle == "Product2");
            _mockCategoryRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
            _mockProductRepo.Verify(r => r.GetProductsByCategoryAsync(categoryId), Times.Once);
        }

        [Fact]
        public async Task GetProductsByCategoryAsync_ShouldThrowArgumentException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _mockCategoryRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync((Category?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetProductsByCategoryAsync(categoryId));
            _mockCategoryRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
            _mockProductRepo.Verify(r => r.GetProductsByCategoryAsync(categoryId), Times.Never);
        }

        [Fact]
        public async Task SearchProductsByTitleAsync_ShouldReturnProductReadDtos_WhenTitleIsValid()
        {
            // Arrange
            var title = "Product";
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product1" },
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product2" }
            };

            _mockProductRepo.Setup(r => r.GetProductsByTitleAsync(title)).ReturnsAsync(products);

            // Act
            var result = await _service.SearchProductsByTitleAsync(title);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ProductTitle == "Product1");
            Assert.Contains(result, dto => dto.ProductTitle == "Product2");
            _mockProductRepo.Verify(r => r.GetProductsByTitleAsync(title), Times.Once);
        }

        [Fact]
        public async Task SearchProductsByTitleAsync_ShouldThrowArgumentException_WhenTitleIsEmpty()
        {
            // Arrange
            var title = string.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.SearchProductsByTitleAsync(title));
            _mockProductRepo.Verify(r => r.GetProductsByTitleAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetProductsByPriceRangeAsync_ShouldReturnProductReadDtos_WhenPriceRangeIsValid()
        {
            // Arrange
            var minPrice = 10m;
            var maxPrice = 50m;
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product1", Price = 20m },
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product2", Price = 30m }
            };

            _mockProductRepo.Setup(r => r.GetProductsByPriceRangeAsync(minPrice, maxPrice)).ReturnsAsync(products);

            // Act
            var result = await _service.GetProductsByPriceRangeAsync(minPrice, maxPrice);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ProductTitle == "Product1");
            Assert.Contains(result, dto => dto.ProductTitle == "Product2");
            _mockProductRepo.Verify(r => r.GetProductsByPriceRangeAsync(minPrice, maxPrice), Times.Once);
        }

        [Fact]
        public async Task GetProductsByPriceRangeAsync_ShouldThrowArgumentException_WhenMinPriceIsGreaterThanMaxPrice()
        {
            // Arrange
            var minPrice = 50m;
            var maxPrice = 10m;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetProductsByPriceRangeAsync(minPrice, maxPrice));
            _mockProductRepo.Verify(r => r.GetProductsByPriceRangeAsync(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never);
        }

        [Fact]
        public async Task GetTopSellingProductsAsync_ShouldReturnTopSellingProductReadDtos_WhenCountIsValid()
        {
            // Arrange
            var count = 5;
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product1" },
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product2" }
            };

            _mockProductRepo.Setup(r => r.GetTopSellingProductsAsync(count)).ReturnsAsync(products);

            // Act
            var result = await _service.GetTopSellingProductsAsync(count);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ProductTitle == "Product1");
            Assert.Contains(result, dto => dto.ProductTitle == "Product2");
            _mockProductRepo.Verify(r => r.GetTopSellingProductsAsync(count), Times.Once);
        }

        [Fact]
        public async Task GetTopSellingProductsAsync_ShouldThrowArgumentException_WhenCountIsNotPositive()
        {
            // Arrange
            var count = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetTopSellingProductsAsync(count));
            _mockProductRepo.Verify(r => r.GetTopSellingProductsAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetInStockProductsAsync_ShouldReturnProducts_WhenProductsAreInStock()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product1" },
                new Product { Id = Guid.NewGuid(), ProductTitle = "Product2" }
            };

            _mockProductRepo.Setup(r => r.GetInStockProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetInStockProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.ProductTitle == "Product1");
            Assert.Contains(result, p => p.ProductTitle == "Product2");
            _mockProductRepo.Verify(r => r.GetInStockProductsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetInStockProductsAsync_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            _mockProductRepo.Setup(r => r.GetInStockProductsAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetInStockProductsAsync());
            _mockProductRepo.Verify(r => r.GetInStockProductsAsync(), Times.Once);
        }
    }
}
