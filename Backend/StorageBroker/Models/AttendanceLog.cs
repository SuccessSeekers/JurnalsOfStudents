namespace StorageBroker.Models;

public class AttendanceLog
{
    public int AttendanceLogId { get; set; }
    public Group Group { get; set; }
    public Student Student { get; set; }
    public DateTime Date { get; set; }
    public int LateTime { get; set; }
    public LateType Status { get; set; }
}

public enum LateType
{
    DidNotCome,
    Came,
    CameLate
}