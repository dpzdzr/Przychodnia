using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Repository.Implementation;

public class ExaminationRepository(AppDbContext context) 
    : BaseRepository<Examination, AppDbContext>(context), IExaminationRepository
{
    public async Task<IEnumerable<Examination>> GetAllWithDetailsAsync()
        => await _dbSet.Include(l => l.ExaminationType).Include(l => l.Patient)
        .Include(l => l.OrderedBy).Include(l => l.PerformingDoctor)
        .Include(l => l.PerformingLaboratory).ToListAsync();
}
