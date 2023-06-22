using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Common.Contracts.Services;
public interface ISetupService
{
    List<IdentityRole> GetAllRoles();
}
