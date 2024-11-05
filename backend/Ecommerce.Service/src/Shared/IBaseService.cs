using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Service.src.Shared
{
    public interface IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseEntity
        where TReadDto : IReadDto<T>, new()
        where TCreateDto : ICreateDto<T>
        where TUpdateDto : IUpdateDto<T>
    {
        Task<TReadDto> CreateAsync(TCreateDto createDto);
        Task<TReadDto> UpdateAsync(Guid id, TUpdateDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<TReadDto> GetByIdAsync(Guid id);
        Task<PaginatedResult<TReadDto>> GetAllAsync(PaginationOptions paginationOptions);
    }
}
