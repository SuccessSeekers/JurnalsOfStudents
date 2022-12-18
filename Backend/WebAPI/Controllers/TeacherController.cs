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
        public ResponseDto<Teacher> GetTeachers()
        {
            return new ResponseDto<Teacher>(RepositoryManager.TeacherRepository.GetAll()
                .Include(teacher => teacher.TeacherGroups));
        }

        [HttpGet("{TeacherId:int}")]
        [Produces(typeof(ResponseDto<Teacher>))]
        public ResponseDto<Teacher> GetTeacherById(int id)
        {
            ResponseDto<Teacher> teacher =new ResponseDto<Teacher>(RepositoryManager.TeacherRepository
                .GetAllWithCondition(teacher => teacher.TeacherId == id));

            if (teacher is null)
                throw new Exception("Teacher not found");

            return teacher;
        }

        [HttpPost]
        [Produces(typeof(ResponseDto<Teacher>))]
        public void CreateTeacher([FromBody] CreateTeacherDto teacher)
        {
            var newTeacher = new Teacher();
            newTeacher.Name = teacher.Name;

            RepositoryManager.TeacherRepository.Create(newTeacher);

            RepositoryManager.Save();
        }

        [HttpPut]
        [Produces(typeof(ResponseDto<Teacher>))]
        public void UpdateTeacher([FromBody] UpdateTeacherDto teacher)
        {
            var newTeacher = new Teacher();
            newTeacher.Name = teacher.Name;

            RepositoryManager.TeacherRepository.Update(newTeacher);

            RepositoryManager.Save();
        }

        [HttpDelete]
        [Produces(typeof(ResponseDto<Teacher>))]
        public void DeleteTeacherById(int id)
        {
            var oldTeacher = RepositoryManager.TeacherRepository.GetAll()
                .FirstOrDefault(teacher => teacher.TeacherId == id);
            if (oldTeacher is null)
                throw new Exception("Teacher not found");

            RepositoryManager.TeacherRepository.Delete(oldTeacher);

            RepositoryManager.Save();
        }
    }
}