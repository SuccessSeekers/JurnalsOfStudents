using System.ComponentModel.DataAnnotations.Schema;

namespace StorageBroker.Models;

public class AttendanceLog
{
    public int AttendanceLogId { get; set; }
    public DateTime Date { get; set; }
    public int LateTime { get; set; }
    public LateType Status { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public Student Student { get; set; }

    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }
    public Group Group { get; set; }

}

public enum LateType
{
    DidNotCome,
    Came,
    CameLate
}