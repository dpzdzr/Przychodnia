using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Features.Entities.UserTypesFeature.Repositories;

public class UserTypeRepository(AppDbContext context)
    : BaseRepository<UserType, AppDbContext>(context), IUserTypeRepository
{
    public async Task<List<string>> GetNamesAsync()
        => await _dbSet.Select(t => t.Name).ToListAsync();
}
