using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.ShipmentService;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Service
{
    public class ShipmentManagementTests
    {
        private readonly Mock<IShipmentRepository> _mockShipmentRepo;
        private readonly ShipmentManagement _service;

        public ShipmentManagementTests()
        {
            _mockShipmentRepo = new Mock<IShipmentRepository>();
            _service = new ShipmentManagement(_mockShipmentRepo.Object);
        }

        [Fact]
        public async Task GetShipmentsByOrderIdAsync_ShouldReturnShipmentReadDtos()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var shipments = new List<Shipment>
            {
                new Shipment { /* Initialize properties here */ },
                new Shipment { /* Initialize properties here */ }
            };

            _mockShipmentRepo.Setup(r => r.GetShipmentsByOrderIdAsync(orderId))
                .ReturnsAsync(shipments);

            // Act
            var result = await _service.GetShipmentsByOrderIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shipments.Count, result.Count());
            Assert.All(result, item => Assert.IsType<ShipmentReadDto>(item));
            _mockShipmentRepo.Verify(r => r.GetShipmentsByOrderIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetShipmentsByOrderIdAsync_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockShipmentRepo.Setup(r => r.GetShipmentsByOrderIdAsync(orderId))
                .ThrowsAsync(new Exception("Repository exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetShipmentsByOrderIdAsync(orderId));
            _mockShipmentRepo.Verify(r => r.GetShipmentsByOrderIdAsync(orderId), Times.Once);
        }
    }
}
