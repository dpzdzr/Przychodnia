using AutoMapper;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Repositories;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Services;

namespace Przychodnia.Features.Entities.UserFeature.Services;

public class UserService(IUserRepository userRepo, ILaboratoryService labService,
    IUserTypeService userTypeService, IMapper mapper)
    : BaseEntityService<User, UserDTO, IUserRepository>(userRepo, mapper), IUserService, IUserLookupService
{
    private readonly ILaboratoryService _labService = labService;
    private readonly IUserTypeService _userTypeService = userTypeService;

    public async Task<List<User>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();
    public async Task<List<User>> GetUsersByUserTypeAsync(UserTypeEnum type)
        => await _repo.GetUsersByTypeAsync(type);
    public async Task<User?> GetByIdWithDetailsAsync(int id)
    {
        var user = await _repo.GetByIdWithDetailsAsync(id);
        return user is null ?
            throw new KeyNotFoundException($"Not found: {typeof(User).Name} with id: {id}") : user;
    }
    public async Task<List<User>> GetLabManagersWithoutLabAssignedAsync()
        => await _repo.GetLabManagersWithoutManagedLabAsync();
    public async Task<User?> GetByLogin(string username)
    {
        return await _repo.GetByLogin(username);
    }

    public override async Task<User> CreateAsync(UserDTO dto)
    {
        await VerifyUniqueness(dto);
        var user = new User();
        await MapDtoAndResolveRelationsAsync(dto, user);
        user = await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();
        return user;
    }
    public override async Task UpdateAsync(int id, UserDTO dto)
    {
        if (dto.LicenseNumber is string licenseNum)
        {
            var exists = await _repo.ExistsByLicenseNumberAsync(licenseNum);
            if (exists)
            {
                var existing = await _repo.GetByLicenseNumberAsync(licenseNum);
                if (existing!.Id != id)
                    throw new InvalidOperationException("Użytkownik z podanym numerem licencji jest już w bazie");
            }
        }

        var user = await GetByIdAsync(id);
        await MapDtoAndResolveRelationsAsync(dto, user!);
        await _repo.SaveChangesAsync();
    }
    public override async Task RemoveAsync(int id)
    {
        var entity = await GetByIdWithDetailsAsync(id);
        EnsureCanBeRemoved(entity);
        _repo.Remove(entity!);
        await _repo.SaveChangesAsync();
    }

    private async Task MapDtoAndResolveRelationsAsync(UserDTO dto, User targetUser)
    {
        _mapper.Map(dto, targetUser);

        targetUser.UserType = await _userTypeService.GetByIdAsync(dto.UserTypeId);

        if (dto.LaboratoryId is int labId)
            targetUser.Laboratory = await _labService.GetByIdAsync(labId);
    }
    private async Task VerifyUniqueness(UserDTO dto)
    {
        bool exists = false;

        if (dto.LicenseNumber is not null)
        {
            exists = await _repo.ExistsByLicenseNumberAsync(dto.LicenseNumber);
            if (exists)
                throw new InvalidOperationException("Użytkownik z podanym numerem licencji jest już w bazie");
        }
        if (dto.Login is not null)
        {
            exists = await _repo.AnyAsync(u => u.Login == dto.Login);
            if (exists)
                throw new InvalidOperationException("Użytkownik z podanym loginem jest już w bazie");
        }
    }
    private static void EnsureCanBeRemoved(User user)
    {
        switch (user.UserTypeId)
        {
            case (int)UserTypeEnum.Lekarz:
                if (user.AttendedAppointments.Count > 0)
                    throw new InvalidOperationException("Nie można usunąć lekarza z przypisanymi wizytami.");
                break;

            case (int)UserTypeEnum.Rejestrator:
                if (user.ScheduledAppointments.Count > 0)
                    throw new InvalidOperationException("Nie można usunąć rejestratora z przypisanymi wizytami.");
                break;

            case (int)UserTypeEnum.KierownikLaboratorium:
                if (user.ManagedLaboratory != null)
                    throw new InvalidOperationException("Nie można usunąć kierownika laboratorium zarządzającego jednostką.");
                break;
                // brak default – inne typy użytkowników nie są sprawdzane
        }
    }

}
