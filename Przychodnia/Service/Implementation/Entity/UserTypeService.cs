using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class UserTypeService(IUserTypeRepository repo) : IUserTypeService
{
    private readonly IUserTypeRepository _repo = repo;

    public async Task<List<UserType>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<List<string>> GetNamesAsync()
        => await _repo.GetNamesAsync();
}
