using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Abstractions;
using Domain.Entities.Identities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(_options.TokenDuracion),
            signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}