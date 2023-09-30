using MediatR;
using Domain.Entities;

namespace Application.Books.Queries.GetAllBooks;

public sealed record GetAllBooksQuery() : IRequest<IQueryable<Book>>;
