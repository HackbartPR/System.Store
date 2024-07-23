namespace Domain.Requests.CouponService;

/// <summary>
/// Representação da requisição a ser feita para atualizar um cupom
/// </summary>
public class PutCouponRequest
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
