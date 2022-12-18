namespace StorageBroker.Models;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
    public int CountOfStudents { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}

public enum GroupStatus
{
    Finished,
    Continued
}

public enum TeacherPosition
{
    Proffessor,
    Mentor,
    Assistent
}