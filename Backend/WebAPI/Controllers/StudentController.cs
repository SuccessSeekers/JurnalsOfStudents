using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageBroker.Dto;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using System.Security.Cryptography;
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
<<<<<<< HEAD
<<<<<<< HEAD
    [Produces(typeof(ResponseDto<StudentDto>))]
    public ResponseDto<StudentDto> GetStudents()
    {
        return new ResponseDto<StudentDto>(repositoryManager.StudentRepository.GetAll()
            .Select(student => new StudentDto()
            {
                StudentId = student.StudentId,
                Attendances = student.Attendances,
                Grades = student.Grades,
                Groups = student.StudentGroups.Select(x => x.Group).ToList(),
                Name = student.Name,
                Surname = student.Surname
            }));
=======
    [Produces(typeof(ResponseDto<Student>))]
    public ResponseDto<Student> GetStudents()
    {
        return new ResponseDto<Student>(repositoryManager.StudentRepository.GetAll());
>>>>>>> origin/main
=======
    [Produces(typeof(ResponseDto<Student>))]
    public ResponseDto<Student> GetStudents()
    {
        return new ResponseDto<Student>(repositoryManager.StudentRepository.GetAll());
>>>>>>> origin/main
    }

    [HttpGet("{StudentId:int}")]
    [Produces(typeof(ResponseDto<Student>))]
    public ResponseDto<Student> GetStudentById(int id)
    {
        var student = repositoryManager.StudentRepository
            .GetAllWithCondition(student => student.StudentId == id);

        if (student is null)
            return new ResponseDto<Student>(StatusCodes.Status400BadRequest, new Exception("Student not found"));

        return new ResponseDto<Student>(student);
    }

    [HttpPost]
<<<<<<< HEAD
<<<<<<< HEAD
    [Produces(typeof(ResponseDto<Student>))]
    public async Task CreateStudent([FromBody] CreateStudentDto student)
=======
    [Produces(typeof(ResponseDto<StudentDto>))]
    public async Task<ResponseDto<StudentDto>> CreateStudent([FromBody] CreateStudentDto createStudentDto)
>>>>>>> origin/main
=======
    [Produces(typeof(ResponseDto<StudentDto>))]
    public async Task<ResponseDto<StudentDto>> CreateStudent([FromBody] CreateStudentDto createStudentDto)
>>>>>>> origin/main
    {
        var newStudent = new Student();
        newStudent.Name = createStudentDto.Name;
        newStudent.Surname = createStudentDto.Surname;

        var createdStudent = await repositoryManager.StudentRepository
            .CreateAsync(newStudent);

        await repositoryManager.SaveAsync();

        var studentDto = new StudentDto()
        {
            Name = createdStudent.Name,
            Surname = createdStudent.Surname,
            Grades = createdStudent.Grades,
            Attendances = createdStudent.Attendances,
            StudentGroups = createdStudent.StudentGroups
        };

        return new ResponseDto<StudentDto>(studentDto);
    }

    [HttpPut]
<<<<<<< HEAD
<<<<<<< HEAD
    [Produces(typeof(ResponseDto<Student>))]
    public void UpdateStudent([FromBody] UpdateStudentDto student)
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
=======
    [Produces(typeof(ResponseDto<StudentDto>))]
    public async Task<ResponseDto<StudentDto>> UpdateStudentById(int id, [FromBody] UpdateStudentDto updateStudent)
>>>>>>> origin/main
=======
    [Produces(typeof(ResponseDto<StudentDto>))]
    public async Task<ResponseDto<StudentDto>> UpdateStudentById(int id, [FromBody] UpdateStudentDto updateStudent)
>>>>>>> origin/main
    {
        var oldStudent = repositoryManager.StudentRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldStudent is null)
            return new ResponseDto<StudentDto>(404, new Exception("Student not found"));

        oldStudent.Name = updateStudent.Name;
        oldStudent.Surname = updateStudent.Surname;

        repositoryManager.StudentRepository.Update(oldStudent);

        await repositoryManager.SaveAsync();

        var studentDto = new StudentDto()
        {
            Name = oldStudent.Name,
            Surname = oldStudent.Surname,
            Grades = oldStudent.Grades,
            Attendances = oldStudent.Attendances,
            StudentGroups = oldStudent.StudentGroups
        };

        return new ResponseDto<StudentDto>(studentDto);
    }

    [HttpDelete]
    [Produces(typeof(ResponseDto<StudentDto>))]
    public async Task<ResponseDto<StudentDto>> DeleteStudentById(int id)
    {
        var oldStudent = repositoryManager.StudentRepository.GetAll()
            .FirstOrDefault(student => student.StudentId == id);

        if (oldStudent is null)
            return new ResponseDto<StudentDto>(StatusCodes.Status400BadRequest, new Exception("Group not found"));

        repositoryManager.StudentRepository.Delete(oldStudent);

        await repositoryManager.SaveAsync();

        var studentDto = new StudentDto()
        {
            Name = oldStudent.Name,
            Surname = oldStudent.Surname,
            Grades = oldStudent.Grades,
            Attendances = oldStudent.Attendances,
            StudentGroups = oldStudent.StudentGroups
        };
        return new ResponseDto<StudentDto>(studentDto);
    }
}