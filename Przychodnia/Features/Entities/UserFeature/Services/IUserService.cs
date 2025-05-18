using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserFeature.Services;

public interface IUserService : IBaseService<User, UserDTO>
{
    Task<List<User>> GetAllWithDetailsAsync();
    Task<List<User>> GetLabManagersWithoutLabAssignedAsync();
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<List<User>> GetUsersByUserTypeAsync(UserTypeEnum type);
}
