using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

public class UserTypeService(IUserTypeRepository repo) : IUserTypeService
{
    private readonly IUserTypeRepository _repo = repo;

    public async Task<List<UserType>> GetAllAsync()
        => await repo.GetAllAsync();

    public async Task<List<string>> GetNamesAsync()
        => await repo.GetNamesAsync();


}
