using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Queries.GetBookById;
using Microsoft.AspNetCore.Http;
using Application.Books.Commands.CreateBook;
using Mapster;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Commands.UpdateBook;
using Application.Books.Commands.DeleteBook;
using Domain.Entities;
using Application.Books.Queries.GetBookByTitle;
using Domain.Core.Enum;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Presentation.Controllers;

[Route("api/[Controller]")]
public sealed class BookController : ApiController
{
    public BookController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("title:string")]
    [Authorize]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(string title, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(title.Trim()))
            return BadRequest(ErrorMessage.BadRequest);

        var query = new GetBookByTitleQuery(title);

        var response = await Sender.Send(query, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status404NotFound, response.Message);
    }


    [HttpGet("id:guid")]
    [Authorize]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(Guid id, CancellationToken cancellationToken)
    {
        if (id == null || id == Guid.Empty)
            return BadRequest(ErrorMessage.BadRequest);

        var query = new GetBookByIdQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status404NotFound, response.Message);
    }

    [HttpGet("all")]
    [Authorize]
    [ProducesResponseType(typeof(IQueryable<Book>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllBooksQuery();

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Manager")]
    [ProducesResponseType(typeof(CreateBookResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(ErrorMessage.BadRequest);

        var query = new GetBookByTitleQuery(request.Title);
        var titleExist = await Sender.Send(query, cancellationToken);

        if (titleExist.StatusCode == ResponseCode.Successful)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.CreateBookTitleExistError);
        }

        var Command = request.Adapt<CreateBookCommand>();
        var response = await Sender.Send(Command, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status500InternalServerError, response.Message);
    }

    [HttpPut]
    [Authorize(Roles = "Administrator,Manager")]
    [ProducesResponseType(typeof(UpdateBookRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(ErrorMessage.BadRequest);

        var Command = request.Adapt<UpdateBookCommand>();

        var response = await Sender.Send(Command, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status500InternalServerError, response.Message);
    }

    [HttpDelete("id:guid")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        if (id == null || id == Guid.Empty)
            return BadRequest(ErrorMessage.BadRequest);

        var Command = new DeleteBookCommand(id);

        var response = await Sender.Send(Command, cancellationToken);

        return response.StatusCode == ResponseCode.Successful ? Json(response.Data) : StatusCode(StatusCodes.Status500InternalServerError, response.Message);
    }
}