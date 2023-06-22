﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Contract.Abstract;

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
    public IActionResult GetAllRoles()
    {
        var roles = _setup.GetAllRoles();
        return Ok(roles);
    }
}