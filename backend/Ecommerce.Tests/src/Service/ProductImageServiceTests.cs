using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ProductImageService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Domain.src.ProductAggregate;

namespace Ecommerce.Tests.Service
{
    public class ProductImageServiceTests
    {
        private readonly Mock<IProductImageRepository> _mockProductImageRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductImageManagement _service;

        public ProductImageServiceTests()
        {
            _mockProductImageRepo = new Mock<IProductImageRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _service = new ProductImageManagement(_mockProductImageRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task GetImagesByProductIdAsync_ShouldReturnProductImageReadDtos_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var productImages = new List<ProductImage>
            {
                new ProductImage { Id = Guid.NewGuid(), ImageURL = "image1.jpg" },
                new ProductImage { Id = Guid.NewGuid(), ImageURL = "image2.jpg" }
            };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductImageRepo.Setup(r => r.GetImagesByProductIdAsync(productId)).ReturnsAsync(productImages);

            // Act
            var result = await _service.GetImagesByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ImageURL == "image1.jpg");
            Assert.Contains(result, dto => dto.ImageURL == "image2.jpg");
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.GetImagesByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetImagesByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetImagesByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetMainImageForProductAsync_ShouldReturnProductImageReadDto_WhenProductAndImageExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var mainImage = new ProductImage { Id = Guid.NewGuid(), ImageURL = "mainImage.jpg" };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductImageRepo.Setup(r => r.GetMainImageForProductAsync(productId)).ReturnsAsync(mainImage);

            // Act
            var result = await _service.GetMainImageForProductAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("mainImage.jpg", result.ImageURL);
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.GetMainImageForProductAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetMainImageForProductAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetMainImageForProductAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.GetMainImageForProductAsync(productId), Times.Never);
        }

        [Fact]
        public async Task GetImageCountByProductIdAsync_ShouldReturnImageCount_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var imageCount = 5;

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductImageRepo.Setup(r => r.GetImageCountByProductIdAsync(productId)).ReturnsAsync(imageCount);

            // Act
            var result = await _service.GetImageCountByProductIdAsync(productId);

            // Assert
            Assert.Equal(imageCount, result);
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.GetImageCountByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetImageCountByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetImageCountByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.GetImageCountByProductIdAsync(productId), Times.Never);
        }

        [Fact]
        public async Task DeleteImagesByProductIdAsync_ShouldReturnTrue_WhenProductExistsAndImagesDeleted()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockProductImageRepo.Setup(r => r.DeleteImagesByProductIdAsync(productId)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteImagesByProductIdAsync(productId);

            // Assert
            Assert.True(result);
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.DeleteImagesByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task DeleteImagesByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteImagesByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockProductImageRepo.Verify(r => r.DeleteImagesByProductIdAsync(productId), Times.Never);
        }
    }
}
