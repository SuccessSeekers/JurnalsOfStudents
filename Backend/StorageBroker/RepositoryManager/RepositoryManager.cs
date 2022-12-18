namespace StorageBroker.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly DataBaseContext _context;
    private TeacherRepository _teacherRepository;
    private StudentRepository _studentRepository;
    private GroupRepository _groupRepository;
    private GradesLogRepository _gradesLogRepository;

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

    public StudentRepository StudentRepository
    {
        get
        {
            if (_studentRepository == null)
                _studentRepository = new StudentRepository(_context);
            return _studentRepository;
        }
    }

    public GroupRepository GroupRepository
    {
        get
        {
            if (_groupRepository is null)
                _groupRepository = new GroupRepository(_context);
            return _groupRepository;
        }
    }

    public GradesLogRepository GradesLogRepository
    {
        get
        {
            if (_gradesLogRepository is null)
                _gradesLogRepository = new GradesLogRepository(_context);
            return _gradesLogRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}