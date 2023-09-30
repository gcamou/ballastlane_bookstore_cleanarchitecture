using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Books.Commands.UpdateBook;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, StandardResponse<UpdateBookResponse>>
{
    private readonly IBookRepository<Book> _bookRepository;

    public UpdateBookCommandHandler(IBookRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<StandardResponse<UpdateBookResponse>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var response = new StandardResponse<UpdateBookResponse>()
        {
            StatusCode = ResponseCode.Successful
        };

        var book = await _bookRepository.GetByIdAsync(request.id);

        if (book == null)
        {
            response.StatusCode = ResponseCode.NotFound;
            response.Message = ErrorMessage.BookNotFound;

            return response;
        }

        request.Adapt<UpdateBookCommand, Book>(book);

        var update = await _bookRepository.UpdateAsync(book);

        if (!update)
        {
            response.StatusCode = ResponseCode.Error;
            response.Message = ErrorMessage.UpdateBookError;

            return response;
        }

        var result = book.Adapt<UpdateBookResponse>();
        response.Data = result;

        return response;
    }
}