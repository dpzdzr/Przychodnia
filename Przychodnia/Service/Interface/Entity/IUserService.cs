using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserService
{
    Task<User> CreateAsync(UserDTO dto);
    Task RemoveAsync(int id);
    Task<List<User>> GetAllWithUserTypeAsync();

    Task<User?> GetByIdWithDetailsAsync(int id);

    Task UpdateAsync(int id, UserDTO dto);
}
