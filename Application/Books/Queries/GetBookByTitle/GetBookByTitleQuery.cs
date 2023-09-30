using Application.Books.Queries.GetBookById;
using Domain.Core;
using MediatR;

namespace Application.Books.Queries.GetBookByTitle;

public sealed record GetBookByTitleQuery(string title) 
    : IRequest<StandardResponse<BookResponse>>;
