namespace StorageBroker.Models;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
    public int CountOfStudents { get; set; }
}

public enum GroupStatus
{
    Finished,
    Continued
}