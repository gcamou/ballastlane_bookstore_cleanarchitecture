using Domain.Core.Constants;
using Domain.Entities.Identities;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Application.Authentication.Queries.LoginUser;
using Domain.Core.Enum;

namespace Test.Application.Authentication.Queries.LoginUser;
public class LoginUserQueryHandlerTest
{
    private Mock<UserManager<ApplicationUser>> _mockUserManager;
    private Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;

    public LoginUserQueryHandlerTest()
    {
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
    }

    [Fact]
    public async Task Should_LoginUserSuccessfully()
    {
        // Arrange
        var role = "role";
        var handler = new LoginUserQueryHandler(_mockUserManager.Object, _mockJwtTokenGenerator.Object);

        var command = new LoginUserQuery("username", "password");

        var user = new ApplicationUser { UserName = command.username };
        _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, command.password)).ReturnsAsync(true);
        _mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(() => 
        {
            return new List<string>{ role };
        });
        _mockJwtTokenGenerator.Setup(x => x.GenerateToken(user, role)).Returns("mocked_token");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, result.StatusCode);
        Assert.Equal("", result.Message);
        Assert.Equal(command.username, result.Data.username);
        Assert.Equal("mocked_token", result.Data.token);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var handler = new LoginUserQueryHandler(_mockUserManager.Object, _mockJwtTokenGenerator.Object);

        var command = new LoginUserQuery("username", "password");

        _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, result.StatusCode);
        Assert.Equal(string.Format(ErrorMessage.LoginUserNotFound, command.username), result.Message);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenPasswordIsInvalid()
    {
        // Arrange
        var handler = new LoginUserQueryHandler(_mockUserManager.Object, _mockJwtTokenGenerator.Object);

        var command = new LoginUserQuery("username", "password");

        var user = new ApplicationUser { UserName = command.username };
        _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, command.password)).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.NotFound, result.StatusCode);
        Assert.Equal(string.Format(ErrorMessage.LoginUserNotFound, command.username), result.Message);
    }
}
