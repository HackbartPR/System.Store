using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Auth.Core.Seeds;

namespace Service.Auth.Application.Startup.Seeds;

public class InsertRoleSeeds : BackgroundService
{
	private readonly ILogger<InsertRoleSeeds> logger;
	private readonly IServiceProvider serviceProvider;

	/// <summary>
	/// Construtor
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="serviceProvider"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public InsertRoleSeeds(ILogger<InsertRoleSeeds> logger,  IServiceProvider serviceProvider)
	{
		this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="stoppingToken"></param>
	/// <returns></returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		try
		{
			logger.LogInformation("Verificando seeds da tabela de Roles");

			using var scope = serviceProvider.CreateScope();
			RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			if (await roleManager.FindByNameAsync(ApplicationRoles.Admin) != null)
				return;

			foreach (string role in RoleSeeds.Seeds())
				await roleManager.CreateAsync(new IdentityRole(role));

			logger.LogInformation("Dados inseridos com sucesso na tabela de Roles ");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Erro ao inserir dados na tabela de Roles");
		}
	}
}
