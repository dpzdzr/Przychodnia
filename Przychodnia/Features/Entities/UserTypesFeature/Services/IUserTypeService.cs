using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserTypesFeature.Services;

public interface IUserTypeService
{
    Task<List<string>> GetNamesAsync();
    Task<List<UserType>> GetAllAsync();
    Task<UserType?> GetByIdAsync(int id);
}
