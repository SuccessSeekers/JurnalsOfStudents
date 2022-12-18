using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using StorageBroker;
using ILogger = LoggerService.ILogger;

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
            Console.Clear();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error on Migration: \n" + e.Message);
            Environment.Exit(1);
        }
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment environment)
    {
        applicationBuilder.UseExceptionHandler(appException =>
        {
            ILogger? logger = applicationBuilder.ApplicationServices
                .GetService<ILogger>();
            appException.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = new ContentType("Application/JSON").MediaType;
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                string errorString = exceptionHandlerFeature.Error.Message;
                if (environment.IsDevelopment())
                    errorString += ". PATH: " + exceptionHandlerFeature.Endpoint;
                logger?.LogInfo(errorString);
                await context.Response.WriteAsync(errorString);
            });
        });
    }
}