using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Common.Contracts.Services;
public interface ISetupService
{
    Task<IEnumerable<IdentityRole>> GetAllRoles();
    Task<BaseResponse> CreateRole(AddRoleRequest role);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<BaseResponse> AddUserToRole(AddUserToRoleDto addUserToRoleDto);
    Task<BaseResponse> CreateUser(CreateUserDto createUserDto, string role);
    Task<BaseResponse> DeleteRole(string id);
    Task<BaseResponse> DeleteUserFromRole(DeleteUserfromRoleDto deleteUserfromRole);
    Task<IEnumerable<string>> GetUserRoles(GetUserRolesDto getUserRolesDto);

}
