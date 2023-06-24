using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Data;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.DAL.Repositories;
public class SetupRepository : ISetupRepository
{
    readonly ApplicationDbContext _applicationDbContext;
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<IdentityRole> _roleManager;

    public SetupRepository(ApplicationDbContext applicationDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<IEnumerable<IdentityRole>> GetAllRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityResult> CreateRole(AddRoleRequest role)
    {
       return await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IdentityResult> AddUserToRole(AddUserToRoleDto addUserToRoleDto)
    {
        var user = await _userManager.FindByEmailAsync(addUserToRoleDto.Email);
        return await _userManager.AddToRoleAsync(user, addUserToRoleDto.RoleName);
    }
}
