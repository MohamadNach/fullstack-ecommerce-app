using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Service.src.Shared
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseEntity
        where TReadDto : IReadDto<T>, new()
        where TCreateDto : ICreateDto<T>
        where TUpdateDto : IUpdateDto<T>
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<TReadDto> CreateAsync(TCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var entity = createDto.CreateEntity();
            await _repository.CreateAsync(entity);

            var readDto = new TReadDto();
            readDto.FromEntity(entity);

            return readDto;
        }

        public virtual async Task<TReadDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid ID", nameof(id));
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var entity = await _repository.GetAsync(e => e.Id == id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity = updateDto.UpdateEntity(entity);
            await _repository.UpdateByIdAsync(entity);

            var readDto = new TReadDto();
            readDto.FromEntity(entity);

            return readDto;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid ID", nameof(id));
            return await _repository.DeleteByIdAsync(id);
        }

        public virtual async Task<TReadDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid ID", nameof(id));

            var entity = await _repository.GetAsync(e => e.Id == id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            var readDto = new TReadDto();
            readDto.FromEntity(entity);

            return readDto;
        }

        public virtual async Task<PaginatedResult<TReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var entities = await _repository.GetAllAsync(paginationOptions);

            var readDtos = entities.Items.Select(entity =>
            {
                var readDto = new TReadDto();
                readDto.FromEntity(entity);
                return readDto;
            });

            return new PaginatedResult<TReadDto>
            {
                Items = readDtos,
                CurrentPage = entities.CurrentPage,
                TotalPages = entities.TotalPages
            };
        }
    }
}
