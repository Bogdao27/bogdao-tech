using BogdaoTech.Domain.Modules;
using BogdaoTech.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BogdaoTech.Infrastructure
{
    //Класс для работы с БД
    public class ApplicationDbContext(
        IConfiguration configuration) : DbContext
    {
        //Ключик к какой БД подключаться. Берется из secrets.json
        private const string DATABASE = "Database";

        //некая обертка для таблицы в БД. Позволяет управлять agregate root
        //создавать репозиторий следует под каждый aggregate root
        public DbSet<Module> Modules => Set<Module>();

        //переопределяемый метод для конфигурирования БД, вызывается один раз при
        //создании БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //подключаемся к бд при помощи ConnectionString и провайдера
            //Npgsql
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            //когда создаются какие-то данные в БД, то делаются они с помощью SnakeCase
            optionsBuilder.UseSnakeCaseNamingConvention();

            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        //когда создаются модели мы хотим применить миграции, для них нужны
        //только что созданные конфигурации моделей
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        
        //фабрика логов, которые записываются в консоль
        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
