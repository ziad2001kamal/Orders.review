using Microsoft.Extensions.DependencyInjection;
using System;
using System.Buffers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Infrastructure.Services.Users;
using Orders.Infrastructure.Services.Categories;
using Orders.Infrastructure.Services.Auth;
using Orders.Infrastructure.Services.Resturents;
namespace Orders.Infrastructure.Extinction
{
    public static class ServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IResturentService, ResturentService>();

            
            return services;
        }
    }
}