using Domain.Core.Enum;

namespace Domain.Core;

public class StandardResponse<T> : Response
{
    public T Data { get; set; }
    
    public StandardResponse()
        : base()
    {
        StatusCode = ResponseCode.Successful;
    }

    public StandardResponse(
        ResponseCode statusCode,
        T? data = default,
         string? message = null)
        : base(statusCode, message)
    {
        Data = data;
    }
}