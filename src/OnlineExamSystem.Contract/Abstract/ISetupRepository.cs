
using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Contract.Abstract;
public interface ISetupRepository
{
    List<IdentityRole> GetAllRoles();
}
