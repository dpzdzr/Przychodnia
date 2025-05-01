using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Model;

namespace Przychodnia.Service.Implementation;

class UserCreationService(IUserRepository userRepository) : IUserCreationService
{
    private readonly IUserRepository _userRepository = userRepository;
    public void CreateUser(UserInputModel model)
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
}
