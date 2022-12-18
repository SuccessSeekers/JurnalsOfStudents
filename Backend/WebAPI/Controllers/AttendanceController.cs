﻿using Microsoft.AspNetCore.Mvc;
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
        [Produces(typeof(ResponseDto<AttendanceLog>))]
        public ResponseDto<AttendanceLog> GetAttendanceLogById(int id)
        {
            ResponseDto<AttendanceLog> attendance = new ResponseDto<AttendanceLog>(repositoryManager.AttendanceLogRepository
                .GetAllWithCondition(attendance => attendance.AttendanceLogId == id));

            if (attendance is null)
                throw new Exception("Attendance not found");

            return attendance;
        }

        [HttpPost]
        [Produces(typeof(ResponseDto<AttendanceLog>))]
        public void CreateAttendanceLog([FromBody] CreateAttendanceLogDto attendanceLog)
        {
            var currentGroup = repositoryManager.GroupRepository
                .GetAll().FirstOrDefault(group => group.GroupId == attendanceLog.GroupId);

            if (currentGroup is null)
                throw new Exception("Group not found");

            var currentStudent = repositoryManager.StudentRepository
                .GetAll().FirstOrDefault(student => student.StudentId == attendanceLog.StudentId);

            if (currentStudent is null)
                throw new Exception("Student not found");

            var newAttendance = new AttendanceLog();
            newAttendance.Date = attendanceLog.Date;
            newAttendance.LateTime = attendanceLog.LateTime;
            newAttendance.Status = attendanceLog.Status;
            newAttendance.StudentId = attendanceLog.StudentId;
            newAttendance.GroupId = attendanceLog.GroupId;
            newAttendance.Group = currentGroup;
            newAttendance.Student = currentStudent;
            
            repositoryManager.AttendanceLogRepository.Create(newAttendance);

            repositoryManager.Save();
        }

        [HttpPut]
        [Produces(typeof(ResponseDto<AttendanceLog>))]
        public void UpdateAttendance([FromBody] UpdateAttendanceLogDto attendanceLog)
        {
            var currentGroup = repositoryManager.GroupRepository
                .GetAll().FirstOrDefault(group => group.GroupId == attendanceLog.GroupId);

            if (currentGroup is null)
                throw new Exception("Group not found");

            var currentStudent = repositoryManager.StudentRepository
                .GetAll().FirstOrDefault(student => student.StudentId == attendanceLog.StudentId);

            if (currentStudent is null)
                throw new Exception("Student not found");

            var newAttendance = new AttendanceLog();
            newAttendance.Date = attendanceLog.Date;
            newAttendance.LateTime = attendanceLog.LateTime;
            newAttendance.Status = attendanceLog.Status;
            newAttendance.StudentId = attendanceLog.StudentId;
            newAttendance.GroupId = attendanceLog.GroupId;
            newAttendance.Group = currentGroup;
            newAttendance.Student = currentStudent;

            repositoryManager.AttendanceLogRepository.Update(newAttendance);

            repositoryManager.Save();
        }

        [HttpDelete]
        [Produces(typeof(ResponseDto<AttendanceLog>))]
        public void DeleteAttendanceLogById(int id)
        {
            var oldAttendanceLog = repositoryManager.AttendanceLogRepository.GetAll()
                .FirstOrDefault(attendance => attendance.AttendanceLogId == id);

            if (oldAttendanceLog is null)
                throw new Exception("AttendanceLog not found");

            repositoryManager.AttendanceLogRepository.Delete(oldAttendanceLog);

            repositoryManager.Save();
        }
    }
}