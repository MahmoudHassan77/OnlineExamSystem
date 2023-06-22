using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Contracts.Services;

namespace OnlineExamSystem.Services.Services;
public class SetupService : ISetupService
{
    readonly ISetupRepository _setupRepository;

    public SetupService(ISetupRepository setupRepository)
    {
        _setupRepository = setupRepository;
    }

    public List<IdentityRole> GetAllRoles()
    {
        return _setupRepository.GetAllRoles();
    }
}
