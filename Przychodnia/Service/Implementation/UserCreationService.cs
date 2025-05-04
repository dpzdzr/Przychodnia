using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

public class UserCreationService(IUserRepository userRepository) : IUserCreationService
{
    private readonly IUserRepository _userRepository = userRepository;
    public void CreateUser(UserInputDTO model)
    {
        try
        {
            _userRepository.Add(new Model.User
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
            _userRepository.SaveChanges();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
