using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class UserRepository(DbContext context) : BaseRepository<User>(context), IUserRepository
{
    public IEnumerable<User> GetUsersByType(UserType type)
    {
        return [.. _dbSet.Where(u => u.UserType == type)];
    }

    public IEnumerable<User> GetAllWithUserType()
    {
        return [.. _dbSet.Include(u => u.UserType)];
    }

}
