using Microsoft.AspNetCore.Mvc;
using StorageBroker.Dto;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using WebAPI.Dto;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IRepositoryManager repositoryManager;

    public StudentController(IRepositoryManager repositoryManager)
    {
        this.repositoryManager = repositoryManager;
    }

    [HttpGet]
    [Produces(typeof(ResponseDto<Student>))]
    public IActionResult GetStudents()
    {
        return Ok(repositoryManager.StudentRepository.GetAll());
    }

    [HttpGet("{StudentId:int}")]
    [Produces(typeof(ResponseDto<Student>))]
    public IActionResult GetStudentById(int id)
    {
        var student = repositoryManager.StudentRepository
            .GetAllWithCondition(student => student.StudentId == id);

        if (student is null)
            throw new Exception("Student not found");

        return Ok(student);
    }

    [HttpPost]
    [Produces(typeof(ResponseDto<Student>))]
    public async Task CreateStudent([FromBody] Student student)
    {
        var newStudent = new Student();
        newStudent.Name = student.Name;
        newStudent.Surname = student.Surname;


        await repositoryManager.StudentRepository.CreateAsync(newStudent);

        repositoryManager.Save();
    }

    [HttpPut]
    [Produces(typeof(ResponseDto<Student>))]
    public void UpdateStudent([FromBody] Student student)
    {
        var newStudent = new Student();
        newStudent.Name = student.Name;
        newStudent.Surname = student.Surname;

        repositoryManager.StudentRepository.Update(newStudent);

        repositoryManager.Save();
    }

    [HttpDelete]
    [Produces(typeof(int))]
    public void DeleteStudentById(int id)
    {
        var oldStudent = repositoryManager.StudentRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldStudent is null)
            throw new Exception("Student not found");

        repositoryManager.StudentRepository.Delete(oldStudent);

        repositoryManager.Save();
    }
}