using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Repositories;

namespace Przychodnia.Features.Entities.UserTypesFeature.Services;

public class UserTypeService(IUserTypeRepository repo) : IUserTypeService
{
    private readonly IUserTypeRepository _repo = repo;

    public async Task<List<UserType>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<UserType?> GetByIdAsync(int id)
    {
        var userType = await _repo.GetByIdAsync(id);
        return userType is null ? 
            throw new KeyNotFoundException($"Not found: {typeof(UserType).Name} with id: {id}") : userType;
    }
        

    public async Task<List<string>> GetNamesAsync()
        => await _repo.GetNamesAsync();
}
