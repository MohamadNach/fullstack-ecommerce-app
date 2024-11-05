using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ecommerce.Domain.src.Model;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateByIdAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<T>> GetAllAsync(PaginationOptions paginationOptions)
        {
            // Check for valid page number and page size
            if (paginationOptions.Page < 1)
                paginationOptions.Page = 1;
            if (paginationOptions.PerPage <= 0)
                paginationOptions.PerPage = 10; // default value

            var totalEntity = await _dbSet.CountAsync();
            IQueryable<T> query = _dbSet;

            var entities = await query.AsNoTracking()
                .Skip((paginationOptions.Page - 1) * paginationOptions.PerPage)
                .Take(paginationOptions.PerPage)
                .ToListAsync();

            var totalPages = totalEntity > 0
                ? (int)Math.Ceiling(totalEntity / (double)paginationOptions.PerPage)
                : 0; // If there are no entities

            return new PaginatedResult<T>
            {
                Items = entities,
                TotalPages = totalPages,
                CurrentPage = paginationOptions.Page,
            };
        }


    }
}