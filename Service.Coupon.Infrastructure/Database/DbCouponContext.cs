using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Entities;
using Service.Coupon.Core.Seeds;

namespace Service.Coupon.Infrastructure.Database;

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

    /// <summary>
    /// Ações para fazer quando o banco for criado
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CouponEntity>().HasData(CouponSeeds.Seeds());

    }
}
