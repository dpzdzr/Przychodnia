using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class ExaminationRepository(AppDbContext context)
    : BaseRepository<Examination, AppDbContext>(context), IExaminationRepository
{
    public async Task<IEnumerable<Examination>> GetAllWithDetailsAsync()
        => await _dbSet
        .Include(p => p.Patient)
        .Include(p => p.OrderedBy)
        .Include(p => p.PerformingDoctor)
        .Include(p => p.PerformingLaboratory)
        .ToListAsync();
}
