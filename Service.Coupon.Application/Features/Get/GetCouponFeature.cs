using Service.Coupon.Infrastructure.Database;
using Service.Coupon.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Service.Coupon.Application.Features.Get;
using System.Net;
using Domain.Responses;
using Domain.DTOs.Coupon;

namespace Service.Coupon.Application.Features.GetAll;

/// <summary>
/// Responsável por buscar os cupons filtrados
/// </summary>
public class GetCouponFeature : IRequestHandler<GetCouponCommand, BasePagedResponse<CouponDto>>
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
    public async Task<BasePagedResponse<CouponDto>> Handle(GetCouponCommand request, CancellationToken cancellationToken)
    {
        BasePagedResponse<CouponDto> response = await dbContext.Coupons
            .AsNoTracking()
            .Where(c => 
                (request.Id == null || c.Id == request.Id) &&
                (string.IsNullOrEmpty(request.CouponCode) || c.CouponCode.Contains(request.CouponCode)))
            .Select(c => new CouponDto
            {
                Id = c.Id,
                CouponCode = c.CouponCode,
                DiscountAmount = c.DiscountAmount,
                MinAmount = c.MinAmount
            })
            .Paginate<CouponDto>(request, cancellationToken);

        response.StatusCode = response.Data?.Count() == 0 ? HttpStatusCode.NotFound : response.StatusCode;
        response.Success = true;
        response.Message = "Consulta realizada com sucesso.";

        return response;
    }
}
