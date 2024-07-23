namespace Domain.Requests.CouponService;

/// <summary>
/// Representação da requisição a ser feita para cadastrar um cupom
/// </summary>
public class PostCouponRequest
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
