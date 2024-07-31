using Domain.DTOs.ShoppingCart;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.ShoppingCart.API.CrossCutting;
using Service.ShoppingCart.API.Requests;
using Service.ShoppingCart.Application.Features.AddToCart;

namespace Service.ShoppingCart.API.Controllers;

[Route("api/v1/shopping-cart")]
public class CartController : BaseController
{
	private readonly IMediator mediator;

	public CartController(ILogger<CartController> logger, IMediator mediator) : base(logger)
		=> this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	/// <summary>
	/// Responsável por adicionar um item ao carrinho de compras
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddToCartRequest request, CancellationToken cancellationToken)
	{
		BaseResponse<CartDto?> response = new();

		try
		{
			AddToCartCommand command = new()
			{
				UserId = request.UserId,
				ProductId = request.ProductId,
			};

			response = await mediator.Send(command, cancellationToken);
		}
		catch (Exception ex)
		{
			response.BuildError(logger, ex);
		}

		return Result(response);
	}
}
