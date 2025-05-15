using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IUserTypeRepository : IBaseRepository<UserType>
{
    Task<List<string>> GetNamesAsync();
}
