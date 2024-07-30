namespace Domain.DTOs.ShoppingCart;

public class CartDto
{
	public int Id { get; set; }

	public string? UserId { get; set; }

	public string? CouponCode { get; set; }

	public double Discount { get; set; }

	public double CartTotal { get; set; }

	public ICollection<CartDetailDto> Details { get; set; } = new HashSet<CartDetailDto>();
}
