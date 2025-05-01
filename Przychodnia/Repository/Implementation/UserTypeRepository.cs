using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class UserTypeRepository(DbContext context) : BaseRepository<UserType>(context), IUserTypeRepository
{
    public IEnumerable<string> GetNames()
    {
        return [.. _dbSet.Select(t => t.Name)];
    }
}
