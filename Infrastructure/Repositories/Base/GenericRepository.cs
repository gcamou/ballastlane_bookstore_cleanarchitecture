using Domain.Abstractions.Base;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            var result = await _dbSet.FindAsync(id);

            return result;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            int result = await _dbContext.SaveChangesAsync();

            return result != null? entity : null;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}