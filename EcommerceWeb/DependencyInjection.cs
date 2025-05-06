using Ecommecre_BE.Repositories.DataContext;
using Ecommerce_BE.Contract.Services.IService;
using Microsoft.EntityFrameworkCore;
using Ecommerce_BE.Services.Service;
using Ecommerce_BE.Services;
namespace EcommerceWeb
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigRoute();
            services.AddDatabase(configuration);
            services.AddInfrastructure(configuration);
            services.AddServices();
        }
        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DbConnect"), b => b.MigrationsAssembly("EcommerceWeb"));
            });
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
