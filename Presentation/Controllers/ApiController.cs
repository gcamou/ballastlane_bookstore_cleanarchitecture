using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
public abstract class ApiController : Controller
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}