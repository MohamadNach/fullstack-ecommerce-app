using System.Linq.Expressions;
using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;
using Ecommerce.Service.src.CategoryService;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Service
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryManagement _categoryService;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryManagement(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetCategoryByNameAsync_ShouldReturnCategoryReadDto_WhenCategoryExists()
        {
            // Arrange
            var categoryName = "Electronics";
            var category = new Category { Id = Guid.NewGuid(), CategoryName = categoryName };
            _mockCategoryRepository.Setup(r => r.GetCategoryByNameAsync(It.IsAny<string>()))
                                   .ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetCategoryByNameAsync(categoryName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryReadDto>(result);
            Assert.Equal(categoryName, result.CategoryName);
            _mockCategoryRepository.Verify(r => r.GetCategoryByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetCategoryByNameAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryName = "NonExistentCategory";
            _mockCategoryRepository.Setup(r => r.GetCategoryByNameAsync(It.IsAny<string>()))
                                   .ReturnsAsync((Category?)null);

            // Act
            var result = await _categoryService.GetCategoryByNameAsync(categoryName);

            // Assert
            Assert.Null(result);
            _mockCategoryRepository.Verify(r => r.GetCategoryByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCategoryReadDto()
        {
            // Arrange
            var createDto = new CategoryCreateDto { CategoryName = "New Category" };
            var category = new Category { Id = Guid.NewGuid(), CategoryName = createDto.CategoryName };
            _mockCategoryRepository.Setup(r => r.CreateAsync(It.IsAny<Category>())).ReturnsAsync(category);

            // Act
            var result = await _categoryService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryReadDto>(result);
            Assert.Equal(createDto.CategoryName, result.CategoryName);
            _mockCategoryRepository.Verify(r => r.CreateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnCategoryReadDto_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateDto = new CategoryUpdateDto { CategoryName = "Updated Category" };
            var existingCategory = new Category { Id = categoryId, CategoryName = "Old Category" };
            _mockCategoryRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(existingCategory);
            _mockCategoryRepository.Setup(r => r.UpdateByIdAsync(It.IsAny<Category>())).ReturnsAsync(true);

            // Act
            var result = await _categoryService.UpdateAsync(categoryId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryReadDto>(result);
            Assert.Equal(updateDto.CategoryName, result.CategoryName);
            _mockCategoryRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
            _mockCategoryRepository.Verify(r => r.UpdateByIdAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDeleteByIdAsync()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _mockCategoryRepository.Setup(r => r.DeleteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            await _categoryService.DeleteAsync(categoryId);

            // Assert
            _mockCategoryRepository.Verify(r => r.DeleteByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfCategoryReadDto()
        {
            // Arrange
            var paginationOptions = new PaginationOptions();
            var categories = new PaginatedResult<Category>
            {
                Items = new List<Category> { new Category { Id = Guid.NewGuid(), CategoryName = "Category1" }, new Category { Id = Guid.NewGuid(), CategoryName = "Category2" } },
                CurrentPage = 1,
                TotalPages = 1
            };

            _mockCategoryRepository.Setup(r => r.GetAllAsync(It.IsAny<PaginationOptions>())).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllAsync(paginationOptions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result?.Items?.Count());
            _mockCategoryRepository.Verify(r => r.GetAllAsync(It.IsAny<PaginationOptions>()), Times.Once);
        }
    }
}
