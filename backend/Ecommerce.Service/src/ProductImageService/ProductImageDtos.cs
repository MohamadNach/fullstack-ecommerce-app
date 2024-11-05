using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductImageService
{
    public class ProductImageReadDto : BaseReadDto<ProductImage>
    {
        public Guid ProductId { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string ImageText { get; set; } = string.Empty;

        public override void FromEntity(ProductImage entity)
        {
            base.FromEntity(entity);
            ProductId = entity.ProductId;
            ImageURL = entity.ImageURL;
            IsDefault = entity.IsDefault;
            ImageText = entity.ImageText;
        }
    }
    public class ProductImageCreateDto : ICreateDto<ProductImage>
    {
        public Guid ProductId { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string ImageText { get; set; } = string.Empty;
        public ProductImage CreateEntity()
        {
            return new ProductImage
            {
                ProductId = ProductId,
                ImageURL = ImageURL,
                IsDefault = IsDefault,
                ImageText = ImageText
            };
        }
    }
    public class ProductImageUpdateDto : IUpdateDto<ProductImage>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string ImageText { get; set; } = string.Empty;
        public ProductImage UpdateEntity(ProductImage entity)
        {
            entity.ProductId = ProductId;
            entity.ImageURL = ImageURL;
            entity.IsDefault = IsDefault;
            entity.ImageText = ImageText;
            return entity;
        }
    }
}