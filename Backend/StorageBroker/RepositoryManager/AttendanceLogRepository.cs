using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public class AttendanceLogRepository : RepositoryBase<AttendanceLog>
{
    public AttendanceLogRepository(DataBaseContext context) : base(context)
    {
    }
}