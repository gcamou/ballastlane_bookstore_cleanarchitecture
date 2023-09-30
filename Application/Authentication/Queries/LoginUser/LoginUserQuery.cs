using Domain.Core;
using MediatR;

namespace Application.Authentication.Queries.LoginUser;

public sealed record LoginUserQuery(string username, string password) 
    : IRequest<StandardResponse<LoginUserResponse>>;