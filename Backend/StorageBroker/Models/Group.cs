namespace StorageBroker.Models;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
    public int CountOfStudents { get; set; }
    public IQueryable<Student> Students { get; set; }
    public IQueryable<Student> Teachers { get; set; }
}

public enum GroupStatus
{
    Finished,
    Continued
}

public class TeacherWithPositionForPerGroup
{
    public Teacher Teacher { get; set; }
    public TeacherPosition Position { get; set; }
}

public enum TeacherPosition
{
    Proffessor,
    Mentor,
    Assistent
}