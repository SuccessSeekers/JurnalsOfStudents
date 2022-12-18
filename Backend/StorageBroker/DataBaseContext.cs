using Microsoft.EntityFrameworkCore;
using StorageBroker.Models;

namespace StorageBroker;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GradesLog> GradeLogs { get; set; }
    public DbSet<AttendanceLog> AttendanceLogs { get; set; }
}