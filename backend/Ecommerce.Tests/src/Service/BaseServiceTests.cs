using System.Linq.Expressions;
using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;
using Ecommerce.Service.src.Shared;
using Moq;

namespace Ecommerce.Tests.Service
{
    public class BaseServiceTests
    {
        private readonly Mock<IBaseRepository<BaseEntity>> _mockRepo;
        private readonly BaseService<BaseEntity, MockReadDto, MockCreateDto, MockUpdateDto> _service;

        public BaseServiceTests()
        {
            _mockRepo = new Mock<IBaseRepository<BaseEntity>>();
            _service = new BaseService<BaseEntity, MockReadDto, MockCreateDto, MockUpdateDto>(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidCreateDto_ReturnsReadDto()
        {
            // Arrange
            var createDto = new MockCreateDto();
            var entity = new BaseEntity { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<BaseEntity>())).ReturnsAsync(entity);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MockReadDto>(result);
            Assert.Equal(entity.Id, result.Id);  // Verify ID mapping
            _mockRepo.Verify(r => r.CreateAsync(It.IsAny<BaseEntity>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_EntityExists_ReturnsReadDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new BaseEntity { Id = id };
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>())).ReturnsAsync(entity);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MockReadDto>(result);
            Assert.Equal(id, result.Id); // Ensure the ID matches
            _mockRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_EntityDoesNotExist_ThrowsEntityNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>())).ReturnsAsync((BaseEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetByIdAsync(id));

            _mockRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_EntityExists_ReturnsReadDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new BaseEntity { Id = id };
            var updateDto = new MockUpdateDto();
            var updatedEntity = new BaseEntity { Id = id }; // Entity after update

            // Mock the repository methods
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>())).ReturnsAsync(entity);
            _mockRepo.Setup(r => r.UpdateByIdAsync(It.IsAny<BaseEntity>())).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MockReadDto>(result);
            Assert.Equal(id, result.Id); // Verify ID is correct after update
            _mockRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateByIdAsync(It.Is<BaseEntity>(e => e.Id == id)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_EntityDoesNotExist_ThrowsEntityNotFoundException()
        {
            var id = Guid.NewGuid();
            var updateDto = new MockUpdateDto();
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>())).ReturnsAsync((BaseEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.UpdateAsync(id, updateDto));

            _mockRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteByIdAsync()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepo.Verify(r => r.DeleteByIdAsync(It.Is<Guid>(g => g == id)), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfReadDto()
        {
            // Arrange
            var paginationOptions = new PaginationOptions();
            var entities = new PaginatedResult<BaseEntity>
            {
                Items = new List<BaseEntity> { new BaseEntity(), new BaseEntity() },
                CurrentPage = 1,
                TotalPages = 1
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<PaginationOptions>())).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAllAsync(paginationOptions);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PaginatedResult<MockReadDto>>(result);
            Assert.Equal(2, result.Items.Count());
            Assert.All(result.Items, item => Assert.IsType<MockReadDto>(item));
            _mockRepo.Verify(r => r.GetAllAsync(It.IsAny<PaginationOptions>()), Times.Once);
        }
    }

    // Custom exception for entity not found scenarios
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }

    // Mock DTO classes for testing purposes
    public class MockReadDto : IReadDto<BaseEntity>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void FromEntity(BaseEntity entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
        }
    }

    public class MockCreateDto : ICreateDto<BaseEntity>
    {
        public BaseEntity CreateEntity() => new BaseEntity { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
    }

    public class MockUpdateDto : IUpdateDto<BaseEntity>
    {
        public Guid Id { get; set; }

        public BaseEntity UpdateEntity(BaseEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            return entity;
        }
    }
}
