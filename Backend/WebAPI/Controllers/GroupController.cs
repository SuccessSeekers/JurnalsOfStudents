using Microsoft.AspNetCore.Mvc;
using StorageBroker.Models;
using StorageBroker.RepositoryManager;

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
        [Produces(typeof(Group))]
        public IActionResult GetGroups()
        {
            return Ok(repositoryManager.GroupRepository.GetAll());
        }

        [HttpGet("{GroupId:int}")]
        [Produces(typeof(Group))]
        public IActionResult GetGroupById(int id)
        {
            IQueryable<Group> group = repositoryManager.GroupRepository
                .GetAllWithCondition(group => group.GroupId == id);

            if (group is null)
                throw new Exception("Group not found");

            return Ok(group);
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
