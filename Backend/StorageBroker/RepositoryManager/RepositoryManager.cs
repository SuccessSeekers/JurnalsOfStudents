namespace StorageBroker.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly DataBaseContext _context;
    private TeacherRepository _teacherRepository;
    private StudentRepository _studentRepository;
    private GroupRepository _groupRepository;
    private AttendanceLogRepository _attendanceLogRepository;

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

    public void Save()
    {
        _context.SaveChanges();
    }
}