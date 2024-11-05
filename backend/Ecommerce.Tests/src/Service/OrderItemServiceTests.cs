using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.OrderService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.OrderItemService;
using System.Linq.Expressions;
using Ecommerce.Domain.src.ProductAggregate;

namespace Ecommerce.Tests.Service
{
    public class OrderItemServiceTests
    {
        private readonly Mock<IOrderItemRepository> _mockOrderItemRepo;
        private readonly Mock<IOrderRepository> _mockOrderRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly OrderItemManagement _service;

        public OrderItemServiceTests()
        {
            _mockOrderItemRepo = new Mock<IOrderItemRepository>();
            _mockOrderRepo = new Mock<IOrderRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _service = new OrderItemManagement(_mockOrderItemRepo.Object, _mockOrderRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task GetOrderItemsByOrderIdAsync_ShouldReturnOrderItemReadDtos_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), OrderId = orderId },
                new OrderItem { Id = Guid.NewGuid(), OrderId = orderId }
            };

            _mockOrderRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(order);
            _mockOrderItemRepo.Setup(r => r.GetOrderItemsByOrderIdAsync(orderId)).ReturnsAsync(orderItems);

            // Act
            var result = await _service.GetOrderItemsByOrderIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockOrderRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _mockOrderItemRepo.Verify(r => r.GetOrderItemsByOrderIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetOrderItemsByOrderIdAsync_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockOrderRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync((Order?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetOrderItemsByOrderIdAsync(orderId));
            _mockOrderRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetOrderItemsByProductIdAsync_ShouldReturnOrderItemReadDtos_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), ProductId = productId },
                new OrderItem { Id = Guid.NewGuid(), ProductId = productId }
            };

            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
            _mockOrderItemRepo.Setup(r => r.GetOrderItemsByProductIdAsync(productId)).ReturnsAsync(orderItems);

            // Act
            var result = await _service.GetOrderItemsByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _mockOrderItemRepo.Verify(r => r.GetOrderItemsByProductIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetOrderItemsByProductIdAsync_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetOrderItemsByProductIdAsync(productId));
            _mockProductRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetTotalQuantityByOrderIdAsync_ShouldReturnTotalQuantity_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), OrderId = orderId, Quantity = 5 },
                new OrderItem { Id = Guid.NewGuid(), OrderId = orderId, Quantity = 10 }
            };

            _mockOrderRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(order);
            _mockOrderItemRepo.Setup(r => r.GetOrderItemsByOrderIdAsync(orderId)).ReturnsAsync(orderItems);

            // Act
            var result = await _service.GetTotalQuantityByOrderIdAsync(orderId);

            // Assert
            Assert.Equal(15, result);
            _mockOrderRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _mockOrderItemRepo.Verify(r => r.GetOrderItemsByOrderIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetTotalQuantityByOrderIdAsync_ShouldThrowArgumentException_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockOrderRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync((Order?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetTotalQuantityByOrderIdAsync(orderId));
            _mockOrderRepo.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
        }
    }
}
