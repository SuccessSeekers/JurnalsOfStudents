namespace StorageBroker.RepositoryManager;

public abstract class RepositoryBase<T> : IRepositryBase<T> where T : class
{
    private readonly DataBaseContext _context;

    public RepositoryBase(DataBaseContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> GetAllWithCondition(Func<T, bool> predicate)
    {
        return _context.Set<T>().Where(predicate).AsQueryable();
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}