using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class UserService(IUserRepository userRepo, ILaboratoryRepository labRepo,
    IUserTypeRepository userTypeRepo, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly ILaboratoryRepository _labRepo = labRepo;
    private readonly IUserTypeRepository _userTypeRepo = userTypeRepo;
    private readonly IMapper _mapper = mapper;

    public async Task<List<User>> GetAllWithDetailsAsync()
        => await _userRepo.GetAllWithDetailsAsync();

    public async Task<List<User>> GetUsersByUserType(UserTypeEnum type)
        => await _userRepo.GetUsersByTypeAsync(type);

    public async Task RemoveAsync(int id)
    {
        var entity = await _userRepo.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException("Nie znaleziono użytkownika");
        _userRepo.Remove(entity);
        await _userRepo.SaveChangesAsync();
    }

    public async Task<User?> GetByIdWithDetailsAsync(int id)
        => await _userRepo.GetByIdAsync(id);

    public async Task UpdateAsync(int id, UserDTO dto)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nie znaleziono użytkownika");
        await MapDtoAndResolveRelationsAsync(dto, user);
        await _userRepo.SaveChangesAsync();
    }

    public async Task<User> CreateAsync(UserDTO dto)
    {
        var user = new User();
        await MapDtoAndResolveRelationsAsync(dto, user);
        user = await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();
        return user;
    }

    private async Task MapDtoAndResolveRelationsAsync(UserDTO dto, User targetUser)
    {
        _mapper.Map(dto, targetUser);

        var userType = await _userTypeRepo.GetByIdAsync(dto.UserTypeId) ?? 
            throw new KeyNotFoundException("Nieprawidłowy typ użytkownika");

        Laboratory? lab = null;
        if (dto.LaboratoryId is not null)
        {
            lab = await _labRepo.GetByIdAsync(dto.LaboratoryId.Value) ??
                throw new KeyNotFoundException("Nieprawidłowe laboratorium");
        }

        targetUser.Laboratory = lab;
        targetUser.UserType = userType;
    }

    public async Task<List<User>> GetLabManagersWithoutLabAssignedAsync()
        => await _userRepo.GetLabManagersWithoutManagedLabAsync();
}
