using System.Linq.Expressions;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.OrderService;
using Moq;

public class OrderServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IAddressRepository> _mockAddressRepository;
    private readonly OrderManagement _orderService;

    public OrderServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockAddressRepository = new Mock<IAddressRepository>();
        _orderService = new OrderManagement(_mockOrderRepository.Object, _mockUserRepository.Object, _mockAddressRepository.Object);
    }

    [Fact]
    public async Task GetOrdersByUserIdAsync_ShouldReturnOrderDtos_WhenUserAndOrdersExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid() },
            new Order { Id = Guid.NewGuid() }
        };

        _mockUserRepository
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        _mockOrderRepository
            .Setup(repo => repo.GetOrdersByUserIdAsync(userId))
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetOrdersByUserIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, dto =>
        {
            Assert.NotNull(dto?.Id);
        });
    }

    [Fact]
    public async Task GetOrdersByStatusAsync_ShouldReturnOrderDtos_WhenOrdersExist()
    {
        // Arrange
        var status = OrderStatus.Pending;
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), OrderStatus = status },
            new Order { Id = Guid.NewGuid(), OrderStatus = status }
        };

        _mockOrderRepository
            .Setup(repo => repo.GetOrdersByStatusAsync(status))
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetOrdersByStatusAsync(status);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, dto =>
        {
            Assert.NotNull(dto?.Id);
            Assert.Equal(status, dto.OrderStatus);
        });
    }

    [Fact]
    public async Task GetOrdersByDateRangeAsync_ShouldReturnOrderDtos_WhenOrdersExist()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), OrderDate = new DateTime(2024, 1, 5) },
            new Order { Id = Guid.NewGuid(), OrderDate = new DateTime(2024, 1, 15) }
        };

        _mockOrderRepository
            .Setup(repo => repo.GetOrdersByDateRangeAsync(startDate, endDate))
            .ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, dto =>
        {
            Assert.NotNull(dto?.Id);
            Assert.InRange(dto.OrderDate, startDate, endDate);
        });
    }

    [Fact]
    public async Task GetTotalPriceByOrderIdAsync_ShouldReturnTotalPrice_WhenOrderExists()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order { Id = orderId };
        var totalPrice = 100.00m;

        _mockOrderRepository
            .Setup(repo => repo.GetAsync(It.Is<Expression<Func<Order, bool>>>(expr => expr.Compile()(order))))
            .ReturnsAsync(order);

        _mockOrderRepository
            .Setup(repo => repo.GetTotalPriceByOrderIdAsync(orderId))
            .ReturnsAsync(totalPrice);

        // Act
        var result = await _orderService.GetTotalPriceByOrderIdAsync(orderId);

        // Assert
        Assert.Equal(totalPrice, result);
    }

}
