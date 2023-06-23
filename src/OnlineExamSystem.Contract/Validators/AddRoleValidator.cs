﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Validators;
public class AddRoleValidator : AbstractValidator<AddRoleRequest>
{
    readonly RoleManager<IdentityRole> _roleManager;

    public AddRoleValidator(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        RuleFor(a => a.RoleName)
            .NotEmpty()
            .NotNull()
            .WithMessage("RoleName is required.");
        RuleFor(a => a.RoleName)
            .MustAsync(async (name, token) =>
            {
                var roleExist = await _roleManager.RoleExistsAsync(name);
                return !roleExist;
            })
            .WithMessage("RoleName is exist!");
        

    }
}
