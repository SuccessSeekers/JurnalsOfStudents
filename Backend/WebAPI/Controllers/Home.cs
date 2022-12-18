using LoggerService;
using Microsoft.AspNetCore.Mvc;
using StorageBroker.RepositoryManager;
using ILogger = LoggerService.ILogger;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Home : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IRepositoryManager _repositoryManager;

    public Home(ILogger logger, IRepositoryManager repositoryManager)
    {
        _logger = logger;
        _repositoryManager = repositoryManager;
    }

    // GET
    [HttpGet]
    public IActionResult Teachers()
    {
        _logger.LogInfo("Info log");
        return Ok(_repositoryManager.TeacherRepository.GetAll().Count());
    }
}