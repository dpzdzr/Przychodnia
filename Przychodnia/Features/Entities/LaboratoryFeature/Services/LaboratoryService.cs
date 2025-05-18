using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Repositories;
using Przychodnia.Features.Entities.UserFeature.Services;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Services;

public class LaboratoryService(ILaboratoryRepository labRepo, IMapper mapper, IUserLookupService userLookupService)
    : BaseService<Laboratory, LaboratoryDTO, ILaboratoryRepository>(labRepo, mapper), ILaboratoryService
{
    private readonly IUserLookupService _userLookupService = userLookupService; 

    public async Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();

    public override async Task<Laboratory> CreateAsync(LaboratoryDTO dto)
    {
        var entity = _mapper.Map<Laboratory>(dto);

        if (dto.ManagerId is int managerId)
            await VerifyManager(managerId);

        entity = await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    public override async Task UpdateAsync(int id, LaboratoryDTO dto)
    {
        var entity = await GetByIdAsync(id);

        if (dto.ManagerId is int managerId)
            await VerifyManager(managerId, id);

        _mapper.Map(dto, entity);
        await _repo.SaveChangesAsync();
    }

    public override async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _repo.Remove(entity!);
        await _repo.SaveChangesAsync();
    }

    private async Task VerifyManager(int managerId, int? currentLabId = null)
    {
        var manager = await _userLookupService.GetByIdAsync(managerId);
        var alreadyManaging = await _repo.AnyAsync(l =>
            l.ManagerId == managerId &&
            (currentLabId == null || l.Id != currentLabId));
        if (alreadyManaging)
            throw new InvalidOperationException("Ten kierownik zarządza już innym laboratorium");
    }
}
