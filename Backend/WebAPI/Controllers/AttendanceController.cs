using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageBroker.Dto;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;

        public AttendanceController(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        [HttpGet]
        [Produces(typeof(ResponseDto<AttendanceLog>))]
        public ResponseDto<AttendanceLog> GetAttendanceLogs()
        {
            return new ResponseDto<AttendanceLog>(repositoryManager.AttendanceLogRepository.GetAll()
                .Include(attendance => attendance.Group));
        }

        [HttpGet("{AttendanceLogId}")]
        [Produces(typeof(ResponseDto<AttendanceDto>))]
        public ResponseDto<AttendanceDto> GetAttendanceLogById(int id)
        {
           var attendance = repositoryManager.AttendanceLogRepository
                .GetAll()
                .FirstOrDefault(attendance => attendance.AttendanceLogId == id);

            if (attendance is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("Attendance not found"));
            var attendanceDto = new AttendanceDto()
            {
                Date = attendance.Date,
                LateTime = attendance.LateTime,
                Status = attendance.Status,
                StudentId = attendance.StudentId,
                GroupId = attendance.GroupId
            };

            return new ResponseDto<AttendanceDto>(attendanceDto);
        }

        [HttpPost]
        [Produces(typeof(ResponseDto<AttendanceDto>))]
        public async Task<ResponseDto<AttendanceDto>> CreateAttendanceLog(
            [FromBody] CreateAttendanceLogDto attendanceLog)
        {
            var student = repositoryManager.StudentRepository.GetAll()
                .FirstOrDefault(student => student.StudentId == attendanceLog.StudentId);
            var group = repositoryManager.GroupRepository.GetAll()
                .FirstOrDefault(group => group.GroupId == attendanceLog.GroupId);

            if (student is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("Student not found"));
            if (group is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("Group not found"));

            Group studentGroup = null;

            try
            {
                studentGroup = student.StudentGroups.FirstOrDefault(gr => gr.GroupId == group.GroupId);
            }
            catch (Exception ex)
            {
                return new ResponseDto<AttendanceDto>(404, new Exception("This student does not exist in the group"));
            }

            if (studentGroup is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("This student does not exist in the group"));

            var attendanceDto = new AttendanceDto()
            {
                Date = attendanceLog.Date,
                LateTime = attendanceLog.LateTime,
                Status = attendanceLog.Status,
                StudentId = attendanceLog.StudentId,
                GroupId = attendanceLog.GroupId
            };
            var ettendance = new AttendanceLog()
            {
                Date = attendanceDto.Date,
                LateTime = attendanceDto.LateTime,
                Status = attendanceDto.Status,
                Student = student,
                Group = group,
                StudentId = attendanceLog.StudentId,
                GroupId = attendanceLog.GroupId
            };

            var createdAttendanceLog = await repositoryManager.AttendanceLogRepository.CreateAsync(ettendance);
            await repositoryManager.SaveAsync();

            return new ResponseDto<AttendanceDto>(attendanceDto);
        }

        [HttpPut]
        [Produces(typeof(ResponseDto<AttendanceDto>))]
        public async Task<ResponseDto<AttendanceDto>> UpdateAttendance([FromBody] UpdateAttendanceLogDto attendanceLog)
        {
            var currentAttendance = repositoryManager.AttendanceLogRepository
                .GetAll().FirstOrDefault(attendance => attendance.GroupId == attendanceLog.GroupId && attendance.StudentId == attendanceLog.StudentId);

            if (currentAttendance is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("Attendance not found"));

            currentAttendance.Status = attendanceLog.Status;
            currentAttendance.Date = attendanceLog.Date;
            currentAttendance.LateTime = attendanceLog.LateTime;

            repositoryManager.AttendanceLogRepository.Update(currentAttendance);
            await repositoryManager.SaveAsync();

            var updateAttendance = new AttendanceDto()
            {
                Date = currentAttendance.Date,
                LateTime = currentAttendance.LateTime,
                Status = currentAttendance.Status,
                StudentId = currentAttendance.StudentId,
                GroupId = currentAttendance.GroupId
            };

            return new ResponseDto<AttendanceDto>(updateAttendance);
        }

        [HttpDelete]
        [Produces(typeof(ResponseDto<AttendanceDto>))]
        public async Task<ResponseDto<AttendanceDto>> DeleteAttendanceLogById(int id)
        {
            var oldAttendanceLog = repositoryManager.AttendanceLogRepository.GetAll()
                .FirstOrDefault(attendance => attendance.AttendanceLogId == id);

            if (oldAttendanceLog is null)
                return new ResponseDto<AttendanceDto>(404, new Exception("Attendance not found"));

            var deleledAttendanceLog = repositoryManager.AttendanceLogRepository.Delete(oldAttendanceLog);
            await repositoryManager.SaveAsync();

            var attendanceDto = new AttendanceDto()
            {
                Date = oldAttendanceLog.Date,
                Status = oldAttendanceLog.Status,
                StudentId = oldAttendanceLog.StudentId,
                GroupId = oldAttendanceLog.GroupId,
                LateTime = oldAttendanceLog.LateTime
            };

            return new ResponseDto<AttendanceDto>(attendanceDto);
        }
    }
}