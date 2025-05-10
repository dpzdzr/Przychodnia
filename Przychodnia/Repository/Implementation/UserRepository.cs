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

    public async Task<List<User>> GetAllWithUserTypeAsync()
        => await _dbSet.Include(u => u.UserType).ToListAsync();

    public async Task<List<User>> GetLabManagersWithoutManagedLabAsync()
    {
        return await _context.Users
            .Include(u => u.UserType)
            .Where(u => u.UserTypeId == (int)UserTypeEnum.KierownikLaboratorium)
            .Where(u => !_context.Laboratories.Any(l => l.ManagerId == u.Id))
            .ToListAsync();
    }
}
