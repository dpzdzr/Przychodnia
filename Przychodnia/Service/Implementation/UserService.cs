using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _repo = userRepository;
    public async Task CreateUserAsync(UserInputDTO model)
    {
        await _repo.AddAsync(new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Login = model.Login,
            PasswordHash = model.PasswordHash,
            UserType = model.UserType,
            LicenseNumber = model.LicenseNumber,
            Laboratory = model.Laboratory,
            IsActive = model.IsActive
        });
        await _repo.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllWithUserTypeAsync()
        => await _repo.GetAllWithUserTypeAsync();


    public async Task RemoveAsync(User user)
    {
        _repo.Remove(user);
        await _repo.SaveChangesAsync();
    }

    public async Task<User?> GetByIdWithDetailsAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task SaveChanges()
    {
        await _repo.SaveChangesAsync();
    }
}
