using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Tests.src.Domain
{
    public class OrderDomainTest
    {
        [Fact]
        public void AddOrderItem_ShouldIncreaseTotalPrice()
        {
            // Arrange
            var order = new Order();
            var orderItem = new OrderItem
            {
                Price = 50m,
                Quantity = 2
            };

            // Act
            order.AddOrderItem(orderItem);

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(100m, order.TotalPrice);
        }

        [Fact]
        public void RemoveOrderItem_ShouldDecreaseTotalPrice()
        {
            // Arrange
            var order = new Order();
            var orderItem = new OrderItem
            {
                Price = 50m,
                Quantity = 2
            };
            order.AddOrderItem(orderItem);

            // Act
            order.RemoveOrderItem(orderItem);

            // Assert
            Assert.Empty(order.OrderItems);
            Assert.Equal(0m, order.TotalPrice);
        }

        [Fact]
        public void RemoveOrderItem_ShouldNotChangeTotalPrice_WhenItemNotInOrder()
        {
            // Arrange
            var order = new Order();
            var orderItem1 = new OrderItem
            {
                Price = 50m,
                Quantity = 2
            };
            var orderItem2 = new OrderItem
            {
                Price = 30m,
                Quantity = 1
            };
            order.AddOrderItem(orderItem1);

            // Act
            order.RemoveOrderItem(orderItem2); // Removing an item that doesn't exist in the order

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(100m, order.TotalPrice);
        }

        [Fact]
        public void RemoveOrderItem_ShouldChangeTotalPrice_WhenItemInOrder()
        {
            // Arrange
            var order = new Order();
            var orderItem1 = new OrderItem
            {
                Price = 50m,
                Quantity = 2
            };
            var orderItem2 = new OrderItem
            {
                Price = 30m,
                Quantity = 1
            };
            order.AddOrderItem(orderItem1);
            order.AddOrderItem(orderItem2);

            // Act
            order.RemoveOrderItem(orderItem2); // Removing an item exist in the order

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(100m, order.TotalPrice);
        }

        [Fact]
        public void UpdateOrderStatus_ShouldChangeOrderStatus()
        {
            // Arrange
            var order = new Order();
            var newStatus = OrderStatus.Completed;

            // Act
            order.UpdateOrderStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, order.OrderStatus);
        }

        [Fact]
        public void AddOrderItem_ShouldAddMultipleItemsAndCalculateTotalCorrectly()
        {
            // Arrange
            var order = new Order();
            var orderItem1 = new OrderItem
            {
                Price = 50m,
                Quantity = 2
            };
            var orderItem2 = new OrderItem
            {
                Price = 30m,
                Quantity = 1
            };

            // Act
            order.AddOrderItem(orderItem1);
            order.AddOrderItem(orderItem2);

            // Assert
            Assert.Equal(2, order.OrderItems.Count);
            Assert.Equal(130m, order.TotalPrice);
        }
    }
}