using Microsoft.EntityFrameworkCore;
using StorageBroker.Models;

namespace StorageBroker;

public class DataBaseContext : DbContext
{
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GradesLog> GradeLogs { get; set; }
    public DbSet<AttendanceLog> AttendanceLogs { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Group>()
            .HasData(
                new Group()
                {
                    GroupId = 1,
                    Name = "B23",
                    Status = GroupStatus.Continued,
                    CountOfStudents = 17
                }
            );
        modelBuilder.Entity<Student>()
            .HasData(
                new Student()
                {
                    StudentId = 1,
                    Name = "Elchin",
                    Surname = "Uralov",
                },
                new Student()
                {
                    StudentId = 2,
                    Name = "Diyor",
                    Surname = "Abdumannonpov",
                }
            );
        modelBuilder.Entity<StudentGroup>()
            .HasData(
                new StudentGroup()
                {
                    Id = 1,
                    StudentId = 1,
                    GroupId = 1
                },
                new StudentGroup()
                {
                    Id = 2,
                    StudentId = 2,
                    GroupId = 1
                }
            );
    }
}