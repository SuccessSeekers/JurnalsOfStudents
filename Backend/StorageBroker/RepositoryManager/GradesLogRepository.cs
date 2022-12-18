using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class GradesLogRepository : RepositoryBase<GradesLog>
{
    public GradesLogRepository(DataBaseContext context) : base(context)
    {
    }
}