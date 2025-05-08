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
    private readonly ILaboratoryRepository _labRepo = labRepo;
    public async Task<List<Laboratory>> GetAllAsync()
        => await _labRepo.GetAllAsync();
}
