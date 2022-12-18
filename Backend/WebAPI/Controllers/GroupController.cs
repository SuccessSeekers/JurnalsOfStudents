using Microsoft.AspNetCore.Mvc;
using StorageBroker.Dto;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;

        public GroupController(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        [HttpGet]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public ResponseDto<GroupDto> GetGroups()
        {
            var groups = repositoryManager.GroupRepository.GetAll().Select(group => new GroupDto()
            {
                Name = group.Name,
                Status = group.Status,
                Students = group.Students,
                Teachers = group.Teachers,
                CountOfStudents = group.CountOfStudents
            });
            return new ResponseDto<GroupDto>(groups);
        }

        [HttpGet("{id:int}")]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public ResponseDto<GroupDto> GetGroupById(int id)
        {
            var groups = repositoryManager.GroupRepository
                .GetAllWithCondition(group => group.GroupId == id)
                .Select(group => new GroupDto()
                {
                    Name = group.Name,
                    Status = group.Status,
                    Students = group.Students,
                    Teachers = group.Teachers,
                    CountOfStudents = group.CountOfStudents
                });

            return new ResponseDto<GroupDto>(400, new Exception("Wrong id number"));
        }

        [HttpPost]
        [Produces(typeof(Group))]
        public void CreateGroup([FromBody] Group group)
        {
            repositoryManager.GroupRepository.Create(group);
        }

        [HttpPut]
        [Produces(typeof(Group))]
        public void UpdateGroup([FromBody] Group group)
        {
            repositoryManager.GroupRepository.Update(group);
        }

        [HttpDelete]
        [Produces(typeof(Group))]
        public void DeleteGroupById(int id)
        {
            var oldGroup = repositoryManager.GroupRepository.GetAll()
                .FirstOrDefault(group => group.GroupId == id);
            if (oldGroup is null)
                throw new Exception("Group not found");

            repositoryManager.GroupRepository.Delete(oldGroup);
        }
    }
}