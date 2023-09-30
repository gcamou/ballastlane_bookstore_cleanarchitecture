using Domain.Core;
using MediatR;

namespace Application.Books.Commands.CreateBook;

public sealed record CreateBookCommand(string author, string title, string description)
    : IRequest<StandardResponse<CreateBookResponse>>;
