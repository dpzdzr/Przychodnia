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
    : BaseService<User, UserDTO, IUserRepository>(userRepo, mapper), IUserService, IUserLookupService
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

    public override async Task<User> CreateAsync(UserDTO dto)
    {
        var user = new User();
        await MapDtoAndResolveRelationsAsync(dto, user);
        user = await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();
        return user;
    }
    public override async Task UpdateAsync(int id, UserDTO dto)
    {
        var user = await GetByIdAsync(id);
        await MapDtoAndResolveRelationsAsync(dto, user!);
        await _repo.SaveChangesAsync();
    }
    public override async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
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
}
