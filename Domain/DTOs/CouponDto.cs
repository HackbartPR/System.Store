namespace Domain.DTOs;

public class CouponDto
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }

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
