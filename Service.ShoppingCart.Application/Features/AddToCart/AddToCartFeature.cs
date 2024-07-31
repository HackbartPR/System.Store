using Domain.DTOs.ShoppingCart;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.ShoppingCart.Core.Entities;
using Service.ShoppingCart.Infrastructure.Database;
using System.Net;

namespace Service.ShoppingCart.Application.Features.AddToCart;

public class AddToCartFeature : IRequestHandler<AddToCartCommand, BaseResponse<CartDto?>>
{
	private readonly DbCartContext context;

	public AddToCartFeature(DbCartContext context)
	{
		this.context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<BaseResponse<CartDto?>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
	{
		//TODO: Verificar veracidade do usuário informado

		CartEntity? cart = await context.Carts.Include(c => c.Details).FirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) && c.Open, cancellationToken);
		if (cart == null)
		{
			cart = new()
			{
				UserId = request.UserId,
				CreatedAt = DateTime.UtcNow,
				Open = true,
			};

			await context.Carts.AddAsync(cart, cancellationToken);
		}

		if (!cart.Details.Any(d => d.ProductId == request.ProductId))
		{
			cart.Details.Add(new CartDetailEntity()
			{
				ProductId = request.ProductId,
				Count = 1,
				CreatedAt = DateTime.UtcNow
			});
		}
		else
		{
			CartDetailEntity detail = cart.Details.First(d => d.ProductId == request.ProductId);
			detail.Count++;
		}	
		
		await context.SaveChangesAsync(cancellationToken);

		return new BaseResponse<CartDto?>(new CartDto
		{
			Id = cart.Id,
			UserId = cart.UserId,
			CouponCode = null,
			CartTotal = 0, //TODO: Verificar sobre
			Details = cart.Details.Select(d => new CartDetailDto
			{
				Id = d.Id,
				CartHeaderId = d.CartHeaderId,
				Count = d.Count,
				Product = d.Product,
			}).ToList()
		}, true, "Produto adicionado com sucesso", HttpStatusCode.OK);
	}
}
