using StorageBroker.Models;

namespace StorageBroker.Dto;

public class StudentDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<GradesLog> Grades { get; set; }
    public ICollection<AttendanceLog> Attendances { get; set; }
    public ICollection<Group> StudentGroups { get; set; }
}
