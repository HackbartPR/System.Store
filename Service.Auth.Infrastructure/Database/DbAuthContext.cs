using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Auth.Core.Entities;

namespace Service.Auth.Infrastructure;

/// <summary>
/// Contexto do banco de dados utilizado no serviço de autenticação
/// </summary>
public class DbAuthContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="options"></param>
    public DbAuthContext(DbContextOptions<DbAuthContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    /// <summary>
    /// Ações para fazer quando o banco for criado
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
