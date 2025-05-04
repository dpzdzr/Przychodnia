using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface;

public interface IUserService
{
    Task CreateUserAsync(UserInputDTO model);
    Task RemoveUserAsync(User user);
    Task<List<User>> GetAllWithUserTypeAsync();
}
