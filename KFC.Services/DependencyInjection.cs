using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KFC.Contract.Repositories.Interface;
using KFC.Core.Utils;
using KFC.Entity;
using KFC.Repositories.Repositories;
using System.Reflection;

namespace KFC.Services
{
    public static class DependencyInjection
    {

        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepository();
            services.AddAutoMapper();
            services.AddServices(configuration);
            services.AddFixedSaltPasswordHasher();
        }
        public static void AddRepository(this IServiceCollection services)
        {
            services
               .AddScoped<IUnitOfWork, UnitOfWork>();
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services
            //    .AddScoped<IAuthService, AuthService>();
            
        }
        public static void AddFixedSaltPasswordHasher(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<ApplicationUser>, FixedSaltPasswordHasher<ApplicationUser>>();
        }

    }
}
