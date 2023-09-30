using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    internal sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, StandardResponse<bool>>
    {
        private readonly IBookRepository<Book> _bookRepository;

        public DeleteBookCommandHandler(IBookRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<StandardResponse<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var response = new StandardResponse<bool>()
            {
                StatusCode = ResponseCode.Error,
                Message = ErrorMessage.DeleteBookError,
                Data = false
            };

            var book = await _bookRepository.GetByIdAsync(request.id);

            if (book == null)
            {
                response.StatusCode = ResponseCode.NotFound;
                response.Message = ErrorMessage.BookNotFound;

                return response;
            }

            var delete = await _bookRepository.DeleteAsync(book);

            if (delete)
            {
                response.StatusCode = ResponseCode.Successful;
                response.Data = delete;
                response.Message = string.Empty;
            }

            return response;
        }
    }
}