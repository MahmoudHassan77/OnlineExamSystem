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
        return await _userManager.AddToRoleAsync(user!, addUserToRoleDto.RoleName);
    }

    public async Task<IdentityResult> CreateUser(CreateUserDto createUserDto, string role)
    {
        ApplicationUser newUser = new ApplicationUser
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PhoneNumber = createUserDto.PhoneNumber,
            UserName = createUserDto.Username.ToLower(),
            Age = createUserDto.Age,
        };
        var userAdded = await _userManager.CreateAsync(newUser, createUserDto.Password);
        if(userAdded.Succeeded)
         return await _userManager.AddToRoleAsync(newUser, role);
        return userAdded;
    }

    public async Task<IdentityResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        return await _roleManager.DeleteAsync(role!);
    }
    public async Task<IdentityResult> DeleteUserFromRole(DeleteUserfromRoleDto deleteUserfromRole)
    {
        var user = await _userManager.FindByEmailAsync(deleteUserfromRole.Email);
        return await _userManager.RemoveFromRoleAsync(user!, deleteUserfromRole.RoleName);
    }
    public async Task<IEnumerable<string>> GetUserRoles(GetUserRolesDto getUserRolesDto)
    {
        var user = await _userManager.FindByEmailAsync(getUserRolesDto.Email);
        return await _userManager.GetRolesAsync(user!);

    }

    public async Task<IdentityResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return await _userManager.DeleteAsync(user!);
    }
}
