using Domain.Entities.Identities;

namespace Domain.Abstractions;
public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}