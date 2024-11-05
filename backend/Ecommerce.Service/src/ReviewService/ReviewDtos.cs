using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ReviewService
{
    public class ReviewReadDto : BaseReadDto<Review>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }

        public override void FromEntity(Review entity)
        {
            base.FromEntity(entity);
            ReviewDate = entity.ReviewDate;
            Rating = entity.Rating;
            ReviewText = entity.ReviewText;
        }
    }
    public class ReviewCreateDto : ICreateDto<Review>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public Review CreateEntity()
        {
            return new Review
            {
                ReviewDate = ReviewDate,
                Rating = Rating,
                ReviewText = ReviewText
            };
        }
    }
    public class ReviewUpdateDto : IUpdateDto<Review>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public Review UpdateEntity(Review entity)
        {
            entity.ReviewDate = ReviewDate;
            entity.Rating = Rating;
            entity.ReviewText = ReviewText;
            return entity;
        }
    }
}