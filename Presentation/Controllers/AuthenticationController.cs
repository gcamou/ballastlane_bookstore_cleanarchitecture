using Application.Authentication.Queries.LoginUser;
using Domain.Core.Constants;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Core.Enum;
using Application.Authentication.Commands.RegisterUser;

namespace Presentation.Controllers;

[Route("auth")]
public sealed class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(ErrorMessage.BadRequest);

        var Command = request.Adapt<RegisterUserCommand>();

        var response = await Sender.Send(Command, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status500InternalServerError, response.Message);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(ErrorMessage.BadRequest);

        var query = request.Adapt<LoginUserQuery>();

        var response = await Sender.Send(query, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : Unauthorized();
    }
}