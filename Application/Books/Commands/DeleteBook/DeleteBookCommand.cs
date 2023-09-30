using Domain.Core;
using MediatR;

namespace Application.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid id) 
    : IRequest<StandardResponse<bool>>;