namespace StorageBroker.RepositoryManager;

public interface IRepositoryManager
{
    TeacherRepository TeacherRepository { get; }
    StudentRepository StudentRepository { get; }
    void Save();
}