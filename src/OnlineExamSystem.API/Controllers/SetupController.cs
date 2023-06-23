using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Exceptions;

namespace OnlineExamSystem.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SetupController : ControllerBase
{
    readonly ISetupService _setup;
    private readonly ILogger<SetupController> _logger;

    public SetupController(ISetupService setup,ILogger<SetupController> logger)
    {
        _setup = setup;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles =await _setup.GetAllRoles();
        return Ok(roles);
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole(AddRoleRequest role)
    {
        var result = await _setup.CreateRole(role);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _setup.GetAllUsers();
        return Ok(users);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> AddUserToRole()
    {
        // Check if user exist, if role exist, if user assigned to this role
        return Ok();
    }
    
}
