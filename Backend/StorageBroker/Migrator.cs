using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace StorageBroker.MigratorService;

public class Migrator : IMigrator
{
    private readonly DataBaseContext _context;

    public Migrator(DataBaseContext context)
    {
        _context = context;
    }

    public void Migrate()
    {
        _context.Database.GetInfrastructure().GetService<IMigrator>().Migrate();
    }
}