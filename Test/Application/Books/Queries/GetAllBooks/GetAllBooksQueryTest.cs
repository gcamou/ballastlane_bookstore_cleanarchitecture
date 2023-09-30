using Application.Books.Queries.GetAllBooks;
using Domain.Abstractions;
using Domain.Entities;
using FizzWare.NBuilder;
using Moq;

namespace Test.Application.Books.Queries.GetAllBooks;
public class GetAllBooksQueryHandlerTest
{
    [Fact]
    public async Task Should_ReturnAllBooks()
    {
        // Arrange
        var books = Builder<Book>.CreateListOfSize(5).Build();

        var mockRepository = new Mock<IBookRepository<Book>>();
        mockRepository.Setup(repo => repo.GetAll()).Returns(books.AsQueryable());

        var query = new GetAllBooksQuery();
        var handler = new GetAllBooksQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
        Assert.Equal(books.Select(b => b.Title), result.Select(b => b.Title));
    }
}
