using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserTypesFeature.Repositories;

public interface IUserTypeRepository : IBaseRepository<UserType>
{
    Task<List<string>> GetNamesAsync();
}
