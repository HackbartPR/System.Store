using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Entities;
using Service.Coupon.Infrastructure.Database;
using System.Net;

namespace Service.Coupon.Application.Features.Delete;

public class DeleteCouponFeature : IRequestHandler<DeleteCouponCommand, BaseResponse<bool>>
{
    private readonly DbCouponContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    public DeleteCouponFeature(DbCouponContext dbContext)
        => this.dbContext = dbContext;

    /// <summary>
    /// Deleta um cupom promocional
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BaseResponse<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        CouponEntity? coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (coupon == null)
            return new BaseResponse<bool>(false, false, "Cupom não encontrado.", HttpStatusCode.NotFound);

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<bool>(true, true, "Removido com sucesso", HttpStatusCode.OK);
    }
}
