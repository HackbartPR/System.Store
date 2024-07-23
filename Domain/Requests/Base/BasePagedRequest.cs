namespace Domain.Requests.Base;

/// <summary>
/// Classe base para conter as propriedades referentes a paginação
/// </summary>
public abstract class BasePagedRequest
{
    /// <summary>
    /// Página atual
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamanho total da página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Propriedade que será utilizada para ordenação
    /// </summary>
    public virtual string OrderByProperty { get; set; } = "Id";
}
