namespace StorageBroker.Models;

public class GradesLog
{
    public int Id { get; set; }
    public Group Group { get; set; }
    public Student Student { get; set; }
    public TypeOfGrade Grade { get; set; }
}

public enum TypeOfGrade
{
    DailyGrade,
    HomeworkGrade,
    ExamGrade
}