using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class StudentRepository : RepositoryBase<Student>
{
    public StudentRepository(DataBaseContext context) : base(context)
    {
    }
}