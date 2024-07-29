using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Net;
using Domain.Responses;
using Domain.DTOs.Coupon;
using Service.Product.Application.Features.Get;
using Service.Product.Infrastructure.Database;
using Service.Product.Infrastructure.Extensions;

namespace Service.Product.Application.Features.GetAll;

/// <summary>
/// Responsável por buscar os produtos filtrados
/// </summary>
public class GetProductFeature : IRequestHandler<GetProductCommand, BasePagedResponse<ProductDto>>
{
    private readonly DbProductContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public GetProductFeature(DbProductContext dbContext)
        => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Recupera os cupons filtrados
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BasePagedResponse<ProductDto>> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        BasePagedResponse<ProductDto> response = await dbContext.Products
            .AsNoTracking()
            .Where(c => 
                (request.Id == null || c.Id == request.Id) &&
                (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name)) &&
				(string.IsNullOrEmpty(request.CategoryName) || c.CategoryName.Contains(request.CategoryName)))
            .Select(c => new ProductDto
			{
                Id = c.Id,
                CategoryName = c.CategoryName,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Price = c.Price
            })
            .Paginate<ProductDto>(request, cancellationToken);

        response.StatusCode = response.Data?.Count() == 0 ? HttpStatusCode.NotFound : response.StatusCode;
        response.Success = true;
        response.Message = "Consulta realizada com sucesso.";

        return response;
    }
}
