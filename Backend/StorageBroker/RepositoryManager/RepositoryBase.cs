using Microsoft.EntityFrameworkCore;

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

    public T Create(T entity)
    {
        return _context.Set<T>().Add(entity).Entity;
    }

    public T Update(T entity)
    {
        return _context.Set<T>().Update(entity).Entity;
    }

    public T Delete(T entity)
    {
        return _context.Set<T>().Remove(entity).Entity;
    }

    public async ValueTask<T> CreateAsync(T entity)
    {
        return (await _context.Set<T>().AddAsync(entity)).Entity;
    }
}