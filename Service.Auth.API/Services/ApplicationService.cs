using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Auth.Application.Services.AuthTokenGenerator;
using Service.Auth.Application.Startup.Seeds;
using Service.Auth.Core.Entities;
using Service.Auth.Infrastructure;
using System.Reflection;
using System.Text;

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

	public static IServiceCollection ConfigureDIContainer(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<DatabaseSettings>(config.GetSection(DatabaseSettings.Identifier));
		services.Configure<AuthenticationSettings>(config.GetSection(AuthenticationSettings.Identifier));

		services.AddTransient<ITokenGenerator, JwtTokenGenerator>();

		services.AddHostedService<InsertRoleSeeds>();

		return services;
	}

	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
	{
		var tokenSettings = new AuthenticationSettings();
		config.GetSection(AuthenticationSettings.Identifier).Bind(tokenSettings);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidAudience = tokenSettings.Audience,
				ValidIssuer = tokenSettings.Issuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SecretKey))
			};
		});

		return services;
	}

	public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services) 
	{
		services.AddSwaggerGen(c => 
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "JWTToken_Auth_API",
				Version = "v1"
			});
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
			});
			c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
						}
					},
					new string[] {}
				}
			});
		});

		return services;
	}
}
