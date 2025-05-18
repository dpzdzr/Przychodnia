using Przychodnia.Features.Entities.UserFeature.Models;
using System.Security;

namespace Przychodnia.Features.Entities.UserFeature.Services;

public interface IUserLookupService
{
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<User?> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<User?> GetByLogin(string username);
}
