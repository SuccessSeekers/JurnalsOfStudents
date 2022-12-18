using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class StudentGroupRepository : RepositoryBase<StudentGroup>
{
    public StudentGroupRepository(DataBaseContext context) : base(context)
    {
    }
}