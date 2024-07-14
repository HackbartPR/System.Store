using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

public class BaseResponse
{
    public string RequestId { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;

    [JsonIgnore, NotMapped]
    public HttpStatusCode? StatusCode { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string Error {  get; set; } = string.Empty;
}

public sealed class BaseResponse<TData> : BaseResponse
{
    public TData? Data { get; set; }

    public BaseResponse() { }

    public BaseResponse(TData data, bool success = false, string message = "", HttpStatusCode? statusCode = HttpStatusCode.InternalServerError)
    {
        Data = data;
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
}
