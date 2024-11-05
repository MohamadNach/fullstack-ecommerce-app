
namespace Ecommerce.Domain.src.Shared
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }

}