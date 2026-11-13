using BogdaoTech.Application.Modules;
using BogdaoTech.Infrastructure.Interceptors;
using BogdaoTech.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Infrastructure
{
    //Для того, чтобы не писать миллиард строчек кода в Program.cs
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddSingleton<SoftDeleteInterceptor>();

            services.AddScoped<IModulesRepository, ModulesRepository>();

            return services;
        }
    }
}
