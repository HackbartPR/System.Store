using Domain.DTOs.Coupon;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Product.Application.Features.Post;
using Service.Product.Core.Entities;
using Service.Product.Infrastructure.Database;
using System.Net;

namespace Service.Coupon.Application.Features.Post;

public class PostProductFeature : IRequestHandler<PostProductCommand, BaseResponse<ProductDto?>>
{
    private readonly DbProductContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    public PostProductFeature(DbProductContext dbContext)
        => this.dbContext = dbContext;

    /// <summary>
    /// Cadastra um novo produto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<ProductDto?>> Handle(PostProductCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Products.AnyAsync(c => c.Name.Equals(request.Name), cancellationToken))
            return new BaseResponse<ProductDto?>(null, false, "Nome informado já está em uso", HttpStatusCode.Conflict);

        ProductEntity product = new()
        {
            Name = request.Name,
            Price = request.Price,
            ImageUrl = request.ImageUrl, // Será alterado futuramente
            Description = request.Description,
            CategoryName = request.CategoryName,
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<ProductDto?>(new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
            CategoryName = product.CategoryName,
        }, true, "Produto cadastrado com sucesso", HttpStatusCode.Created);
    }
}
