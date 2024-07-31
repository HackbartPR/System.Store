using Domain.DTOs.ShoppingCart;
using Domain.Responses;
using MediatR;

namespace Service.ShoppingCart.Application.Features.AddToCart;

public class AddToCartCommand : IRequest<BaseResponse<CartDto?>>
{
	public string UserId { get; set; } = string.Empty;
	public int ProductId { get; set; }
}
