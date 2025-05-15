using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetUsersByTypeAsync(UserTypeEnum type);
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutManagedLabAsync();
}
