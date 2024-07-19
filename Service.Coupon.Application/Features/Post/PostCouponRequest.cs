using MediatR;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;
using System.ComponentModel.DataAnnotations;

namespace Service.Coupon.Application.Features.Post;

public class PostCouponRequest : IRequest<BaseResponse<CouponDto?>>
{
    /// <summary>
    /// Código promocional
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Código' não pode ser nulo ou vazio")]
    public string CouponCode { get; set; } = string.Empty;

    /// <summary>
    /// Desconto aplicado
    /// </summary>
    [Required(ErrorMessage = "O campo 'Desconto' não pode ser nulo")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo 'Desconto' deve possuir o valor maior que zero")]
    public double DiscountAmount { get; set; }

    /// <summary>
    /// Caso exista algum valor mínimo atrelado
    /// </summary>
    public int? MinAmount { get; set; } = 0;
}
