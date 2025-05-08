using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _repo = userRepository;
    public async Task<User> CreateUserAsync(UserDTO dto)
    {
        var entity = await _repo.AddAsync(new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Login = dto.Login,
            PasswordHash = dto.PasswordHash,
            UserType = dto.UserType,
            LicenseNumber = dto.LicenseNumber,
            Laboratory = dto.Laboratory,
            IsActive = dto.IsActive
        });
        await _repo.SaveChangesAsync();
        return entity;
    }

    public async Task<List<User>> GetAllWithUserTypeAsync()
        => await _repo.GetAllWithUserTypeAsync();

    public async Task RemoveAsync(int id)
    {   
        var entity = await _repo.GetByIdAsync(id) ?? throw new ArgumentNullException(nameof(id));
        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
    }

    public async Task<User?> GetByIdWithDetailsAsync(int id) 
        => await _repo.GetByIdAsync(id);

    public async Task UpdateAsync(int id, UserDTO dto)
    {
        var existing = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nie znaleziono użytkownika");

        existing.FirstName = dto.FirstName;
        existing.LastName = dto.LastName;
        existing.Login = dto.Login;
        existing.PasswordHash = dto.PasswordHash;
        existing.LicenseNumber = dto.LicenseNumber;
        existing.IsActive = dto.IsActive;
        existing.Laboratory = dto.Laboratory;
        existing.UserType = dto.UserType;

        await _repo.SaveChangesAsync();
    }
}
