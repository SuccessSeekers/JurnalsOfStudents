using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using StorageBroker;

namespace WebAPI.ServiceExtensions;

public static class AppExtensions
{
    public static void MigrateDataBase(this IApplicationBuilder builder)
    {
        try
        {
            Console.WriteLine("Please wait for migrating for database....");
            using var scope = builder.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<DataBaseContext>();
            if (dbContext == null)
                throw new Exception("DataBase context has been registered");
            // Console.WriteLine(dbContext.Database.);
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.GetInfrastructure().GetService<IMigrator>().Migrate();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error on Migration: \n" + e.Message);
            Environment.Exit(1);
        }
    }
}