using Domain.Core.Constants;
using Domain.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Core;
using Domain.Core.Enum;
using Domain.Abstractions;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Authentication.Queries.LoginUser;

internal sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, StandardResponse<LoginUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserQueryHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<StandardResponse<LoginUserResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var response = new StandardResponse<LoginUserResponse>()
        {
            Message = string.Format(ErrorMessage.LoginUserNotFound, request.username),
            StatusCode = ResponseCode.NotFound
        };

        var user = await _userManager.FindByNameAsync(request.username);

        if (user != null)
        {
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.password);

            if (isPasswordValid)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles.FirstOrDefault());

                response.Message = string.Empty;
                response.StatusCode = ResponseCode.Successful;
                response.Data = new LoginUserResponse()
                {
                    username = user.UserName,
                    token = token
                };
            }
        }
        
        return response;
    }
}