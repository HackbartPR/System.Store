using Domain.DTOs.Coupon;

namespace Domain.DTOs.ShoppingCart;

public class CartDetailDto
{
	public int Id { get; set; }

	public int CartHeaderId { get; set; }

	public int ProductId { get; set; }

	public int Count { get; set; }

	public ProductDto Product { get; set; } = null!;
}
