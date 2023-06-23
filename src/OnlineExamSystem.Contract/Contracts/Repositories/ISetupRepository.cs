
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;

namespace OnlineExamSystem.Common.Contracts.Repositories;
public interface ISetupRepository
{
    List<IdentityRole> GetAllRoles();
    Task<IdentityResult> CreateRole(AddRoleRequest role);
}
