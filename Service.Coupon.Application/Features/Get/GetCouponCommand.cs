using Domain.DTOs;
using Domain.Requests.Base;
using Domain.Responses.Base;
using MediatR;

namespace Service.Coupon.Application.Features.Get;

/// <summary>
/// Representação da requisição para listar os cupons
/// </summary>
public class GetCouponCommand : BasePagedRequest, IRequest<BasePagedResponse<CouponDto>>
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
