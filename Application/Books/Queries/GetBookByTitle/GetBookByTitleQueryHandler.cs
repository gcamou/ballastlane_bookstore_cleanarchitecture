using Application.Books.Queries.GetBookById;
using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Books.Queries.GetBookByTitle;
internal sealed class GetBookByTitleQueryHandler : IRequestHandler<GetBookByTitleQuery, StandardResponse<BookResponse>>
{
    private readonly IBookRepository<Book> _bookRepository;

    public GetBookByTitleQueryHandler(IBookRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<StandardResponse<BookResponse>> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
    {
       var result = new StandardResponse<BookResponse>()
        {
            Message = ErrorMessage.BookNotFound,
            StatusCode = ResponseCode.NotFound
        };

        var book = _bookRepository.GetByTitle(request.title)
                                    .SingleOrDefault();

        if (book != null)
        {
            var bookResponse = book.Adapt<BookResponse>();

            result.Message = string.Empty;
            result.StatusCode = ResponseCode.Successful;
            result.Data = bookResponse;
        }

        return result;
    }
}