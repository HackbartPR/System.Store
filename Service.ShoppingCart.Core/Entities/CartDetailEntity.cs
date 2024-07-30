using Domain.DTOs.Coupon;
using Domain.Entities;

namespace Service.ShoppingCart.Core.Entities;

public class CartDetailEntity : BaseEntity
{
	public int CartHeaderId { get; set; }

	public int ProductId { get; set; }

	public int Count { get; set; }

	public virtual CartEntity CartHeader { get; set; } = null!;

	public virtual ProductDto Product { get; set; } = null!;
}
