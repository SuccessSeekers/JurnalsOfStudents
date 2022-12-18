
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
    [Produces(typeof(ResponseDto<GradesLog>))]
    public ResponseDto<GradesLog> GetGradesLogs()
    {
        return new ResponseDto<GradesLog>(repositoryManager.GradesLogRepository.GetAll()
            .Include(gradesLog => gradesLog.Grade));
    }

    [HttpGet("{GradesLogId:int}")]
    [Produces(typeof(ResponseDto<GradesLog>))]
    public IActionResult GetGradesLogById(int id)
    {
        var gradesLog = repositoryManager.GradesLogRepository
            .GetAllWithCondition(gradesLog => gradesLog.Id == id);

        if (gradesLog is null)
            throw new Exception("Student not found");

        return Ok(gradesLog);
    }

    [HttpPost]
    [Produces(typeof(ResponseDto<GradesLog>))]
    public void CreateGradesLog([FromBody] CreateGradesLogDto gradesLog)
    {
        var newGradesLog = new GradesLog();
        newGradesLog.Grade = gradesLog.Grade;
        newGradesLog.StudentId = gradesLog.StudentId;
        newGradesLog.GroupId = gradesLog.GroupId;

        repositoryManager.GradesLogRepository.Create(newGradesLog);
    }

    [HttpPut]
    [Produces(typeof(ResponseDto<GradesLog>))]
    public void UpdateGradesLog([FromBody] UpdateGradesLogDto gradesLog)
    {
        var newGradesLog = new GradesLog();
        newGradesLog.Grade = gradesLog.Grade;
        newGradesLog.StudentId = gradesLog.StudentId;
        newGradesLog.GroupId = gradesLog.GroupId;

        repositoryManager.GradesLogRepository.Update(newGradesLog);
    }

    [HttpDelete]
    [Produces(typeof(int))]
    public void DeleteGradesLogById(int id)
    {
        var oldGradesLog = repositoryManager.GradesLogRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldGradesLog is null)
            throw new Exception("Student not found");

        repositoryManager.GradesLogRepository.Delete(oldGradesLog);
    }
}