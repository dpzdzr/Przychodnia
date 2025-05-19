using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using System.Security;

namespace Przychodnia.Features.Entities.UserFeature.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetUsersByTypeAsync(UserTypeEnum type);
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutManagedLabAsync();
    Task<User?> GetByLogin(string username);
    Task<bool> ExistsByLicenseNumberAsync(string licenseNumber);
    Task<User?> GetByLicenseNumberAsync(string licenseNumber);
}
