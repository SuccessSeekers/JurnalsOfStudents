﻿using Microsoft.AspNetCore.Mvc;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;

namespace WebAPI.Controllers
{
    public class TeacherController : ControllerBase
    {
        private readonly IRepositoryManager RepositoryManager;

        public TeacherController(IRepositoryManager repositoryManager)
        {
            this.RepositoryManager = repositoryManager;
        }

        [HttpGet]
        [Produces(nameof(Teacher))]
        public IActionResult GetTeachers()
        {
            return Ok(RepositoryManager.TeacherRepository.GetAll());
        }

        [HttpGet("{TeacherId:int}")]
        [Produces(nameof(Teacher))]
        public IActionResult GetTeacherById(int id)
        {
            IQueryable<Teacher> teacher = RepositoryManager.TeacherRepository
                .GetAllWithCondition(teacher => teacher.TeacherId == id);

            if (teacher is null)
                throw new Exception("Teacher not found");

            return Ok(teacher);
        }

        [HttpPost]
        [Produces(nameof(Teacher))]
        public void CreateTeacher([FromBody]Teacher teacher)
        {
            RepositoryManager.TeacherRepository.Create(teacher);
        }

        [HttpPut]
        [Produces(nameof(Teacher))]
        public void UpdateTeacher([FromBody]Teacher teacher)
        {
            RepositoryManager.TeacherRepository.Update(teacher);
        }

        [HttpDelete]
        [Produces(nameof(Teacher))]
        public void DeleteTeacherById(int id)
        {
            var oldTeacher = RepositoryManager.TeacherRepository.GetAll()
                .FirstOrDefault(teacher => teacher.TeacherId == id);
            if (oldTeacher is null)
                throw new Exception("Teacher not found");

            RepositoryManager.TeacherRepository.Delete(oldTeacher);
        }

    }
}