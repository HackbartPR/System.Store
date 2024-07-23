using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace Domain.Responses.Base;

/// <summary>
/// Classe base para carregar as informações da requisição
/// </summary>
public class BaseResponse
{
    public string RequestId { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;

    [JsonIgnore, NotMapped]
    public HttpStatusCode? StatusCode { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;

    public ICollection<ModelError> Errors { get; set; } = new HashSet<ModelError>();
}

/// <summary>
/// Classe Base para carregar as informações da requisição e o objeto de retorno
/// </summary>
/// <typeparam name="TData"></typeparam>
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

/// <summary>
/// Classe carregará os erros de validação das requisições
/// </summary>
public class ModelError
{
    public ModelError() { }

    public ModelError(string message)
        => Message = message;

    /// <summary>
    /// Mensagem
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
