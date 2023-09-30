using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Books.Queries.GetAllBooks;

internal sealed class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IQueryable<Book>>
{
    private readonly IBookRepository<Book> _bookRepository;
    
    public GetAllBooksQueryHandler(IBookRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IQueryable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var result = _bookRepository.GetAll();

        return await Task.FromResult(result);
    }
}