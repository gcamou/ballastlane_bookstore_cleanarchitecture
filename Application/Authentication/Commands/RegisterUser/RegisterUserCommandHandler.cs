using Domain.Abstractions;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Entities.Identities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, StandardResponse<RegisterUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    [assembly: InternalsVisibleTo("Test")]
    public async Task<StandardResponse<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = new StandardResponse<RegisterUserResponse>()
        {
            StatusCode = ResponseCode.Error
        };

        var user = request.Adapt<ApplicationUser>();

        if (!await _roleManager.RoleExistsAsync(request.role))
        {
            response.Message = string.Format(ErrorMessage.RoleNotFound, request.role);
            return response;
        }

        var result = await _userManager.CreateAsync(user, request.password);

        if (!result.Succeeded)
        {
            response.Message = result.Errors.SingleOrDefault()?.Description;
            return response;
        }

        await _userManager.AddToRoleAsync(user, request.role);

        var token = _jwtTokenGenerator.GenerateToken(user, request.role);

        if (string.IsNullOrEmpty(token))
        {
            response.Message = ErrorMessage.TokenGenerationError;
            return response;
        }

        var userResponse = user.Adapt<RegisterUserResponse>();

        userResponse.Token = token;
        userResponse.Role = request.role;

        response.StatusCode = ResponseCode.Successful;
        response.Message = string.Empty;
        response.Data = userResponse;

        return response;
    }
}