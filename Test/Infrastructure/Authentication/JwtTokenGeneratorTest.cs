using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Entities.Identities;
using Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace Test.Infrastructure.Authentication;

public class JwtTokenGeneratorTest
{
    [Fact]
    public void Should_Create_Token()
    {
        var mockOptions = new Mock<IOptions<JwtOptions>>();
        mockOptions.Setup(x => x.Value).Returns(new JwtOptions
        {
            SecretKey = "DocumentManagementKeySecretTemporal",
            Issuer = "TestUser",
            Audience = "User",
            TokenDuracion = 1
        });

        var jwtTokenGenerator = new JwtTokenGenerator(mockOptions.Object);
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com"
        };

        // Act
        var token = jwtTokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public void Should_GenerateToken_Token_And_Return_Valid_Token()
    {
        // Arrange
        var jwtOptions = new JwtOptions
        {
            SecretKey = "DocumentManagementKeySecretTemporal",
            Issuer = "TestUser",
            Audience = "User",
            TokenDuracion = 1
        };

        var jwtTokenGenerator = new JwtTokenGenerator(Options.Create(jwtOptions));
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com"
        };

        // Act
        var token = jwtTokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.True(ValidateToken(token, jwtOptions.SecretKey));
    }

    [Fact]
    public void Should_GenerateToken_Token_And_Return_Not_Valid_Token()
    {
        // Arrange
        var jwtOptions = new JwtOptions
        {
            SecretKey = "DocumentManagementKeySecretTemporal",
            Issuer = "TestUser",
            Audience = "User",
            TokenDuracion = 1
        };

        var invalidSecretKey = "invalidSecretKey";

        var jwtTokenGenerator = new JwtTokenGenerator(Options.Create(jwtOptions));
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com"
        };

        // Act
        var token = jwtTokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.False(ValidateToken(token, invalidSecretKey));
    }

    private bool ValidateToken(string token, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "TestUser",
                ValidAudience = "User",
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}