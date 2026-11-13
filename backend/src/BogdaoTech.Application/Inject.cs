using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Modules.Delete;
using BogdaoTech.Application.Modules.UpdateMainInfo;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BogdaoTech.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateModuleHandler>();
            
            services.AddScoped<UpdateMainInfoHandler>();
            
            services.AddScoped<DeleteModuleHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
