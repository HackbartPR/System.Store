using Service.Coupon.Application.DTOs;
using Service.Coupon.Application.Features.Get.Request;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;
using Service.Coupon.Infrastructure.Database;
using Service.Coupon.Infrastructure.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Service.Coupon.Application.Features.GetAll;

/// <summary>
/// Responsável por buscar os cupons filtrados
/// </summary>
public class GetCouponFeature : IRequestHandler<GetCouponRequest, BasePagedResponse<CouponDto>>
{
    private readonly DbCouponContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public GetCouponFeature(DbCouponContext dbContext)
        => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Recupera os cupons filtrados
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BasePagedResponse<CouponDto>> Handle(GetCouponRequest request, CancellationToken cancellationToken)
    {
        return await dbContext.Coupons
            .AsNoTracking()
            .Where(c => 
                (request.Id == null || c.Id == request.Id) &&
                (string.IsNullOrEmpty(request.CouponCode) || c.CouponCode.Equals(request.CouponCode)))
            .Select(c => new CouponDto
            {
                Id = c.Id,
                CouponCode = c.CouponCode,
                DiscountAmount = c.DiscountAmount,
                MinAmount = c.MinAmount
            })
            .Paginate<CouponDto>(request, cancellationToken);
    }
}
