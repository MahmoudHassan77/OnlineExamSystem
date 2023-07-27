using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Enums;

namespace OnlineExamSystem.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStudent(CreateUserDto user)
    {
        var result = await _authService.Register(user, UserRoles.Student);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterInstructor(CreateUserDto user)
    {
        var result = await _authService.Register(user, UserRoles.Instructor);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto cred)
    {
        var result = await _authService.Login(cred);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken()
    {
        var result = await _authService.RefreshToken();
        if (!result.Success) return BadRequest(result);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.Logout();
        if (!result.Success) return BadRequest(result);
        return Ok();
    }
}
 