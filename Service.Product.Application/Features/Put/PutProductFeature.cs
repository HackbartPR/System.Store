using Domain.DTOs.Coupon;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Product.Application.Features.Put;
using Service.Product.Core.Entities;
using Service.Product.Infrastructure.Database;
using System.Net;

namespace Service.Coupon.Application.Features.Put;

public class PutProductFeature : IRequestHandler<PutProductCommand, BaseResponse<ProductDto?>>
{
    private readonly DbProductContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PutProductFeature(DbProductContext dbContext)
        => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Atualiza um produto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BaseResponse<ProductDto?>> Handle(PutProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity? product = await dbContext.Products.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (product == null)
            return new BaseResponse<ProductDto?>(null, false, "Produto não encontrado.", HttpStatusCode.NotFound);

        if (await dbContext.Products.AnyAsync(c => c.Name.Equals(request.Name) && c.Id !=  request.Id, cancellationToken))
            return new BaseResponse<ProductDto?>(null, false, "Nome informado já está em uso", HttpStatusCode.Conflict);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryName = request.CategoryName;
        product.ImageUrl = request.ImageUrl;

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<ProductDto?>(new ProductDto()
        {
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			ImageUrl = product.ImageUrl,
			Description = product.Description,
			CategoryName = product.CategoryName,
		}, true, "Atualizado com sucesso", HttpStatusCode.OK);
    }
}
