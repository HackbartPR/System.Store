using Microsoft.EntityFrameworkCore;
using Service.ShoppingCart.Core.Entities;

namespace Service.ShoppingCart.Infrastructure.Database;

/// <summary>
/// Contexto do banco de dados
/// </summary>
public class DbCartContext : DbContext
{
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="options"></param>
    public DbCartContext(DbContextOptions<DbCartContext> options) : base(options) { }
    
    public DbSet<CartEntity> Carts { get; set; }
	public DbSet<CartDetailEntity> CartDetails { get; set; }

	/// <summary>
	/// Ações para fazer quando o banco for criado
	/// </summary>
	/// <param name="modelBuilder"></param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartDetailEntity>()
            .HasOne(c => c.CartHeader)
            .WithMany(c => c.Details)
            .HasForeignKey(c => c.CartHeaderId);

        base.OnModelCreating(modelBuilder);
    }
}
