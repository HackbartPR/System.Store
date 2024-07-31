using System.ComponentModel.DataAnnotations;

namespace Service.ShoppingCart.API.Requests;

/// <summary>
/// Requisição para adicionar um produto ao carrinho de compras
/// </summary>
public class AddToCartRequest
{
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Id do Usuário' não pode ser nulo ou vazio.")]
	public string UserId { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Id do Produto' não pode ser nulo ou vazio.")]
	public int ProductId { get; set; }
}
