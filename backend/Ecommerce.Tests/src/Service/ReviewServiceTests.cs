using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ReviewService;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Tests.Service
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> _mockReviewRepo;
        private readonly ReviewManagement _service;

        public ReviewServiceTests()
        {
            _mockReviewRepo = new Mock<IReviewRepository>();
            _service = new ReviewManagement(_mockReviewRepo.Object);
        }

        [Fact]
        public async Task GetReviewsByProductIdAsync_ShouldReturnReviewReadDtos_WhenReviewsExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var reviews = new List<Review>
            {
                new Review { Id = Guid.NewGuid(), ProductId = productId, ReviewText = "Great product!", Rating = 5 },
                new Review { Id = Guid.NewGuid(), ProductId = productId, ReviewText = "Not bad.", Rating = 3 }
            };

            _mockReviewRepo.Setup(r => r.GetReviewsByProductIdAsync(productId)).ReturnsAsync(reviews);

            // Act
            var result = await _service.GetReviewsByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ReviewText == "Great product!");
            Assert.Contains(result, dto => dto.ReviewText == "Not bad.");
            _mockReviewRepo.Verify(r => r.GetReviewsByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetReviewsByProductIdAsync_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockReviewRepo.Setup(r => r.GetReviewsByProductIdAsync(productId)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetReviewsByProductIdAsync(productId));
            _mockReviewRepo.Verify(r => r.GetReviewsByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetReviewsByUserIdAsync_ShouldReturnReviewReadDtos_WhenReviewsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var reviews = new List<Review>
            {
                new Review { Id = Guid.NewGuid(), UserId = userId, ReviewText = "Amazing!", Rating = 5 },
                new Review { Id = Guid.NewGuid(), UserId = userId, ReviewText = "Okay.", Rating = 3 }
            };

            _mockReviewRepo.Setup(r => r.GetReviewsByUserIdAsync(userId)).ReturnsAsync(reviews);

            // Act
            var result = await _service.GetReviewsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.ReviewText == "Amazing!");
            Assert.Contains(result, dto => dto.ReviewText == "Okay.");
            _mockReviewRepo.Verify(r => r.GetReviewsByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetReviewsByUserIdAsync_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockReviewRepo.Setup(r => r.GetReviewsByUserIdAsync(userId)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetReviewsByUserIdAsync(userId));
            _mockReviewRepo.Verify(r => r.GetReviewsByUserIdAsync(userId), Times.Once);
        }
    }
}
