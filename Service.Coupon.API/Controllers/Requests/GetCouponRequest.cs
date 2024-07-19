using Service.Coupon.Infrastructure.CrossCutting.BaseRequests;

namespace Service.Coupon.API.Controllers.Requests;

/// <summary>
/// Representação da requisição para listar os cupons
/// </summary>
public class GetCouponRequest : BasePagedRequest
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
