using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserService : IBaseService<User, UserDTO>
{
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutLabAssignedAsync();
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<List<User>> GetUsersByUserTypeAsync(UserTypeEnum type);
}
