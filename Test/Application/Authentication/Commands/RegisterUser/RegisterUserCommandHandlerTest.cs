using Moq;
using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Domain.Abstractions;
using Application.Authentication.Commands.RegisterUser;
using Domain.Core.Enum;

namespace Test.Application.Authentication.Commands.RegisterUser;
public class RegisterUserCommandHandlerTest
{
    private Mock<UserManager<ApplicationUser>> _mockUserManager;
    private Mock<RoleManager<ApplicationRole>> _mockRoleManager;
    private Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;

    public RegisterUserCommandHandlerTest()
    {
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _mockRoleManager = new Mock<RoleManager<ApplicationRole>>(
            Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
        _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
    }

    [Fact]
    public async Task Should_Register_User_Successfully()
    {
        // Arrange 
        var handler = new RegisterUserCommandHandler(
            _mockUserManager.Object, _mockRoleManager.Object, _mockJwtTokenGenerator.Object);

        var command = new RegisterUserCommand("username", "password", "email", "role");

        _mockRoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockJwtTokenGenerator.Setup(x => x.GenerateToken(It.IsAny<ApplicationUser>()))
            .Returns("mocked_token");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Successful, result.StatusCode);
    }

    [Fact]
    public async Task Should_ReturnError_WhenRoleDoesNotExist()
    {
        // Arrange
        var handler = new RegisterUserCommandHandler(
            _mockUserManager.Object, _mockRoleManager.Object, _mockJwtTokenGenerator.Object);

        var command = new RegisterUserCommand("username", "password", "email", "notexist");

        _mockRoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
    }

    [Fact]
    public async Task Should_ReturnError_WhenRegistrationFails()
    {
        // Arrange
        var handler = new RegisterUserCommandHandler(
            _mockUserManager.Object, _mockRoleManager.Object, _mockJwtTokenGenerator.Object);

        var command = new RegisterUserCommand("username", "password", "email", "role");

        var identityErrors = new List<IdentityError>
    {
        new IdentityError { Description = "Registration failed due to XYZ error." }
    };

        var identityResult = IdentityResult.Failed(identityErrors.ToArray());

        _mockRoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(identityResult);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
        Assert.Equal("Registration failed due to XYZ error.", result.Message);
    }

    [Fact]
    public async Task Should_ReturnError_WhenTokenGenerationFails()
    {
        // Arrange
        var handler = new RegisterUserCommandHandler(
            _mockUserManager.Object, _mockRoleManager.Object, _mockJwtTokenGenerator.Object);

        var command = new RegisterUserCommand("username", "password", "email", "role");

        _mockRoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockJwtTokenGenerator.Setup(x => x.GenerateToken(It.IsAny<ApplicationUser>()))
            .Returns<string>(null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResponseCode.Error, result.StatusCode);
        Assert.Equal("Token generation failed.", result.Message);
    }
}
