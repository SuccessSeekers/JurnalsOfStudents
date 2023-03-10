namespace StorageBroker.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly DataBaseContext _context;
    private TeacherRepository _teacherRepository;
    private StudentRepository _studentRepository;
    private GroupRepository _groupRepository;
    private AttendanceLogRepository _attendanceLogRepository;
    private GradesLogRepository _gradesLogRepository;
    private StudentGroupRepository _studentGroupRepository;


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

    public AttendanceLogRepository AttendanceLogRepository
    {
        get
        {
            if (_attendanceLogRepository is null)
                _attendanceLogRepository = new AttendanceLogRepository(_context);
            return _attendanceLogRepository;
        }
    }

    public GradesLogRepository GradesLogRepository
    {
        get
        {
            if (_gradesLogRepository == null)
                _gradesLogRepository = new GradesLogRepository(_context);
            return _gradesLogRepository;
        }
    }

    public StudentGroupRepository StudentGroupRepository
    {
        get
        {
            if (_studentGroupRepository == null)
                _studentGroupRepository = new StudentGroupRepository(_context);
            return _studentGroupRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}