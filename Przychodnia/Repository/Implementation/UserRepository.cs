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
    public async Task<List<User>> GetUsersByTypeAsync(UserType type)
        => await _dbSet.Where(u => u.UserType == type).ToListAsync();

    public async Task<List<User>> GetAllWithUserTypeAsync()
        => await _dbSet.Include(u => u.UserType).ToListAsync();

    public override async Task<User?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(u => u.UserType)
            .Include(u => u.Laboratory)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
