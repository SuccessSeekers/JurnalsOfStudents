namespace StorageBroker.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public ICollection<Group> TeacherGroups { get; set; }
}