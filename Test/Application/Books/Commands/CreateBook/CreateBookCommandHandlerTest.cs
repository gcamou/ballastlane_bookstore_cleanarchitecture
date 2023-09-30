using Application.Books.Commands.CreateBook;
using Domain.Abstractions;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities;
using Mapster;
using Moq;

namespace Test.Application.Books.Commands.CreateBook;
public class CreateBookCommandHandlerTest
{
    private Mock<IBookRepository<Book>> _mockBookRepository;

    public CreateBookCommandHandlerTest()
    {
        _mockBookRepository = new Mock<IBookRepository<Book>>();
    }

    [Fact]
    public async Task Should_CreateBookSuccessfully()
    {
        // Arrange
        var handler = new CreateBookCommandHandler(_mockBookRepository.Object);

        var command = new CreateBookCommand("title", "author", "description");

        var book = command.Adapt<Book>();
        _mockBookRepository.Setup(x => x.InsertAsync(It.IsAny<Book>())).ReturnsAsync(book);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, result.StatusCode);
        Assert.Equal("", result.Message);
        // Add more assertions based on expected behavior
    }

    [Fact]
    public async Task Should_ReturnError_WhenBookCreationFails()
    {
        // Arrange
        var handler = new CreateBookCommandHandler(_mockBookRepository.Object);

        var command = new CreateBookCommand("title", "author", "description");

        _mockBookRepository.Setup(x => x.InsertAsync(It.IsAny<Book>())).ReturnsAsync((Book)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
        Assert.Equal(ErrorMessage.CreateBookError, result.Message);
    }
}
