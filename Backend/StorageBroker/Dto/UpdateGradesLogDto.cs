
using StorageBroker.Models;

namespace StorageBroker.Dto;

public class UpdateGradesLogDto
{
    public TypeOfGrade Grade { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
}
