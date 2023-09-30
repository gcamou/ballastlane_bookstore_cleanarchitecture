namespace Domain.Abstractions.Base;

public interface IGenericRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> GetByIdAsync(object id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
}