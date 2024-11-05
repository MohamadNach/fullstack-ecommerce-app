using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductColorService
{
    public class ProductColorReadDto : BaseReadDto<ProductColor>
    {
        public Guid ProductId { get; set; }
        public string ColorName { get; set; }

        public override void FromEntity(ProductColor entity)
        {
            base.FromEntity(entity);
            ProductId = entity.ProductId;
            ColorName = entity.ColorName.ToString();
        }
    }
    public class ProductColorCreateDto : ICreateDto<ProductColor>
    {
        public Guid ProductId { get; set; }
        public string ColorName { get; set; }
        public ProductColor CreateEntity()
        {
            return new ProductColor
            {
                ProductId = ProductId,
                ColorName = (ColorName)System.Enum.Parse(typeof(ColorName), ColorName)
            };
        }
    }
    public class ProductColorUpdateDto : IUpdateDto<ProductColor>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ColorName { get; set; }
        public ProductColor UpdateEntity(ProductColor entity)
        {
            entity.ProductId = ProductId;
            entity.ColorName = (ColorName)System.Enum.Parse(typeof(ColorName), ColorName);
            return entity;
        }
    }

}