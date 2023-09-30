using System.Text;
using Domain.Core.Enum;

namespace Domain.Core;
public class Response
{
    public ResponseCode StatusCode { get; set; }
    public string? Message { get; set; }

    public Response()
    {
    }

    public Response(ResponseCode statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message;
    }
}