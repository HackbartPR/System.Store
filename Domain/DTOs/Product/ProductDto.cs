namespace Domain.DTOs.Coupon;

public class ProductDto
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }

	/// <summary>
	/// Nome
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Preço
	/// </summary>
	public double Price { get; set; }

	/// <summary>
	/// Descrição
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Nome da Categoria a qual o produto pertence
	/// Esta propriedade deve ser uma entidade separada, mas para fins de estudo, será colocado dessa maneira.
	/// </summary>
	public string CategoryName { get; set; } = string.Empty;

	/// <summary>
	/// URL da imagem
	/// </summary>
	public string ImageUrl { get; set; } = string.Empty;
}
