using Application.Books.Queries.GetBookByTitle;
using Domain.Abstractions;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using FizzWare.NBuilder;
using Moq;

namespace Test.Application.Books.Queries.GetBookByTitle;
public class GetBookByTitleQueryHandlerTests
{
    [Fact]
    public async Task Should_ReturnBookResponse_WhenBookExists()
    {
        // Arrange
        var title = "Sample Book";
        var book = Builder<Book>.CreateNew()
                                    .With(book => book.Title = title)
                                    .Build();

        var mockRepository = new Mock<IBookRepository<Book>>();
        mockRepository.Setup(repo => repo.GetByTitle(title)).Returns(new[] { book }.AsQueryable());

        var query = new GetBookByTitleQuery(title);
        var handler = new GetBookByTitleQueryHandler(mockRepository.Object);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, response.StatusCode);
        Assert.Empty(response.Message);
        Assert.NotNull(response.Data);
        Assert.Equal(book.Title, response.Data.Title);
    }

    [Fact]
    public async Task Should_ReturnBookNotFound_WhenBookDoesNotExist()
    {
        // Arrange
        var nonExistingTitle = "Nonexistent Book";

        var mockRepository = new Mock<IBookRepository<Book>>();
        mockRepository.Setup(repo => repo.GetByTitle(nonExistingTitle)).Returns(Enumerable.Empty<Book>().AsQueryable());

        var query = new GetBookByTitleQuery(nonExistingTitle);
        var handler = new GetBookByTitleQueryHandler(mockRepository.Object);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, response.StatusCode);
        Assert.Equal(ErrorMessage.BookNotFound, response.Message);
        Assert.Null(response.Data);
    }
}
