using Domain.Enums;

namespace Domain.Requests;

/// <summary>
/// Representa uma requisição a um serviço
/// </summary>
public class BaseRequest<TData>
{
    public ERequestMethod Method { get; set; }

    public string Url { get; set; } = string.Empty;

    public TData? Data { get; set; }

    public string AccessToken { get; set; } = string.Empty;
}
