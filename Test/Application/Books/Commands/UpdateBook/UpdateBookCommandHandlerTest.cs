using Application.Books.Commands.UpdateBook;
using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using FizzWare.NBuilder;
using MediatR;
using Moq;
using Xunit;

namespace Test.Application.Books.Commands.UpdateBook;
public class UpdateBookCommandHandlerTest
{
    private Mock<IBookRepository<Book>> _mockBookRepository;

    public UpdateBookCommandHandlerTest()
    {
        _mockBookRepository = new Mock<IBookRepository<Book>>();
    }

    [Fact]
    public async Task Should_UpdateBookSuccessfully()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(_mockBookRepository.Object);

        var title = "title";
        var author = "author";
        var description = "description";

        var command = new UpdateBookCommand(Guid.NewGuid(), author, title, description);

        var existingBook = Builder<Book>.CreateNew().Build();
        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync(existingBook);
        _mockBookRepository.Setup(x => x.UpdateAsync(existingBook)).ReturnsAsync(() =>
        {
            existingBook.Author = author;
            existingBook.Title = title;
            existingBook.Description = description;

            return true;
        });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, result.StatusCode);
        Assert.Equal(existingBook.Author, author);
        Assert.Equal(existingBook.Title, title);
        Assert.Equal(existingBook.Description, description);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBookNotFound()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(_mockBookRepository.Object);

        var command = new UpdateBookCommand(Guid.NewGuid(), "author", "title", "description");

        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync((Book)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, result.StatusCode);
        Assert.Null(result.Data);
        Assert.Equal(ErrorMessage.BookNotFound, result.Message);
    }

    [Fact]
    public async Task Should_ReturnError_WhenBookUpdateFails()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(_mockBookRepository.Object);

        var command = new UpdateBookCommand(Guid.NewGuid(), "author", "title", "description");

        var existingBook = Builder<Book>.CreateNew().Build();
        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync(existingBook);
        _mockBookRepository.Setup(x => x.UpdateAsync(existingBook)).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
        Assert.Null(result.Data);
        Assert.Equal(ErrorMessage.UpdateBookError, result.Message);
    }

    [Fact]
    public async Task Should_ReturnError_WhenBookIsNull()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(_mockBookRepository.Object);

        var command = new UpdateBookCommand(Guid.NewGuid(), "author", "title", "description");

        _mockBookRepository.Setup(x => x.GetByIdAsync(command.id)).ReturnsAsync((Book)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, result.StatusCode);
        Assert.Null(result.Data);
        Assert.Equal(ErrorMessage.BookNotFound, result.Message);
    }
}
