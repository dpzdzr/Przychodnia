using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserTypeService
{
    Task<List<string>> GetNamesAsync();
    Task<List<UserType>> GetAllAsync();
}
