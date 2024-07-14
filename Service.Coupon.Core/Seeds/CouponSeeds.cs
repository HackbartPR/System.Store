using Service.Coupon.Core.Entities;

namespace Service.Coupon.Core.Seeds;

/// <summary>
/// Responsável por manter as seeds dos cupons
/// </summary>
public static class CouponSeeds
{
    /// <summary>
    /// Lista de Seeds
    /// </summary>
    /// <returns></returns>
    public static ICollection<CouponEntity> Seeds()
    {
        return
        [
            new CouponEntity()
            {
                Id = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20,
            },
            new CouponEntity()
            {
                Id = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40,
            },
        ];
    }
}

