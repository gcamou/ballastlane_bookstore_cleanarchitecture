using Application.Books.Queries.GetBookById;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Core.Constants;
using Moq;
using FizzWare.NBuilder;
using Domain.Core.Enum;

namespace Test.Application.Books.Queries.GetBookById;
public class GetBookByIdQueryHandlerTest
{
    [Fact]
    public async Task Should_ReturnBookResponse_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = Builder<Book>.CreateNew()
                                    .With(book => book.Id = bookId)
                                    .Build();
        
        var mockRepository = new Mock<IBookRepository<Book>>();
        mockRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);

        var query = new GetBookByIdQuery(bookId);
        var handler = new GetBookByIdQueryHandler(mockRepository.Object);

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
        var nonExistingBookId = 999;  // Assuming 999 is not a valid book ID
        
        var mockRepository = new Mock<IBookRepository<Book>>();
        mockRepository.Setup(repo => repo.GetByIdAsync(nonExistingBookId)).ReturnsAsync((Book)null);

        var query = new GetBookByIdQuery(Guid.NewGuid());
        var handler = new GetBookByIdQueryHandler(mockRepository.Object);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, response.StatusCode);
        Assert.Equal(ErrorMessage.BookNotFound, response.Message);
        Assert.Null(response.Data);
    }
}
