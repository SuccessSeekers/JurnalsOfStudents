namespace StorageBroker.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly DataBaseContext _context;
    private TeacherRepository _teacherRepository;

    public RepositoryManager(DataBaseContext context)
    {
        _context = context;
    }

    public TeacherRepository TeacherRepository
    {
        get
        {
            if (_teacherRepository == null)
                _teacherRepository = new TeacherRepository(_context);
            return _teacherRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}