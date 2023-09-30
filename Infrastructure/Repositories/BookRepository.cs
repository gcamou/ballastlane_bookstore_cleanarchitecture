using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository<Book>
{
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }

    public IQueryable<Book> GetByTitle(string title)
    {
        return GetAll().Where(book =>
                book.Title == title);
    }
}