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

        if (dto.ManagerId is int managerId)
            await VerifyManager(managerId);

        entity = await _labRepo.AddAsync(entity);
        await _labRepo.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Laboratory>> GetAllAsync()
        => await _labRepo.GetAllAsync();

    public async Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync()
        => await _labRepo.GetAllWithDetailsAsync();

    public async Task RemoveAsync(int id)
    {
        var entity = await _labRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Nie znaleziono laboratorium z podanym identyfikatorem({id})");
        _labRepo.Remove(entity);
        await _labRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, LaboratoryDTO dto)
    {
        var entity = await _labRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Nie znaleziono laboratorium z podanym identyfikatorem({id})");

        if (dto.ManagerId is int managerId)
            await VerifyManager(managerId, id);

        _mapper.Map(dto, entity);
        await _labRepo.SaveChangesAsync();
    }

    private async Task<User> GetValidManagerById(int id)
    {
        return await _userRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nieprawidłowe id managera laboratorium");
    }

    private async Task VerifyManager(int managerId, int? currentLabId = null)
    {
        var manager = await GetValidManagerById(managerId);
        var alreadyManaging = await _labRepo.AnyAsync(l =>
            l.ManagerId == managerId &&
            (currentLabId == null || l.Id != currentLabId));
        if (alreadyManaging)
            throw new InvalidOperationException("Ten kierownik zarządza już innym laboratorium");
    }

}
