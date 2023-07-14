using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Enums;
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
        if (!result.Success) return BadRequest(result); 
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
        var result = await _setup.CreateUser(createUserDto, UserRoles.Admin);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleDto addUserToRoleDto)
    {
        var result = await _setup.AddUserToRole(addUserToRoleDto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await _setup.DeleteRole(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserRoles(string email)
    {
        var roles = await _setup.GetUserRoles(new GetUserRolesDto(email));
        return Ok(roles);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteUserFromRole(DeleteUserfromRoleDto deleteUserfromRole)
    {
        var result = await _setup.DeleteUserFromRole(deleteUserfromRole);
        return Ok(result);
    }
}
