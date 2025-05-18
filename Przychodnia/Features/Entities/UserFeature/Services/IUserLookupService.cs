using Przychodnia.Features.Entities.UserFeature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.UserFeature.Services;

public interface IUserLookupService
{
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<User?> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}
