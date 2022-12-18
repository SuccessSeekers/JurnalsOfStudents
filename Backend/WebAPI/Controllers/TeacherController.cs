using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using StorageBroker.Dto;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IRepositoryManager RepositoryManager;

        public TeacherController(IRepositoryManager repositoryManager)
        {
            this.RepositoryManager = repositoryManager;
        }

        [HttpGet]
        [Produces(typeof(ResponseDto<Teacher>))]
        public ResponseDto<TeacherDto> GetTeachers()
        {
            var teachers = RepositoryManager.TeacherRepository.GetAll().Select(teacher =>
                new TeacherDto()
                {
                    Name = teacher.Name
                }
            );

            return new ResponseDto<TeacherDto>(teachers);
        }

        [HttpGet("{id:int}")]
        [Produces(typeof(ResponseDto<TeacherDto>))]
        public ResponseDto<TeacherDto> GetTeacherById(int id)
        {
            var teacher = RepositoryManager.TeacherRepository
                .GetAll()
                .FirstOrDefault(teacher => teacher.TeacherId == id);
            if (teacher is null)
                return new ResponseDto<TeacherDto>(StatusCodes.Status400BadRequest, new Exception("Teacher not found"));
            var teacherDto = new TeacherDto()
            {
                Name = teacher.Name
            };
            return new ResponseDto<TeacherDto>(teacherDto);
        }

        [HttpPost]
        [Produces(typeof(ResponseDto<TeacherDto>))]
        public async Task<ResponseDto<TeacherDto>> CreateTeacher([FromBody] CreateTeacherDto createTeacherDto)
        {
            var newTeacher = new Teacher()
            {
                Name = createTeacherDto.Name
            };
            var createdTeacher = await RepositoryManager.TeacherRepository.CreateAsync(newTeacher);
            await RepositoryManager.SaveAsync();
            var teacherDto = new TeacherDto()
            {
                Name = createdTeacher.Name
            };
            return new ResponseDto<TeacherDto>(teacherDto);
        }

        [HttpPut]
        [Produces(typeof(ResponseDto<TeacherDto>))]
        public async Task<ResponseDto<TeacherDto>> UpdateTeacher(int id, [FromBody] UpdateTeacherDto updateTeacherDto)
        {
            var oldTeacher = RepositoryManager.TeacherRepository.GetAll().FirstOrDefault(teacher => teacher.TeacherId == id);
            if (oldTeacher == null)
                return new ResponseDto<TeacherDto>(404, new Exception("Teacher not found"));

            oldTeacher.Name = updateTeacherDto.Name;
            var updatedTeacher = RepositoryManager.TeacherRepository.Update(oldTeacher);
            await RepositoryManager.SaveAsync();
            var teacherDto = new TeacherDto()
            {
                Name = oldTeacher.Name
            };
            return new ResponseDto<TeacherDto>(teacherDto);
        }

        [HttpDelete]
        [Produces(typeof(ResponseDto<TeacherDto>))]
        public async Task<ResponseDto<TeacherDto>> DeleteTeacherById(int id)
        {
            var oldTeacher = RepositoryManager.TeacherRepository
                .GetAll()
                .FirstOrDefault(teacher => teacher.TeacherId == id);

            if (oldTeacher == null)
                return new ResponseDto<TeacherDto>(404, new Exception("Teacher not found"));
            RepositoryManager.TeacherRepository.Delete(oldTeacher);
            await RepositoryManager.SaveAsync();
            var teacherDto = new TeacherDto()
            {
                Name = oldTeacher.Name
            };
            return new ResponseDto<TeacherDto>(teacherDto);
        }
    }
}