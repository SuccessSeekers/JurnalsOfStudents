namespace StorageBroker.RepositoryManager;

public interface IRepositryBase<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAllWithCondition(Func<T, bool> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
    ValueTask<T> CreateAsync(T entity);
}