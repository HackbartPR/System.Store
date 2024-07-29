using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Product.Core.Entities;
using Service.Product.Infrastructure.Database;
using System.Net;

namespace Service.Product.Application.Features.Delete;

public class DeleteProductFeature : IRequestHandler<DeleteProductCommand, BaseResponse<bool>>
{
    private readonly DbProductContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    public DeleteProductFeature(DbProductContext dbContext)
        => this.dbContext = dbContext;

    /// <summary>
    /// Deleta um cupom promocional
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BaseResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity? product = await dbContext.Products.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (product == null)
            return new BaseResponse<bool>(false, false, "Produto não encontrado.", HttpStatusCode.NotFound);

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<bool>(true, true, "Removido com sucesso", HttpStatusCode.OK);
    }
}
