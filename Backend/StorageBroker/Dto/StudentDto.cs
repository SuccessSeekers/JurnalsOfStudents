using StorageBroker.Models;

namespace StorageBroker.Dto;

public class StudentDto
{
<<<<<<< HEAD
<<<<<<< HEAD
    public int StudentId { get; set; }
=======
>>>>>>> origin/main
=======
>>>>>>> origin/main
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<GradesLog> Grades { get; set; }
    public ICollection<AttendanceLog> Attendances { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
    public ICollection<Group> Groups { get; set; }
}
=======
    public ICollection<Group> StudentGroups { get; set; }
}
>>>>>>> origin/main
=======
    public ICollection<Group> StudentGroups { get; set; }
}
>>>>>>> origin/main
