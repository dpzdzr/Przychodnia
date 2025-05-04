using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IUserTypeRepository : IBaseRepository<UserType>
{
    Task<List<string>> GetNamesAsync();
}
