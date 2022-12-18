using StorageBroker.Models;

namespace StorageBroker.Dto;

public class GroupDto
{
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
    public int CountOfStudents { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}