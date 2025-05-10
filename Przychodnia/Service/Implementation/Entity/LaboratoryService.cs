using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class LaboratoryService(ILaboratoryRepository labRepo, IMapper mapper, IUserRepository userRepo) 
    : ILaboratoryService
{
    private readonly ILaboratoryRepository _labRepo = labRepo;
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IMapper _mapper = mapper;

    public async Task<Laboratory> AddAsync(LaboratoryDTO dto)
    {   
        var entity = _mapper.Map<Laboratory>(dto);

        if(dto.ManagerId is int managerId)
            entity.Manager = await GetValidManagerById(managerId);

        entity = await _labRepo.AddAsync(entity);
        await _labRepo.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Laboratory>> GetAllAsync()
        => await _labRepo.GetAllAsync();

    public async Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync()
        => await _labRepo.GetAllWithDetailsAsync();

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    private async Task<User> GetValidManagerById(int id)
    {
        return await _userRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nieprawidłowe id managera laboratorium");
    }
}
