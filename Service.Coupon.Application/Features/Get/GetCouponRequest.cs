using MediatR;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Infrastructure.CrossCutting.BaseRequests;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.Application.Features.Get;

/// <summary>
/// Representação da requisição para listar os cupons
/// </summary>
public class GetCouponRequest : BasePagedRequest, IRequest<BasePagedResponse<CouponDto>>
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Código do cupon
    /// </summary>
    public string? CouponCode { get; set; }
}
