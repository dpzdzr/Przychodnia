using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class UserRepository(AppDbContext context)
    : BaseRepository<User, AppDbContext>(context), IUserRepository
{
    public async Task<List<User>> GetUsersByTypeAsync(UserTypeEnum type)
        => await _dbSet.Where(u => u.UserType.Id == (int)type).ToListAsync();

    public async Task<List<User>> GetAllWithDetailsAsync()
        => await _dbSet.Include(u => u.UserType).Include(u => u.Laboratory).ToListAsync();

    public async Task<List<User>> GetLabManagersWithoutManagedLabAsync()
    {
        return await _dbSet
            .Include(u => u.UserType)
            .Where(u => u.UserTypeId == (int)UserTypeEnum.KierownikLaboratorium)
            .Where(u => !_context.Laboratories.Any(l => l.ManagerId == u.Id))
            .ToListAsync();
    }

    public async Task<User?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet.Where(u => u.Id == id)
            .Include(u => u.UserType)
            .Include(u => u.Laboratory)
            .FirstOrDefaultAsync();
    }
}
