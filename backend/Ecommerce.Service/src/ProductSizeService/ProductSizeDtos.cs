using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductSizeService
{
    public class ProductSizeReadDto : BaseReadDto<ProductSize>
    {
        public Guid ProductId { get; set; }
        public SizeValue SizeValue { get; set; }
        public int Quantity { get; set; }

        public override void FromEntity(ProductSize entity)
        {
            base.FromEntity(entity);
            ProductId = entity.ProductId;
            SizeValue = entity.SizeValue;
            Quantity = entity.Quantity;
        }
    }
    public class ProductSizeCreateDto : ICreateDto<ProductSize>
    {
        public Guid ProductId { get; set; }
        public SizeValue SizeValue { get; set; }
        public int Quantity { get; set; }
        public ProductSize CreateEntity()
        {
            return new ProductSize
            {
                ProductId = ProductId,
                SizeValue = SizeValue,
                Quantity = Quantity
            };
        }
    }
    public class ProductSizeUpdateDto : IUpdateDto<ProductSize>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public SizeValue SizeValue { get; set; }
        public int Quantity { get; set; }
        public ProductSize UpdateEntity(ProductSize entity)
        {
            entity.ProductId = ProductId;
            entity.SizeValue = SizeValue;
            entity.Quantity = Quantity;
            return entity;
        }
    }
}