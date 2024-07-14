using Service.Coupon.Core.Entities._Shared;

namespace Service.Coupon.Core.Entities;

/// <summary>
/// Representação de um cupom promocional
/// </summary>
public class CouponEntity : BaseEntity
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
    public int MinAmount { get; set; }
}
