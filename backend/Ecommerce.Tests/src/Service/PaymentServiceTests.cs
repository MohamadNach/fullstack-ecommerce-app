using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.PaymentService;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Tests.Service
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentRepository> _mockPaymentRepo;
        private readonly PaymentManagement _service;

        public PaymentServiceTests()
        {
            _mockPaymentRepo = new Mock<IPaymentRepository>();
            _service = new PaymentManagement(_mockPaymentRepo.Object);
        }

        [Fact]
        public async Task GetAllUserPaymentAsync_ShouldReturnPaymentReadDtos_WhenPaymentsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var payments = new List<Payment>
            {
                new Payment { Id = Guid.NewGuid(), UserId = userId, Amount = 100 },
                new Payment { Id = Guid.NewGuid(), UserId = userId, Amount = 200 }
            };

            _mockPaymentRepo.Setup(r => r.GetAllUserPaymentAsync(userId)).ReturnsAsync(payments);

            // Act
            var result = await _service.GetAllUserPaymentAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.IsType<PaymentReadDto>(item));
            _mockPaymentRepo.Verify(r => r.GetAllUserPaymentAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetAllUserPaymentAsync_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockPaymentRepo.Setup(r => r.GetAllUserPaymentAsync(userId)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAllUserPaymentAsync(userId));
            Assert.Equal("Database error", exception.Message);
            _mockPaymentRepo.Verify(r => r.GetAllUserPaymentAsync(userId), Times.Once);
        }
    }

    // Mock DTO classes for testing purposes
    public class PaymentReadDto : IReadDto<Payment>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void FromEntity(Payment entity)
        {
            Id = entity.Id;
            UserId = entity.UserId;
            Amount = entity.Amount;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
        }
    }

    public class PaymentCreateDto : ICreateDto<Payment>
    {
        public Payment CreateEntity()
        {
            return new Payment();
        }
    }

    public class PaymentUpdateDto : IUpdateDto<Payment>
    {
        public Guid Id { get; set; }

        public Payment UpdateEntity(Payment entity)
        {
            entity.Id = Id;
            return entity;
        }
    }
}
