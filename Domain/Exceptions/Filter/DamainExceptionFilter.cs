using System.Net;
using Domain.Core;
using Domain.Core.Constants;
using Domain.Core.Enum;
using Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Domain.Exceptions.Filter;
public class DamainExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Response response = new Response(ResponseCode.Error);

        response.Message = ErrorMessage.GenericExceptionMessage;

        if(context.Exception is DomainException)
        {
            response.Message = context.Exception.Message;
        }

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new JsonResult(response);
        context.ExceptionHandled = true;
        
        base.OnException(context);
    }
}