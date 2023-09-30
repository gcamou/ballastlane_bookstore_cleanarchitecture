using Domain.Abstractions.Base;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBookRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    public IQueryable<Book> GetByTitle(string title);
}