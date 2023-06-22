
using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Common.Contracts.Repositories;
public interface ISetupRepository
{
    List<IdentityRole> GetAllRoles();
}
