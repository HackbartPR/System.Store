namespace Service.Coupon.Application.DTOs
{
    public class CouponDto
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
}
