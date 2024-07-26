namespace Domain.Responses;

/// <summary>
/// Response para consultas que envolvem listagem
/// </summary>
/// <typeparam name="TData"></typeparam>
public class BasePagedResponse<TData> : BaseResponse
{
    /// <summary>
    /// Resposta
    /// </summary>
    public IEnumerable<TData>? Data { get; set; }

    /// <summary>
    /// Total de respostas por página
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Total de respostas
    /// </summary>
    public int TotalRegisters { get; set; }
}
