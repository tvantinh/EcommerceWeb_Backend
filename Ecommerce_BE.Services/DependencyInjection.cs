using Azure.Core;
using Ecommecre_BE.Repositories.UnitOfWork;
using Ecommerce_BE.Contract.Repositories.IUnitOfWork;
using Ecommerce_BE.Contract.Services.IService;
using Ecommerce_BE.Services.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce_BE.Services
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRespositories();
            services.AddServices();
        }
        public static void AddRespositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}