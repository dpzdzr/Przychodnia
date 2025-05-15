using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Interface.Entity;

public interface IUserLookupService
{
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<User?> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}
