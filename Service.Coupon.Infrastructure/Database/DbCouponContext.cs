using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Entities;

namespace Service.Coupon.Infrastructure.Database
{
    /// <summary>
    /// Contexto do banco de dados utilizado no serviço de Cupom
    /// </summary>
    public class DbCouponContext : DbContext
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="options"></param>
        public DbCouponContext(DbContextOptions<DbCouponContext> options) : base(options) { }

        public DbSet<CouponEntity> Coupons { get; set; }
    }
}
