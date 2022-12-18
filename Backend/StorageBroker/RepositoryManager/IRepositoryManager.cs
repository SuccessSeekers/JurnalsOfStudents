namespace StorageBroker.RepositoryManager;

public interface IRepositoryManager
{
    TeacherRepository TeacherRepository { get; }
    StudentRepository StudentRepository { get; }
    GroupRepository GroupRepository { get; }
    void Save();
}