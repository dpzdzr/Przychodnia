using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserService
{
    Task<User> CreateAsync(UserDTO dto);
    Task RemoveAsync(int id);
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutLabAssignedAsync();

    Task<User?> GetByIdWithDetailsAsync(int id);

    Task UpdateAsync(int id, UserDTO dto);

    Task<List<User>> GetUsersByUserType(UserTypeEnum type);
}
