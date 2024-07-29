using Domain.Requests;

namespace Service.Product.API.Requests;

/// <summary>
/// Representação da requisição para listar os produtos
/// </summary>
public class GetProductRequest : BasePagedRequest
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int? Id { get; set; }

	/// <summary>
	/// Nome
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Nome da categoria
	/// </summary>
	public string? CategoryName { get; set; }
}
