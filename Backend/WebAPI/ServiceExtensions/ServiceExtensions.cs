using LoggerService;
using Microsoft.EntityFrameworkCore;
using StorageBroker;
using StorageBroker.RepositoryManager;
using ILogger = LoggerService.ILogger;

namespace WebAPI.ServiceExtensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
        serviceCollection.AddDbContext<DataBaseContext>(optionsAction =>
        {
            optionsAction.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static void ConfigureRepositoryManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureLoggingService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var logFilePathSection = configuration.GetSection("Logging").GetSection("LogFilePath");

        if (logFilePathSection.Exists())
            serviceCollection.AddSingleton<ILogger>(provider => new Logger(logFilePathSection.Value));
        else
            serviceCollection.AddSingleton<ILogger>(provider => new Logger());
    }
}