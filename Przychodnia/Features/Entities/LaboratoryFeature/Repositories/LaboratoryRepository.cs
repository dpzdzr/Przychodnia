using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Repositories;

public class LaboratoryRepository(AppDbContext context)
    : BaseRepository<Laboratory, AppDbContext>(context), ILaboratoryRepository
{
    public async Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync()
        => await _dbSet.Include(l => l.Manager).Include(l => l.Workers).ToListAsync();
}
