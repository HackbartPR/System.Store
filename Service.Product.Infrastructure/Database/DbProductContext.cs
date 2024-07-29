using Microsoft.EntityFrameworkCore;
using Service.Product.Core.Entities;
using Service.Product.Core.Seeds;

namespace Service.Product.Infrastructure.Database;

/// <summary>
/// Contexto do banco de dados utilizado no serviço de Cupom
/// </summary>
public class DbProductContext : DbContext
{
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="options"></param>
    public DbProductContext(DbContextOptions<DbProductContext> options) : base(options) { }
    
    public DbSet<ProductEntity> Products { get; set; }

    /// <summary>
    /// Ações para fazer quando o banco for criado
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>().HasData(ProductSeeds.Seeds());
    }
}
