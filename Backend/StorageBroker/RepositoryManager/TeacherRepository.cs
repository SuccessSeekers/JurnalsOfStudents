using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class TeacherRepository : RepositoryBase<Teacher>
{
    public TeacherRepository(DataBaseContext context) : base(context)
    {
    }
}