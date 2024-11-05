using System.Globalization;
using System.Security.Policy;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.PaymentAggregate;
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Infrastructure.src.Repository.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Infrastructure.src.Database
{
    public static class SeedingData
    {

        private static readonly PasswordHasher passwordHasher = new PasswordHasher();

        public static User GetNewUser(int count, string firstName, string lastName, string phoneNumber, string originalPassword, UserRole role)
        {
            passwordHasher.HashPassword(originalPassword, out string hash, out byte[] salt);

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = $"email{count}@example.com",
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = hash,
                Salt = salt,
                PhoneNumber = phoneNumber,
                Role = role
            };


            return user;
        }
        public static User user1 = GetNewUser(1, "Moh", "nach", "0413333333", "123456789m", UserRole.Admin);
        public static User user2 = GetNewUser(2, "Jane", "Smith", "0413333333", "123456789m", UserRole.User);
        public static User user3 = GetNewUser(3, "Bob", "Johnson", "0413333333", "123456789m", UserRole.User);
        public static User user4 = GetNewUser(4, "Alice", "Williams", "0413333333", "123456789m", UserRole.User);
        public static User user5 = GetNewUser(5, "Charlie", "Brown", "0413333333", "123456789m", UserRole.User);
        public static User user6 = GetNewUser(6, "Oula", "Mok", "0413333333", "123456789m", UserRole.User);
        public static List<User> GetUsers()
        {

            return new List<User>
            {
                user1, user2, user3, user4, user5, user6
            };
        }

        public static Address address1 = new Address { Id = Guid.NewGuid(), UnitNumber = "1452", StreetNumber = "123 Main St", City = "New York", PostalCode = "10001", Country = "USA", AddressLine1 = "Terveystie", AddressLine2 = "" };
        public static Address address2 = new Address { Id = Guid.NewGuid(), UnitNumber = "1452", StreetNumber = "456 Elm St", City = "Los Angeles", PostalCode = "90001", Country = "USA", AddressLine1 = "Terveystie", AddressLine2 = "" };
        public static Address address3 = new Address { Id = Guid.NewGuid(), UnitNumber = "1452", StreetNumber = "789 Oak St", City = "Chicago", PostalCode = "60601", Country = "USA", AddressLine1 = "Terveystie", AddressLine2 = "" };
        public static Address address4 = new Address { Id = Guid.NewGuid(), UnitNumber = "1452", StreetNumber = "101 Pine St", City = "Houston", PostalCode = "77001", Country = "USA", AddressLine1 = "Terveystie", AddressLine2 = "" };
        public static Address address5 = new Address { Id = Guid.NewGuid(), UnitNumber = "1452", StreetNumber = "202 Cedar St", City = "Miami", PostalCode = "33101", Country = "USA", AddressLine1 = "Terveystie", AddressLine2 = "" };

        public static List<Address> GetAddresses()
        {
            return new List<Address>
            {
                address1, address2, address3, address4, address5
            };
        }

        private static Category category1 = new Category { Id = Guid.NewGuid(), CategoryName = "Men" };
        private static Category category2 = new Category { Id = Guid.NewGuid(), CategoryName = "Women" };
        private static Category category3 = new Category { Id = Guid.NewGuid(), CategoryName = "Kids" };

        // Categories under Men
        private static Category category4 = new Category { Id = Guid.NewGuid(), CategoryName = "Topwear", ParentCategoryId = category1.Id };
        private static Category category5 = new Category { Id = Guid.NewGuid(), CategoryName = "Bottomwear", ParentCategoryId = category1.Id };
        private static Category category6 = new Category { Id = Guid.NewGuid(), CategoryName = "WinterWear", ParentCategoryId = category1.Id };
        private static Category category7 = new Category { Id = Guid.NewGuid(), CategoryName = "Footwear", ParentCategoryId = category1.Id };

        // Categories under Women
        private static Category category8 = new Category { Id = Guid.NewGuid(), CategoryName = "Dresses", ParentCategoryId = category2.Id };
        private static Category category9 = new Category { Id = Guid.NewGuid(), CategoryName = "EthnicWear", ParentCategoryId = category2.Id };
        private static Category category10 = new Category { Id = Guid.NewGuid(), CategoryName = "MaternityWear", ParentCategoryId = category2.Id };
        private static Category category11 = new Category { Id = Guid.NewGuid(), CategoryName = "Footwear", ParentCategoryId = category2.Id };

        // Categories under Kids
        private static Category category12 = new Category { Id = Guid.NewGuid(), CategoryName = "Toys", ParentCategoryId = category3.Id };
        private static Category category13 = new Category { Id = Guid.NewGuid(), CategoryName = "School Supplies", ParentCategoryId = category3.Id };
        private static Category category14 = new Category { Id = Guid.NewGuid(), CategoryName = "Footwear", ParentCategoryId = category3.Id };

        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                category1, category2, category3,
                category4, category5, category6, category7,
                category8, category9, category10, category11,
                category12, category13, category14
            };
        }

        public static Product product1 = new Product { Id = Guid.NewGuid(), ProductTitle = "Laptop XYZ", Price = 999.99M, CategoryId = category1.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product2 = new Product { Id = Guid.NewGuid(), ProductTitle = "Smartphone ABC", Price = 699.99M, CategoryId = category2.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product3 = new Product { Id = Guid.NewGuid(), ProductTitle = "TV DEF", Price = 499.99M, CategoryId = category3.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product4 = new Product { Id = Guid.NewGuid(), ProductTitle = "Tablet GHI", Price = 299.99M, CategoryId = category4.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product5 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product6 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product7 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product8 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product9 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product10 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product11 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product12 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product13 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product14 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };
        public static Product product15 = new Product { Id = Guid.NewGuid(), ProductTitle = "Headphones JKL", Price = 149.99M, CategoryId = category5.Id, Description = "Description", Quantity = 3, BrandName = "Adibas" };

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                product1, product2, product3, product4, product5,
                product6, product7, product8, product9, product10,
                product11, product12, product13, product14, product15
            };
        }

        public static Cart cart1 = new Cart { Id = Guid.NewGuid(), UserId = user1.Id };
        public static Cart cart2 = new Cart { Id = Guid.NewGuid(), UserId = user2.Id };
        public static Cart cart3 = new Cart { Id = Guid.NewGuid(), UserId = user3.Id };
        public static Cart cart4 = new Cart { Id = Guid.NewGuid(), UserId = user4.Id };
        public static Cart cart5 = new Cart { Id = Guid.NewGuid(), UserId = user5.Id };
        public static List<Cart> GetCarts()
        {
            return new List<Cart>
            {
                cart1, cart2,cart3, cart4, cart5
            };
        }

        public static CartItem cartItem1 = new CartItem { Id = Guid.NewGuid(), CartId = cart1.Id, ProductId = product1.Id, Quantity = 2, UnitPrice = 19.9M, TotalPrice = 39.8m };
        public static CartItem cartItem2 = new CartItem { Id = Guid.NewGuid(), CartId = cart2.Id, ProductId = product2.Id, Quantity = 1, UnitPrice = 19.9M, TotalPrice = 19.9m };
        public static CartItem cartItem3 = new CartItem { Id = Guid.NewGuid(), CartId = cart3.Id, ProductId = product3.Id, Quantity = 3, UnitPrice = 19.9M, TotalPrice = 59.7m };
        public static CartItem cartItem4 = new CartItem { Id = Guid.NewGuid(), CartId = cart4.Id, ProductId = product4.Id, Quantity = 1, UnitPrice = 19.9M, TotalPrice = 19.9m };
        public static CartItem cartItem5 = new CartItem { Id = Guid.NewGuid(), CartId = cart5.Id, ProductId = product5.Id, Quantity = 4, UnitPrice = 19.9M, TotalPrice = 79.6m };

        public static List<CartItem> GetCartItems()
        {
            return new List<CartItem>
            {
                cartItem1, cartItem2, cartItem3, cartItem4, cartItem5
            };
        }

        public static Order order1 = new Order { Id = Guid.NewGuid(), UserId = user1.Id, TotalPrice = 340M, OrderDate = DateTime.UtcNow, OrderStatus = OrderStatus.Pending, ShippingAddressId = address1.Id };
        public static Order order2 = new Order { Id = Guid.NewGuid(), UserId = user2.Id, TotalPrice = 50M, OrderDate = DateTime.UtcNow, OrderStatus = OrderStatus.Processing, ShippingAddressId = address2.Id };
        public static Order order3 = new Order { Id = Guid.NewGuid(), UserId = user3.Id, TotalPrice = 336M, OrderDate = DateTime.UtcNow, OrderStatus = OrderStatus.Completed, ShippingAddressId = address3.Id };
        public static Order order4 = new Order { Id = Guid.NewGuid(), UserId = user4.Id, TotalPrice = 65M, OrderDate = DateTime.UtcNow, OrderStatus = OrderStatus.Canceled, ShippingAddressId = address4.Id };
        public static Order order5 = new Order { Id = Guid.NewGuid(), UserId = user5.Id, TotalPrice = 11M, OrderDate = DateTime.UtcNow, OrderStatus = OrderStatus.Pending, ShippingAddressId = address5.Id };

        public static List<Order> GetOrders()
        {
            return new List<Order>
            {
                order1, order2, order3, order4, order5
            };
        }

        public static OrderItem orderItem1 = new OrderItem { Id = Guid.NewGuid(), OrderId = order1.Id, ProductId = product5.Id, Quantity = 2, Price = 325 };
        public static OrderItem orderItem2 = new OrderItem { Id = Guid.NewGuid(), OrderId = order1.Id, ProductId = product1.Id, Quantity = 1, Price = 15 };
        public static OrderItem orderItem3 = new OrderItem { Id = Guid.NewGuid(), OrderId = order2.Id, ProductId = product2.Id, Quantity = 3, Price = 5 };
        public static OrderItem orderItem4 = new OrderItem { Id = Guid.NewGuid(), OrderId = order2.Id, ProductId = product3.Id, Quantity = 1, Price = 45 };
        public static OrderItem orderItem5 = new OrderItem { Id = Guid.NewGuid(), OrderId = order3.Id, ProductId = product4.Id, Quantity = 4, Price = 11 };
        public static OrderItem orderItem6 = new OrderItem { Id = Guid.NewGuid(), OrderId = order3.Id, ProductId = product5.Id, Quantity = 2, Price = 325 };
        public static OrderItem orderItem7 = new OrderItem { Id = Guid.NewGuid(), OrderId = order4.Id, ProductId = product1.Id, Quantity = 1, Price = 15 };
        public static OrderItem orderItem8 = new OrderItem { Id = Guid.NewGuid(), OrderId = order4.Id, ProductId = product2.Id, Quantity = 3, Price = 5 };
        public static OrderItem orderItem9 = new OrderItem { Id = Guid.NewGuid(), OrderId = order4.Id, ProductId = product3.Id, Quantity = 1, Price = 45 };
        public static OrderItem orderItem10 = new OrderItem { Id = Guid.NewGuid(), OrderId = order5.Id, ProductId = product4.Id, Quantity = 4, Price = 11 };

        public static List<OrderItem> GetOrderItems()
        {
            return new List<OrderItem>
            {
                orderItem1, orderItem2, orderItem3, orderItem4, orderItem5, orderItem6, orderItem7, orderItem8, orderItem9, orderItem10
            };
        }


        public static PaymentMethod paymentMethod1 = new PaymentMethod { Id = Guid.NewGuid(), PaymentType = "CreditCard", Provider = "Visa", CardNumber = "1151184", ExpiryDate = DateTime.UtcNow, UserId = user1.Id };
        public static PaymentMethod paymentMethod2 = new PaymentMethod { Id = Guid.NewGuid(), PaymentType = "CreditCard", Provider = "Visa", CardNumber = "14528795", ExpiryDate = DateTime.UtcNow, UserId = user2.Id };
        public static PaymentMethod paymentMethod3 = new PaymentMethod { Id = Guid.NewGuid(), PaymentType = "CreditCard", Provider = "MasterCard", CardNumber = "14528795", ExpiryDate = DateTime.UtcNow, UserId = user3.Id };
        public static PaymentMethod paymentMethod4 = new PaymentMethod { Id = Guid.NewGuid(), PaymentType = "PayPal", Provider = "PayPal", CardNumber = "14528795", ExpiryDate = DateTime.UtcNow, UserId = user4.Id };
        public static PaymentMethod paymentMethod5 = new PaymentMethod { Id = Guid.NewGuid(), PaymentType = "BankTransfer", Provider = "Bank", CardNumber = "14528795", ExpiryDate = DateTime.UtcNow, UserId = user5.Id };

        public static List<PaymentMethod> GetPaymentMethods()
        {
            return new List<PaymentMethod>
            {
                paymentMethod1, paymentMethod2, paymentMethod3, paymentMethod4, paymentMethod5
            };
        }

        public static Payment payment1 = new Payment { Id = Guid.NewGuid(), OrderId = order5.Id, UserId = user1.Id, Amount = 299.99M, PaymentDate = DateTime.UtcNow, PaymentMethodId = paymentMethod1.Id, PaymentStatus = PaymentStatus.paid };
        public static Payment payment2 = new Payment { Id = Guid.NewGuid(), OrderId = order1.Id, UserId = user2.Id, Amount = 149.99M, PaymentDate = DateTime.UtcNow, PaymentMethodId = paymentMethod1.Id, PaymentStatus = PaymentStatus.paid };
        public static Payment payment3 = new Payment { Id = Guid.NewGuid(), OrderId = order2.Id, UserId = user1.Id, Amount = 89.99M, PaymentDate = DateTime.UtcNow, PaymentMethodId = paymentMethod1.Id, PaymentStatus = PaymentStatus.paid };
        public static Payment payment4 = new Payment { Id = Guid.NewGuid(), OrderId = order3.Id, UserId = user3.Id, Amount = 49.99M, PaymentDate = DateTime.UtcNow, PaymentMethodId = paymentMethod1.Id, PaymentStatus = PaymentStatus.paid };
        public static Payment payment5 = new Payment { Id = Guid.NewGuid(), OrderId = order4.Id, UserId = user5.Id, Amount = 399.99M, PaymentDate = DateTime.UtcNow, PaymentMethodId = paymentMethod1.Id, PaymentStatus = PaymentStatus.paid };

        public static List<Payment> GetPayments()
        {
            return new List<Payment>
            {
                payment1, payment2, payment3, payment4, payment5
            };
        }


        public static ProductImage productImage1 = new ProductImage { Id = Guid.NewGuid(), ProductId = product1.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = true, ImageText = "Sample product image" };
        public static ProductImage productImage2 = new ProductImage { Id = Guid.NewGuid(), ProductId = product4.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = true, ImageText = "Sample product image" };
        public static ProductImage productImage3 = new ProductImage { Id = Guid.NewGuid(), ProductId = product1.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = false, ImageText = "Sample product image" };
        public static ProductImage productImage4 = new ProductImage { Id = Guid.NewGuid(), ProductId = product3.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = true, ImageText = "Sample product image" };
        public static ProductImage productImage5 = new ProductImage { Id = Guid.NewGuid(), ProductId = product1.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = false, ImageText = "Sample product image" };
        public static ProductImage productImage6 = new ProductImage { Id = Guid.NewGuid(), ProductId = product5.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = true, ImageText = "Sample product image" };
        public static ProductImage productImage7 = new ProductImage { Id = Guid.NewGuid(), ProductId = product2.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = true, ImageText = "Sample product image" };
        public static ProductImage productImage8 = new ProductImage { Id = Guid.NewGuid(), ProductId = product2.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = false, ImageText = "Sample product image" };
        public static ProductImage productImage9 = new ProductImage { Id = Guid.NewGuid(), ProductId = product5.Id, ImageURL = "https://picsum.photos/200/300", IsDefault = false, ImageText = "Sample product image" };

        public static List<ProductImage> GetProductImages()
        {
            return new List<ProductImage>
            {
                productImage1, productImage2, productImage3, productImage4,
                productImage5 , productImage6, productImage7, productImage8,
                productImage9
            };
        }

        public static ProductColor productColor1 = new ProductColor { Id = Guid.NewGuid(), ProductId = product1.Id, ColorName = ColorName.Green, Quantity = 15 };
        public static ProductColor productColor2 = new ProductColor { Id = Guid.NewGuid(), ProductId = product2.Id, ColorName = ColorName.Blue, Quantity = 5 };
        public static ProductColor productColor3 = new ProductColor { Id = Guid.NewGuid(), ProductId = product3.Id, ColorName = ColorName.White, Quantity = 3 };
        public static ProductColor productColor4 = new ProductColor { Id = Guid.NewGuid(), ProductId = product4.Id, ColorName = ColorName.Red, Quantity = 6 };
        public static ProductColor productColor5 = new ProductColor { Id = Guid.NewGuid(), ProductId = product5.Id, ColorName = ColorName.Yellow, Quantity = 4 };

        public static List<ProductColor> GetProductColors()
        {
            return new List<ProductColor>
            {
                productColor1, productColor2, productColor3, productColor4, productColor5
            };
        }

        public static ProductSize productSize1 = new ProductSize { Id = Guid.NewGuid(), ProductId = product1.Id, Quantity = 5, SizeValue = SizeValue.Large };
        public static ProductSize productSize2 = new ProductSize { Id = Guid.NewGuid(), ProductId = product2.Id, Quantity = 3, SizeValue = SizeValue.Large };
        public static ProductSize productSize3 = new ProductSize { Id = Guid.NewGuid(), ProductId = product3.Id, Quantity = 10, SizeValue = SizeValue.ExtraLarge };
        public static ProductSize productSize4 = new ProductSize { Id = Guid.NewGuid(), ProductId = product4.Id, Quantity = 14, SizeValue = SizeValue.Medium };
        public static ProductSize productSize5 = new ProductSize { Id = Guid.NewGuid(), ProductId = product5.Id, Quantity = 9, SizeValue = SizeValue.Small };

        public static List<ProductSize> GetProductSizes()
        {
            return new List<ProductSize>
            {
                productSize1, productSize2, productSize3, productSize4, productSize5
            };
        }

        public static Review review1 = new Review { Id = Guid.NewGuid(), ProductId = product1.Id, UserId = user1.Id, Rating = 5, ReviewText = "Excellent!", ReviewDate = DateTime.UtcNow };
        public static Review review2 = new Review { Id = Guid.NewGuid(), ProductId = product2.Id, UserId = user2.Id, Rating = 4, ReviewText = "Very good.", ReviewDate = DateTime.UtcNow };
        public static Review review3 = new Review { Id = Guid.NewGuid(), ProductId = product3.Id, UserId = user3.Id, Rating = 3, ReviewText = "Average.", ReviewDate = DateTime.UtcNow };
        public static Review review4 = new Review { Id = Guid.NewGuid(), ProductId = product4.Id, UserId = user4.Id, Rating = 2, ReviewText = "Not great.", ReviewDate = DateTime.UtcNow };
        public static Review review5 = new Review { Id = Guid.NewGuid(), ProductId = product5.Id, UserId = user5.Id, Rating = 1, ReviewText = "Terrible!", ReviewDate = DateTime.UtcNow };

        public static List<Review> GetReviews()
        {
            return new List<Review>
            {
                review1, review2, review3, review4, review5
            };
        }

        public static Shipment shipment1 = new Shipment { Id = Guid.NewGuid(), OrderId = order1.Id, ShipmentDate = DateTime.UtcNow, ShipmentStatus = ShippingStatus.Pending, AddressId = address1.Id };
        public static Shipment shipment2 = new Shipment { Id = Guid.NewGuid(), OrderId = order2.Id, ShipmentDate = DateTime.UtcNow, ShipmentStatus = ShippingStatus.Returned, AddressId = address2.Id };
        public static Shipment shipment3 = new Shipment { Id = Guid.NewGuid(), OrderId = order3.Id, ShipmentDate = DateTime.UtcNow, ShipmentStatus = ShippingStatus.Shipped, AddressId = address3.Id };
        public static Shipment shipment4 = new Shipment { Id = Guid.NewGuid(), OrderId = order4.Id, ShipmentDate = DateTime.UtcNow, ShipmentStatus = ShippingStatus.Delivered, AddressId = address4.Id };
        public static Shipment shipment5 = new Shipment { Id = Guid.NewGuid(), OrderId = order5.Id, ShipmentDate = DateTime.UtcNow, ShipmentStatus = ShippingStatus.Preparing, AddressId = address5.Id };

        public static List<Shipment> GetShipments()
        {
            return new List<Shipment>
            {
                shipment1, shipment2, shipment3, shipment4, shipment5
            };
        }

        public static UserAddress userAddress1 = new UserAddress { Id = Guid.NewGuid(), UserId = user1.Id, AddressId = address1.Id, IsDefault = true };
        public static UserAddress userAddress2 = new UserAddress { Id = Guid.NewGuid(), UserId = user2.Id, AddressId = address1.Id, IsDefault = false };
        public static UserAddress userAddress3 = new UserAddress { Id = Guid.NewGuid(), UserId = user3.Id, AddressId = address2.Id, IsDefault = true };
        public static UserAddress userAddress4 = new UserAddress { Id = Guid.NewGuid(), UserId = user4.Id, AddressId = address3.Id, IsDefault = false };
        public static UserAddress userAddress5 = new UserAddress { Id = Guid.NewGuid(), UserId = user5.Id, AddressId = address4.Id, IsDefault = true };

        public static List<UserAddress> GetUserAddresses()
        {
            return new List<UserAddress>
            {
                userAddress1, userAddress2, userAddress3, userAddress4, userAddress5
            };
        }
    }
}