using Domain.DTOs;
using Domain.Responses.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Entities;
using Service.Coupon.Infrastructure.Database;
using System.Net;

namespace Service.Coupon.Application.Features.Put;

public class PutCouponFeature : IRequestHandler<PutCouponCommand, BaseResponse<CouponDto?>>
{
    private readonly DbCouponContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PutCouponFeature(DbCouponContext dbContext)
        => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Atualiza um cupom promocional
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BaseResponse<CouponDto?>> Handle(PutCouponCommand request, CancellationToken cancellationToken)
    {
        CouponEntity? coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (coupon == null)
            return new BaseResponse<CouponDto?>(null, false, "Cupom não encontrado.", HttpStatusCode.NotFound);

        if (await dbContext.Coupons.AnyAsync(c => c.CouponCode.Equals(request.CouponCode) && c.Id !=  request.Id, cancellationToken))
            return new BaseResponse<CouponDto?>(null, false, "Código informado já está em uso", HttpStatusCode.Conflict);

        coupon.MinAmount = (int) request.MinAmount!;
        coupon.CouponCode = request.CouponCode;
        coupon.DiscountAmount = request.DiscountAmount;

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<CouponDto?>(new CouponDto()
        {
            Id = coupon.Id,
            MinAmount = coupon.MinAmount,
            CouponCode = coupon.CouponCode,
            DiscountAmount = coupon.DiscountAmount,
        }, true, "Atualizado com sucesso", HttpStatusCode.OK);
    }
}
