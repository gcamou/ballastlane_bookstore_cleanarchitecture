using Domain.Core;
using MediatR;

namespace Application.Authentication.Commands.RegisterUser;

public sealed record RegisterUserCommand(string username, string password, string email, string role) 
    : IRequest<StandardResponse<RegisterUserResponse>>;