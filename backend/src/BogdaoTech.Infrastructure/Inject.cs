using BogdaoTech.Application.Modules;
using BogdaoTech.Infrastructure.Interceptors;
using BogdaoTech.Infrastructure.Options;
using BogdaoTech.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace BogdaoTech.Infrastructure
{
    //Для того, чтобы не писать миллиард строчек кода в Program.cs
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddSingleton<SoftDeleteInterceptor>();

            services.AddScoped<IModulesRepository, ModulesRepository>();

            services.AddMinio(configuration);

            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
            
            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                                   ?? throw new ApplicationException("Missing minio configuration");
                
                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials("minioadmin", "minioadmin");
                options.WithSSL(minioOptions.WithSSL);
            });

            return services;
        }
    }
}
