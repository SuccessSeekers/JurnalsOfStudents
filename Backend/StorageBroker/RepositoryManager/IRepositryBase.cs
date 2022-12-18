namespace StorageBroker.RepositoryManager;

public interface IRepositryBase<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAllWithCondition(Func<T, bool> predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}