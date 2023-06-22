using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Contract.Abstract;
public interface ISetupService
{
    List<IdentityRole> GetAllRoles();
}
