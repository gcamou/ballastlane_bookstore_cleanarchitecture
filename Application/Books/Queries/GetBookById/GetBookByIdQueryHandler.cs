using MediatR;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;
using Domain.Exceptions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;

namespace Application.Books.Queries.GetBookById;

internal sealed class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, StandardResponse<BookResponse>>
{
    private readonly IBookRepository<Book> _bookRepository;

    public GetBookByIdQueryHandler(IBookRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<StandardResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new StandardResponse<BookResponse>()
        {
            Message = ErrorMessage.BookNotFound,
            StatusCode = ResponseCode.NotFound
        };

        var book = await _bookRepository.GetByIdAsync(request.bookId);

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