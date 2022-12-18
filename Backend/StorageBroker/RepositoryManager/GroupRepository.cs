using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class GroupRepository : RepositoryBase<Group>
{
    public GroupRepository(DataBaseContext context) : base(context)
    {
    }
}