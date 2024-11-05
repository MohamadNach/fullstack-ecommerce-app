using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ProductSizeService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Domain.src.ProductAggregate;

namespace Ecommerce.Tests.Service
{
    public class ProductSizeServiceTests
    {
        private readonly Mock<IProductSizeRepository> _mockProductSizeRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductSizeManagement _service;

        public ProductSizeServiceTests()
        {
            _mockProductSizeRepo = new Mock<IProductSizeRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _service = new ProductSizeManagement(_mockProductSizeRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task GetSizesByProductIdAsync_ShouldReturnProductSizeReadDtos_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var productSizes = new List<ProductSize>
            {
                new ProductSize { Id = Guid.NewGuid(), SizeValue = SizeValue.Small },
                new ProductSize { Id = Guid.NewGuid(), SizeValue = SizeValue.Medium }
            };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductSizeRepo.Setup(r => r.GetSizesByProductIdAsync(productId)).ReturnsAsync(productSizes);

            // Act
            var result = await _service.GetSizesByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.SizeValue == SizeValue.Small);
            Assert.Contains(result, dto => dto.SizeValue == SizeValue.Medium);
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductSizeRepo.Verify(r => r.GetSizesByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetSizesByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetSizesByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductSizeRepo.Verify(r => r.GetSizesByProductIdAsync(productId), Times.Never);
        }

        [Fact]
        public async Task GetSizeByValueAsync_ShouldReturnProductSize_WhenSizeExists()
        {
            // Arrange
            var sizeValue = SizeValue.Large;
            var productSize = new ProductSize { Id = Guid.NewGuid(), SizeValue = sizeValue };

            _mockProductSizeRepo.Setup(r => r.GetSizeByValueAsync(sizeValue)).ReturnsAsync(productSize);

            // Act
            var result = await _service.GetSizeByValueAsync(sizeValue);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sizeValue, result.SizeValue);
            _mockProductSizeRepo.Verify(r => r.GetSizeByValueAsync(sizeValue), Times.Once);
        }

        [Fact]
        public async Task GetSizeByValueAsync_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var sizeValue = SizeValue.Large;
            _mockProductSizeRepo.Setup(r => r.GetSizeByValueAsync(sizeValue)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetSizeByValueAsync(sizeValue));
            _mockProductSizeRepo.Verify(r => r.GetSizeByValueAsync(sizeValue), Times.Once);
        }
    }
}
