using Przychodnia.Model;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserTypeService
{
    Task<List<string>> GetNamesAsync();
    Task<List<UserType>> GetAllAsync();
}
