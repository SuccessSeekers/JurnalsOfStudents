
namespace StorageBroker.Models;

public class GradesLog
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int StudentId { get; set; }
    public TypeOfGrade Grade { get; set; }
}

public enum TypeOfGrade
{
    DailyGrade,
    HomeworkGrade,
    ExamGrade
}

