using StorageBroker.Models;

namespace StorageBroker.Dto;

public class UpdateGroupDto
{
    public string Name { get; set; }
    public GroupStatus Status { get; set; }
}