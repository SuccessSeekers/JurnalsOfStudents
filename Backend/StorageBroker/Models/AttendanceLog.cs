namespace StorageBroker.Models
{
    public class AttendanceLog
    {
        public int AttendanceLogId { get; set; }
        public int GroupId { get; set; }
        public int StudentId { get; set; }
        public int Mark { get; set; }
    }
}