using StorageBroker.Models;

namespace StorageBroker.Dto;

public class CreateGroupDto
{
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
    public int CountOfStudents { get; set; }
}