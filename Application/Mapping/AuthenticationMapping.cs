using Application.Authentication.Commands.RegisterUser;
using Domain.Entities.Identities;
using Mapster;

namespace Application.Mapping;

public class AuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config = TypeAdapterConfig.GlobalSettings;

        TypeAdapterConfig<RegisterUserCommand, ApplicationUser>.NewConfig()
            .Map(dest => dest.PasswordHash, src => src.password)
            .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

        TypeAdapterConfig<ApplicationUser, RegisterUserResponse>.NewConfig()
            .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
    }
}