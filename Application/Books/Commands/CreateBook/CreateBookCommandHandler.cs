using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Books.Commands.CreateBook;

internal sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, StandardResponse<CreateBookResponse>>
{
    private readonly IBookRepository<Book> _bookRepository;

    public CreateBookCommandHandler(IBookRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<StandardResponse<CreateBookResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var result = new StandardResponse<CreateBookResponse>()
        {
            Message = ErrorMessage.CreateBookError,
            StatusCode = ResponseCode.Error
        };

        var book = request.Adapt<Book>();

        var newBook = await _bookRepository.InsertAsync(book);

        if (newBook != null)
        {
            var bookResponse = newBook.Adapt<CreateBookResponse>();

            result.Message = string.Empty;
            result.StatusCode = ResponseCode.Successful;
            result.Data = bookResponse;
        }

        return result;
    }
}
