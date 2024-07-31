using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.ShoppingCart.Core.Entities;

public class CartEntity : BaseEntity
{
	public string UserId { get; set; } = string.Empty;

	public string? CouponCode { get; set; }

	public bool Open { get; set; }

	[NotMapped]
	public double Discount { get; set; }

	[NotMapped]
	public double CartTotal { get; set; }

	public virtual ICollection<CartDetailEntity> Details { get; set; } = new HashSet<CartDetailEntity>();
}
