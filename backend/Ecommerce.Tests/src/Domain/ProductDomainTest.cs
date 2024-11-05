using Ecommerce.Domain.src.ProductAggregate;

namespace Ecommerce.Tests.src.Domain
{
    public class ProductDomainTest
    {
        [Fact]
        public void IsInStock_ShouldBeMoreThanZero()
        {
            // Arrange
            var product = new Product
            {
                Quantity = 10
            };

            // Act
            var result = product.IsInStock();

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void IsInStock_ShouldBeEqualToZero()
        {
            // Arrange
            var product = new Product
            {
                Quantity = 0
            };

            // Act
            var result = product.IsInStock();

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void UpdateStock_ShouldUpdateStockValue()
        {
            var product = new Product
            {
                Quantity = 1
            };

            // Act
            product.UpdateStock(5);

            // Assert
            Assert.Equal(6, product.Quantity);
        }

    }
}