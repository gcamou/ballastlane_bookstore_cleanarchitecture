using Domain.Core;
using MediatR;

namespace Application.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(Guid id, string author, string title, string description) 
    : IRequest<StandardResponse<UpdateBookResponse>>;
