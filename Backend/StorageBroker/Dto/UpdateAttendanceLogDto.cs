using StorageBroker.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageBroker.Dto
{
    public class UpdateAttendanceLogDto
    {
        public DateTime Date { get; set; }
        public int LateTime { get; set; }
        public LateType Status { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
    }
}
