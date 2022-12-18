namespace StorageBroker.RepositoryManager;

public interface IRepositoryManager
{
    TeacherRepository TeacherRepository { get; }
    void Save();
}