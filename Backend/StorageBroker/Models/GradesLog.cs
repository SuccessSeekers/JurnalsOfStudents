using System.ComponentModel.DataAnnotations.Schema;

namespace StorageBroker.Models;

public class GradesLog
{
    public int Id { get; set; }
    public TypeOfGrade Grade { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public Student Student { get; set; }

    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }
    public Group Group { get; set; }
}

public enum TypeOfGrade
{
    DailyGrade,
    HomeworkGrade,
    ExamGrade
}