using StorageBroker.Models;

namespace StorageBroker.Dto;

public class GradesLogDto
{
    public int Id { get; set; }
    public TypeOfGrade Grade { get; set; }
    public Student Student { get; set; }
    public Group Group { get; set; }
}