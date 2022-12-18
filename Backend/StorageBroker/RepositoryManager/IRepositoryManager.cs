using StorageBroker.Models;

namespace StorageBroker.RepositoryManager;

public interface IRepositoryManager
{
    TeacherRepository TeacherRepository { get; }
    StudentRepository StudentRepository { get; }
    GroupRepository GroupRepository { get; }
    AttendanceLogRepository AttendanceLogRepository { get; }
    GradesLogRepository GradesLogRepository { get; }
    StudentGroupRepository StudentGroupRepository { get; }
    void Save();
    Task SaveAsync();
}