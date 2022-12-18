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
    public async Task<ResponseDto<GradesLog>> CreateGradesLog([FromBody] CreateGradesLogDto createGradesLogDto)
    {
        var newGradesLog = new GradesLog()
        {
            Grade = createGradesLogDto.Grade,
            GroupId = createGradesLogDto.GroupId,
            StudentId = createGradesLogDto.StudentId
        };
        var createdGradesLog = await repositoryManager.GradesLogRepository.CreateAsync(newGradesLog);
        return new ResponseDto<GradesLog>(createdGradesLog);
    }

    [HttpPut()]
    [Produces(typeof(ResponseDto<GradesLog>))]
    public ResponseDto<GradesLog> UpdateGradesLog(int gradeId, [FromBody] UpdateGradesLogDto updateGradesLogDto)
    {
        var oldGradesLog = repositoryManager.GradesLogRepository.GetAll().FirstOrDefault(grade => grade.Id == gradeId);
        if (oldGradesLog is null)
            return new ResponseDto<GradesLog>(400, new Exception("Wrong grade Id number"));
        var newGradeLog = new GradesLog()
        {
            Grade = updateGradesLogDto.Grade,
            GroupId = updateGradesLogDto.GroupId,
            StudentId = updateGradesLogDto.StudentId,
        };
        var updatedGradesLog = repositoryManager.GradesLogRepository.Update(newGradeLog);
        return new ResponseDto<GradesLog>(updatedGradesLog);
    }

    [HttpDelete]
    [Produces(typeof(ResponseDto<GradesLog>))]
    public ResponseDto<GradesLog> DeleteGradesLogById(int id)
    {
        var oldGradesLog = repositoryManager.GradesLogRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldGradesLog is null)
            throw new Exception("Student not found");

        var deletedStudent = repositoryManager.GradesLogRepository.Delete(oldGradesLog);
        return new ResponseDto<GradesLog>(deletedStudent);
    }
}