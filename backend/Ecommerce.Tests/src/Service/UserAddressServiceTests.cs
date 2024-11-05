using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.UserAddressService;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Service
{
    public class UserAddressManagementTests
    {
        private readonly Mock<IUserAddressRepository> _mockUserAddressRepo;
        private readonly UserAddressManagement _service;

        public UserAddressManagementTests()
        {
            _mockUserAddressRepo = new Mock<IUserAddressRepository>();
            _service = new UserAddressManagement(_mockUserAddressRepo.Object);
        }

        [Fact]
        public async Task GetUserAddressesByUserIdAsync_ShouldReturnUserAddressReadDtos()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userAddresses = new List<UserAddress>
            {
                new UserAddress { /* Initialize properties here */ },
                new UserAddress { /* Initialize properties here */ }
            };

            _mockUserAddressRepo.Setup(r => r.GetUserAddressesByUserIdAsync(userId))
                .ReturnsAsync(userAddresses);

            // Act
            var result = await _service.GetUserAddressesByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userAddresses.Count, result.Count());
            Assert.All(result, item => Assert.IsType<UserAddressReadDto>(item));
            _mockUserAddressRepo.Verify(r => r.GetUserAddressesByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserAddressesByUserIdAsync_ShouldReturnEmptyList_WhenNoAddressesFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserAddressRepo.Setup(r => r.GetUserAddressesByUserIdAsync(userId))
                .ReturnsAsync((List<UserAddress>)null);

            // Act
            var result = await _service.GetUserAddressesByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockUserAddressRepo.Verify(r => r.GetUserAddressesByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetDefaultUserAddressAsync_ShouldReturnUserAddressReadDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userAddress = new UserAddress { /* Initialize properties here */ };

            _mockUserAddressRepo.Setup(r => r.GetDefaultUserAddressAsync(userId))
                .ReturnsAsync(userAddress);

            // Act
            var result = await _service.GetDefaultUserAddressAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserAddressReadDto>(result);
            _mockUserAddressRepo.Verify(r => r.GetDefaultUserAddressAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetDefaultUserAddressAsync_ShouldReturnEmptyDto_WhenNoDefaultAddressFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserAddressRepo.Setup(r => r.GetDefaultUserAddressAsync(userId))
                .ReturnsAsync((UserAddress)null);

            // Act
            var result = await _service.GetDefaultUserAddressAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserAddressReadDto>(result);
            // Optionally check if properties of result are default values
            _mockUserAddressRepo.Verify(r => r.GetDefaultUserAddressAsync(userId), Times.Once);
        }
    }
}
