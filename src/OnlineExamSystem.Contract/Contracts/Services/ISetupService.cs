using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Contracts.Services;
public interface ISetupService
{
    List<IdentityRole> GetAllRoles();
    Task<BaseResponse> CreateRole(AddRoleRequest role);
}
