using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserFeature.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetUsersByTypeAsync(UserTypeEnum type);
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutManagedLabAsync();
}
