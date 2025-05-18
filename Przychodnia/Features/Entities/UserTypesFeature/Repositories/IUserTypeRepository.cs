using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserTypesFeature.Repositories;

public interface IUserTypeRepository : IBaseRepository<UserType>
{
    Task<List<string>> GetNamesAsync();
}
