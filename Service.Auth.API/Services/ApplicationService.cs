using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Auth.Core.Entities;
using Service.Auth.Infrastructure;
using System.Reflection;

namespace Service.Auth.API.Services;

public static class ApplicationService
{
	public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
	{
		DatabaseSettings settings = new();
		config.GetSection(DatabaseSettings.Identifier).Bind(settings);

		services.AddDbContext<DbAuthContext>(option =>
		{
			option.UseSqlServer(settings.ConnectionString);
		});

		return services;
	}

	public static void ApplyMigrations(IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<DbAuthContext>();

		if (dbContext.Database.GetPendingMigrations().Count() > 0)
			dbContext.Database.Migrate();
	}

	public static IServiceCollection AddMediatrService(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([
			Assembly.GetExecutingAssembly(),
			AppDomain.CurrentDomain.Load("Service.Auth.Application")]));

		return services;
	}

	public static IServiceCollection AddIdentity(this IServiceCollection services)
	{
		services
			.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<DbAuthContext>()
			.AddDefaultTokenProviders();

		return services;
	}

	public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<DatabaseSettings>(config.GetSection(DatabaseSettings.Identifier));
		services.Configure<AuthenticationSettings>(config.GetSection(AuthenticationSettings.Identifier));

		return services;
	}
}
