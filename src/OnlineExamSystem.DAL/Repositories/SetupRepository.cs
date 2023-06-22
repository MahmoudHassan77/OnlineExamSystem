using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
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

    public List<IdentityRole> GetAllRoles()
    {
        return _roleManager.Roles.ToList();
    }
}
