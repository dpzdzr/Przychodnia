using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class LaboratoryService(ILaboratoryRepository labRepo) : ILaboratoryService
{
    private readonly ILaboratoryRepository _repo = labRepo;

    public async Task AddAsync(Laboratory lab)
    {
        await _repo.AddAsync(lab);
        await _repo.SaveChangesAsync();
    }

    public async Task<List<Laboratory>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task RemoveAsync(Laboratory lab)
    {
        _repo.Remove(lab);
        await _repo.SaveChangesAsync();
    }
}
