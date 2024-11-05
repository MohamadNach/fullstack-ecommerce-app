using System;
using System.Threading.Tasks;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.UserService;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Service
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly UserManagement _userService;

        public UserServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _userService = new UserManagement(_mockUserRepo.Object, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidCreateDto_ReturnsUserReadDto()
        {
            // Arrange
            var createDto = new UserCreateDto { PasswordHash = "password" };
            var user = new User { Id = Guid.NewGuid() };
            var hashedPassword = "hashedPassword";
            var salt = new byte[16];

            _mockPasswordHasher
                .Setup(ph => ph.HashPassword(createDto.PasswordHash, out hashedPassword, out salt))
                .Callback((string pwd, out string hash, out byte[] s) =>
                {
                    hash = hashedPassword;
                    s = salt;
                });

            _mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>())).Returns((Task<User>)Task.CompletedTask);

            var expectedDto = new UserReadDto();
            expectedDto.FromEntity(user);

            // Act
            var result = await _userService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserReadDto>(result);
            _mockPasswordHasher.Verify(ph => ph.HashPassword(createDto.PasswordHash, out hashedPassword, out salt), Times.Once);
            _mockUserRepo.Verify(repo => repo.CreateAsync(It.Is<User>(u => u.PasswordHash == hashedPassword && u.Salt == salt)), Times.Once);
        }

        [Fact]
        public async Task UpdatePasswordAsync_ValidUserId_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var newPassword = "newPassword";
            var hashedPassword = "hashedNewPassword";
            var salt = new byte[16];

            _mockPasswordHasher
                .Setup(ph => ph.HashPassword(newPassword, out hashedPassword, out salt))
                .Callback((string pwd, out string hash, out byte[] s) =>
                {
                    hash = hashedPassword;
                    s = salt;
                });

            _mockUserRepo.Setup(repo => repo.UpdatePasswordAsync(userId, newPassword)).ReturnsAsync(true);

            // Act
            var result = await _userService.UpdatePasswordAsync(userId, newPassword);

            // Assert
            Assert.True(result);
            _mockUserRepo.Verify(repo => repo.UpdatePasswordAsync(userId, newPassword), Times.Once);
        }

        [Fact]
        public async Task GetUserByEmailAsync_UserExists_ReturnsUserReadDto()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Id = Guid.NewGuid(), Email = email };
            var expectedDto = new UserReadDto();
            expectedDto.FromEntity(user);

            _mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserReadDto>(result);
            Assert.Equal(expectedDto.Id, result.Id);
            _mockUserRepo.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task GetUserByEmailAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetUserByEmailAsync(email);

            // Assert
            Assert.Null(result);
            _mockUserRepo.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
        }
    }
}
