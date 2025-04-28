using Ecommecre_BE.Repositories.DataContext;
using Microsoft.EntityFrameworkCore;
namespace EcommerceWeb
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DbConnect"), b => b.MigrationsAssembly("EcommerceWeb"));
            });
        }
    }
}
