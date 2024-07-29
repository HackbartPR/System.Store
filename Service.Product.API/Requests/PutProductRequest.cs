using System.ComponentModel.DataAnnotations;

namespace Service.Product.API.Requests;

/// <summary>
/// Representação da requisição a ser feita para atualizar um produto
/// </summary>
public class PutProductRequest
{
	/// <summary>
	/// Nome
	/// </summary>
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome' não pode ser nulo ou vazio")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Preço
	/// </summary>
	[Required(ErrorMessage = "O campo 'Preço' não pode ser nulo ou vazio")]
	public double Price { get; set; }

	/// <summary>
	/// Descrição
	/// </summary>
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Descrição' não pode ser nulo ou vazio")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Nome da Categoria a qual o produto pertence
	/// Esta propriedade deve ser uma entidade separada, mas para fins de estudo, será colocado dessa maneira.
	/// </summary>
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome da Categoria' não pode ser nulo ou vazio")]
	public string CategoryName { get; set; } = string.Empty;

	/// <summary>
	/// URL da imagem
	/// A URL mudará a forma com o qual será enviada
	/// </summary>
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'URL da imagem' não pode ser nulo ou vazio")]
	public string ImageUrl { get; set; } = string.Empty;
}
