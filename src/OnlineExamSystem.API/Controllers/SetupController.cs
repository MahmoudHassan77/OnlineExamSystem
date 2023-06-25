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
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var result = await _setup.CreateUser(createUserDto);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleDto addUserToRoleDto)
    {
        var result = await _setup.AddUserToRole(addUserToRoleDto);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await _setup.DeleteRole(id);
        return Ok(result);
    }

    
}
