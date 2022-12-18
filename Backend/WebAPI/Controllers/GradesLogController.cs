using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageBroker.Dto;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using WebAPI.Dto;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesLogController : ControllerBase
{
    private readonly IRepositoryManager repositoryManager;

    public GradesLogController(IRepositoryManager repositoryManager)
    {
        this.repositoryManager = repositoryManager;
    }

    [HttpGet]
    [Produces(typeof(ResponseDto<GradesLogDto>))]
    public ResponseDto<GradesLogDto> GetGradesLogs()
    {
        var gradesLogs = repositoryManager.GradesLogRepository.GetAll()
            .Select(gradesLog =>
                new GradesLogDto()
                {
                    Grade = gradesLog.Grade,
                    Group = gradesLog.Group,
                    Id = gradesLog.Id,
                    Student = gradesLog.Student
                }
            );
        return new ResponseDto<GradesLogDto>(gradesLogs);
    }

    [HttpGet("{id:int}")]
    [Produces(typeof(ResponseDto<GradesLogDto>))]
    public ResponseDto<GradesLogDto> GetGradesLogById(int id)
    {
        var gradesLog = repositoryManager.GradesLogRepository
            .GetAll()
            .FirstOrDefault(gradesLog => gradesLog.Id == id);

        if (gradesLog is null)
            return new ResponseDto<GradesLogDto>(404, new Exception("Grades log not found"));

        var gradesLogDto = new GradesLogDto()
        {
            Grade = gradesLog.Grade,
            Group = gradesLog.Group,
            Id = gradesLog.Id,
            Student = gradesLog.Student
        };

        return new ResponseDto<GradesLogDto>(gradesLogDto);
    }

    [HttpPost]
    [Produces(typeof(ResponseDto<GradesLogDto>))]
    public async Task<ResponseDto<GradesLogDto>> CreateGradesLog([FromBody] CreateGradesLogDto gradesLog)
    {
        var group = repositoryManager.GroupRepository.GetAll()
            .FirstOrDefault(group => group.GroupId == gradesLog.GroupId);
        if (group == null)
            return new ResponseDto<GradesLogDto>(404, new Exception("Group not found"));
        var student = repositoryManager.StudentRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == gradesLog.StudentId);
        if (student == null)
            return new ResponseDto<GradesLogDto>(404, new Exception("Student not found"));

        var newGradesLog = new GradesLog();
        newGradesLog.Grade = gradesLog.Grade;
        // newGradesLog.StudentId = gradesLog.StudentId;
        // newGradesLog.GroupId = gradesLog.GroupId;
        newGradesLog.Student = student;
        newGradesLog.Group = group;

        var createdGradesLog = await repositoryManager.GradesLogRepository.CreateAsync(newGradesLog);
        await repositoryManager.SaveAsync();
        var gradesLogDto = new GradesLogDto()
        {
            Grade = createdGradesLog.Grade,
            Group = createdGradesLog.Group,
            Id = createdGradesLog.Id,
            Student = createdGradesLog.Student
        };
        return new ResponseDto<GradesLogDto>(gradesLogDto);
    }

    [HttpPut("{id:int}")]
    [Produces(typeof(ResponseDto<GradesLogDto>))]
    public async Task<ResponseDto<GradesLogDto>> UpdateGradesLog(int id, [FromBody] UpdateGradesLogDto gradesLog)
    {
        var oldGradesLog = repositoryManager.GradesLogRepository
            .GetAll()
            .FirstOrDefault(gradesLog => gradesLog.Id == id);
        if (oldGradesLog == null)
            return new ResponseDto<GradesLogDto>(404, new Exception("Grades log not found"));
        oldGradesLog.Grade = gradesLog.Grade;
        oldGradesLog.GroupId = gradesLog.GroupId;
        oldGradesLog.StudentId = gradesLog.StudentId;
        var updatedGradesLog = repositoryManager.GradesLogRepository.Update(oldGradesLog);
        await repositoryManager.SaveAsync();
        var gradesLogDto = new GradesLogDto()
        {
            Grade = updatedGradesLog.Grade,
            Group = updatedGradesLog.Group,
            Id = updatedGradesLog.Id,
            Student = updatedGradesLog.Student
        };
        return new ResponseDto<GradesLogDto>(gradesLogDto);
    }

    [HttpDelete]
    [Produces(typeof(ResponseDto<GradesLogDto>))]
    public async Task<ResponseDto<GradesLogDto>> DeleteGradesLogById(int id)
    {
        var oldGradesLog = repositoryManager.GradesLogRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldGradesLog is null)
            return new ResponseDto<GradesLogDto>(404, new Exception("Grades log not found"));

        repositoryManager.GradesLogRepository.Delete(oldGradesLog);
        await repositoryManager.SaveAsync();

        var gradesLogDto = new GradesLogDto()
        {
            Grade = oldGradesLog.Grade,
            Group = oldGradesLog.Group,
            Id = oldGradesLog.Id,
            Student = oldGradesLog.Student
        };
        return new ResponseDto<GradesLogDto>(gradesLogDto);
    }
}