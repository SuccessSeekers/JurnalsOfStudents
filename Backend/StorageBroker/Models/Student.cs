using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StorageBroker.Models;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<GradesLog> Grades { get; set; }
    public ICollection<AttendanceLog> Attendances { get; set; }

    // public ICollection<Group> StudentGroups { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
}