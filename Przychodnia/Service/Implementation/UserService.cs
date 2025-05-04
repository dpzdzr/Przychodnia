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
    private readonly IUserRepository _userRepo = userRepository;
    public async Task CreateUserAsync(UserInputDTO model)
    {
        await _userRepo.AddAsync(new User
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
        await _userRepo.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllWithUserTypeAsync()
        => await _userRepo.GetAllWithUserTypeAsync();


    public async Task RemoveUserAsync(User user)
    {
        _userRepo.Remove(user);
        await _userRepo.SaveChangesAsync();
    }
}
