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
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTimestamps(modelBuilder);

            /* Seeding the database - comment out after migrations */
            modelBuilder.Entity<User>().HasData(SeedingData.GetUsers());
            modelBuilder.Entity<Address>().HasData(SeedingData.GetAddresses());
            modelBuilder.Entity<UserAddress>().HasData(SeedingData.GetUserAddresses());
            modelBuilder.Entity<Category>().HasData(SeedingData.GetCategories());
            modelBuilder.Entity<Product>().HasData(SeedingData.GetProducts());
            modelBuilder.Entity<ProductColor>().HasData(SeedingData.GetProductColors());
            modelBuilder.Entity<ProductSize>().HasData(SeedingData.GetProductSizes());
            modelBuilder.Entity<ProductImage>().HasData(SeedingData.GetProductImages());
            modelBuilder.Entity<Cart>().HasData(SeedingData.GetCarts());
            modelBuilder.Entity<CartItem>().HasData(SeedingData.GetCartItems());
            modelBuilder.Entity<Order>().HasData(SeedingData.GetOrders());
            modelBuilder.Entity<OrderItem>().HasData(SeedingData.GetOrderItems());
            modelBuilder.Entity<Payment>().HasData(SeedingData.GetPayments());
            modelBuilder.Entity<PaymentMethod>().HasData(SeedingData.GetPaymentMethods());
            modelBuilder.Entity<Review>().HasData(SeedingData.GetReviews());
            modelBuilder.Entity<Shipment>().HasData(SeedingData.GetShipments());
        }

        private void ConfigureTimestamps(ModelBuilder modelBuilder)
        {
            var entitiesWithTimestamps = new[]
            {
                typeof(Address),
                typeof(Category),
                typeof(User),
                typeof(UserAddress),
                typeof(Cart),
                typeof(CartItem),
                typeof(Order),
                typeof(OrderItem),
                typeof(Payment),
                typeof(PaymentMethod),
                typeof(Product),
                typeof(ProductColor),
                typeof(ProductImage),
                typeof(ProductSize),
                typeof(Review),
                typeof(Shipment)
            };

            foreach (var entityType in entitiesWithTimestamps)
            {
                modelBuilder.Entity(entityType).Property("CreatedAt")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity(entityType).Property("UpdatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }
    }
}
