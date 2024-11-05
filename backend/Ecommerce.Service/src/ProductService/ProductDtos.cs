using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductService
{
    public class ProductReadDto : BaseReadDto<Product>
    {
        public string ProductTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public string BrandName { get; set; } = string.Empty;

        public override void FromEntity(Product entity)
        {
            base.FromEntity(entity);
            ProductTitle = entity.ProductTitle;
            Description = entity.Description;
            Price = entity.Price;
            Quantity = entity.Quantity;
            CategoryId = entity.CategoryId;
            BrandName = entity.BrandName;
        }
    }
    public class ProductCreateDto : ICreateDto<Product>
    {
        public string ProductTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public Product CreateEntity()
        {
            return new Product
            {
                ProductTitle = ProductTitle,
                Description = Description,
                Price = Price,
                Quantity = Quantity,
                CategoryId = CategoryId,
                BrandName = BrandName
            };
        }
    }
    public class ProductUpdateDto : IUpdateDto<Product>
    {
        public Guid Id { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public Product UpdateEntity(Product entity)
        {
            entity.ProductTitle = ProductTitle;
            entity.Description = Description;
            entity.Price = Price;
            entity.Quantity = Quantity;
            entity.CategoryId = CategoryId;
            entity.BrandName = BrandName;
            return entity;
        }
    }
}