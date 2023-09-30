using System.Threading;
using System.Threading.Tasks;
using Application.Books.Commands.DeleteBook;
using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Entities;
using MediatR;
using Moq;
using Xunit;
using FizzWare.NBuilder;
using Domain.Core.Enum;

namespace Test.Application.Books.Commands.DeleteBook;
public class DeleteBookCommandHandlerTest
{
    private Mock<IBookRepository<Book>> _mockBookRepository;

    public DeleteBookCommandHandlerTest()
    {
        _mockBookRepository = new Mock<IBookRepository<Book>>();
    }

    [Fact]
    public async Task Should_DeleteBookSuccessfully()
    {
        // Arrange
        var handler = new DeleteBookCommandHandler(_mockBookRepository.Object);

        var command = new DeleteBookCommand(Guid.NewGuid());

        var book = Builder<Book>.CreateNew().Build();
        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync(book);
        _mockBookRepository.Setup(x => x.DeleteAsync(book)).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, result.StatusCode);
        Assert.True(result.Data);
        Assert.Equal("", result.Message);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBookNotFound()
    {
        // Arrange
        var handler = new DeleteBookCommandHandler(_mockBookRepository.Object);

        var command = new DeleteBookCommand(Guid.NewGuid());

        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync((Book)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, result.StatusCode);
        Assert.False(result.Data);
        Assert.Equal(ErrorMessage.BookNotFound, result.Message);
    }

    [Fact]
    public async Task Should_ReturnError_WhenBookDeletionFails()
    {
        // Arrange
        var handler = new DeleteBookCommandHandler(_mockBookRepository.Object);

        var command = new DeleteBookCommand(Guid.NewGuid());

        var book = Builder<Book>.CreateNew().Build();
        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync(book);
        _mockBookRepository.Setup(x => x.DeleteAsync(book)).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
        Assert.False(result.Data);
        Assert.Equal(ErrorMessage.DeleteBookError, result.Message);
    }
}
