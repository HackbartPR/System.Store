using MediatR;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.Application.Features.Post;

public class PostCouponCommand : IRequest<BaseResponse<CouponDto?>>
{
    /// <summary>
    /// Código promocional
    /// </summary>
    public string CouponCode { get; set; } = string.Empty;

    /// <summary>
    /// Desconto aplicado
    /// </summary>
    public double DiscountAmount { get; set; }

    /// <summary>
    /// Caso exista algum valor mínimo atrelado
    /// </summary>
    public int? MinAmount { get; set; } = 0;
}
