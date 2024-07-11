using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Settings;
using Service.Coupon.Infrastructure.Database;

namespace Service.Coupon.API.Services
{
    public static partial class ApplicationService
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            DatabaseSettings settings = new();
            config.GetSection(DatabaseSettings.Identifier).Bind(settings);

            services.AddDbContext<DbCouponContext>(option =>
            {
                option.UseSqlServer(settings.ConnectionString);
            });

            return services;
        }
    }
}
