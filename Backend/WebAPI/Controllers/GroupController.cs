using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ResponseDto<GroupDto> GetAllGroups()
        {
            var groups = repositoryManager.GroupRepository.GetAll().Select(group =>
                new GroupDto()
                {
                    Name = group.Name,
                    Status = group.Status,
                    Students = group.StudentGroups.Select(x => x.Student).ToList(),
                    Teachers = group.Teachers,
                    CountOfStudents = group.CountOfStudents
                }
            );

            return new ResponseDto<GroupDto>(groups);
        }

        [HttpGet("{id:int}")]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public ResponseDto<GroupDto> GetGroupById(int id)
        {
            var group = repositoryManager.GroupRepository
                .GetAll()
                .Where(x => x.GroupId == id)
                .FirstOrDefault(x => x.GroupId == id);
            if (group == null)
                return new ResponseDto<GroupDto>(StatusCodes.Status400BadRequest, new Exception("Group not found"));
            var groupDto = new GroupDto()
            {
                Name = group.Name,
                Status = group.Status,
                Students = group.StudentGroups.AsQueryable()
                    .Include(x => x.Student)
                    .Select(x => x.Student).ToList(),
                Teachers = group.Teachers,
                CountOfStudents = group.CountOfStudents
            };
            return new ResponseDto<GroupDto>(groupDto);
        }

        [HttpPost]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public async Task<ResponseDto<GroupDto>> CreateGroupById([FromBody] CreateGroupDto createGroupDto)
        {
            var newGroup = new Group()
            {
                Name = createGroupDto.Name,
                Status = createGroupDto.Status,
                CountOfStudents = createGroupDto.CountOfStudents
            };
            var createdGroup = await repositoryManager.GroupRepository.CreateAsync(newGroup);
            await repositoryManager.SaveAsync();
            var groupDto = new GroupDto()
            {
                Name = createdGroup.Name,
                Status = createdGroup.Status,
                Students = createdGroup.StudentGroups.Select(x => x.Student).ToList(),
                Teachers = createdGroup.Teachers,
                CountOfStudents = createdGroup.CountOfStudents
            };
            return new ResponseDto<GroupDto>(groupDto);
        }

        [HttpPost("addStudent/")]
        public async Task AddStudent(int studentId, int groupId)
        {
            var group = repositoryManager.GroupRepository.GetAll().FirstOrDefault(group => group.GroupId == groupId);
            var student = repositoryManager.StudentRepository.GetAll()
                .FirstOrDefault(student => student.StudentId == studentId);
            await repositoryManager.StudentGroupRepository.CreateAsync(new StudentGroup()
            {
                StudentId = student.StudentId,
                GroupId = group.GroupId
            });
            await repositoryManager.SaveAsync();
        }

        [HttpPut]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public async Task<ResponseDto<GroupDto>> UpdateGroupById(int id, [FromBody] UpdateGroupDto updateGroupDto)
        {
            var oldGroup = repositoryManager.GroupRepository.GetAll().FirstOrDefault(group => group.GroupId == id);
            if (oldGroup == null)
                return new ResponseDto<GroupDto>(404, new Exception("Group not found"));
            oldGroup.Name = updateGroupDto.Name;
            oldGroup.Status = updateGroupDto.Status;
            var updatedGroup = repositoryManager.GroupRepository.Update(oldGroup);
            await repositoryManager.SaveAsync();
            var groupDto = new GroupDto()
            {
                Name = oldGroup.Name,
                Status = oldGroup.Status,
                Students = oldGroup.StudentGroups.Select(x => x.Student).ToList(),
                Teachers = oldGroup.Teachers,
                CountOfStudents = oldGroup.CountOfStudents
            };
            return new ResponseDto<GroupDto>(groupDto);
        }

        [HttpDelete]
        [Produces(typeof(ResponseDto<GroupDto>))]
        public async Task<ResponseDto<GroupDto>> DeleteGroupById(int id)
        {
            var oldGroup = repositoryManager.GroupRepository
                .GetAll()
                .FirstOrDefault(group => group.GroupId == id);
            if (oldGroup == null)
                return new ResponseDto<GroupDto>(404, new Exception("Group not found"));
            repositoryManager.GroupRepository.Delete(oldGroup);
            await repositoryManager.SaveAsync();
            var groupDto = new GroupDto()
            {
                Name = oldGroup.Name,
                Status = oldGroup.Status,
                Students = oldGroup.StudentGroups.Select(x => x.Student).ToList(),
                Teachers = oldGroup.Teachers,
                CountOfStudents = oldGroup.CountOfStudents
            };
            return new ResponseDto<GroupDto>(groupDto);
        }
    }
}