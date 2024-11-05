using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ProductColorService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Domain.src.ProductAggregate;
using System.Linq.Expressions;

namespace Ecommerce.Tests.Service
{
    public class ProductColorServiceTests
    {
        private readonly Mock<IProductColorRepository> _mockProductColorRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductColorManagement _service;

        public ProductColorServiceTests()
        {
            _mockProductColorRepo = new Mock<IProductColorRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _service = new ProductColorManagement(_mockProductColorRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task GetColorsByProductIdAsync_ShouldReturnProductColorReadDtos_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var productColors = new List<ProductColor>
            {
                new ProductColor { Id = Guid.NewGuid(), ColorName = ColorName.Red },
                new ProductColor { Id = Guid.NewGuid(), ColorName = ColorName.Blue }
            };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductColorRepo.Setup(r => r.GetColorsByProductIdAsync(productId)).ReturnsAsync(productColors);

            // Act
            var result = await _service.GetColorsByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ColorName == ColorName.Red.ToString());
            Assert.Contains(result, dto => dto.ColorName == ColorName.Blue.ToString());
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductColorRepo.Verify(r => r.GetColorsByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetColorsByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetColorsByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetColorByNameAsync_ShouldReturnProductColorReadDto_WhenColorExists()
        {
            // Arrange
            var colorName = ColorName.Red.ToString();
            var productColor = new ProductColor { Id = Guid.NewGuid(), ColorName = ColorName.Red };

            _mockProductColorRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<ProductColor, bool>>>())).ReturnsAsync(productColor);

            // Act
            var result = await _service.GetColorByNameAsync(colorName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(colorName, result.ColorName);
            _mockProductColorRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<ProductColor, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetColorByNameAsync_ShouldThrowArgumentException_WhenColorDoesNotExist()
        {
            // Arrange
            var colorName = ColorName.Red.ToString();
            _mockProductColorRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<ProductColor, bool>>>())).ReturnsAsync((ProductColor?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetColorByNameAsync(colorName));
            _mockProductColorRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<ProductColor, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetColorByNameAsync_ShouldThrowArgumentException_WhenColorNameIsInvalid()
        {
            // Arrange
            var invalidColorName = "InvalidColorName";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetColorByNameAsync(invalidColorName));
            _mockProductColorRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<ProductColor, bool>>>()), Times.Never);
        }
    }
}
