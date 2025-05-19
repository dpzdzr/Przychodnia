using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using System.Security;

namespace Przychodnia.Features.Entities.UserFeature.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User, AppDbContext>(context), IUserRepository
{
    public async Task<List<User>> GetUsersByTypeAsync(UserTypeEnum type)
    {
        return await _dbSet.Where(u => u.UserType.Id == (int)type).ToListAsync();
    }

    public async Task<List<User>> GetAllWithDetailsAsync()
    { 
        return await _dbSet.Include(u => u.UserType).Include(u => u.Laboratory).ToListAsync(); 
    }

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

    public async Task<User?> GetByLogin(string login)
    {
        return await _dbSet
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<bool> ExistsByLicenseNumberAsync(string licenseNumber)
    {
        return await _dbSet.AnyAsync(p => p.LicenseNumber == licenseNumber);
    }

    public async Task<User?> GetByLicenseNumberAsync(string licenseNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.LicenseNumber == licenseNumber);
    }
}
