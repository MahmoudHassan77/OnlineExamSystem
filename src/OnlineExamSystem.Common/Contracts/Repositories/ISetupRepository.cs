
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Contracts.Repositories;
public interface ISetupRepository
{
    Task<IEnumerable<IdentityRole>> GetAllRoles();
    Task<IdentityResult> CreateRole(AddRoleRequest role);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<IdentityResult> AddUserToRole(AddUserToRoleDto addUserToRoleDto);
    Task<IdentityResult> CreateUser(CreateUserDto createUserDto);
    Task<IdentityResult> DeleteRole(string id);
}
