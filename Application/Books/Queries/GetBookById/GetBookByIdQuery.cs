using Domain.Core;
using MediatR;

namespace Application.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(Guid bookId) 
    : IRequest<StandardResponse<BookResponse>>;