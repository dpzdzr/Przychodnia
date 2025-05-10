using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class LaboratoryRepository(AppDbContext context) 
    : BaseRepository<Laboratory, AppDbContext>(context), ILaboratoryRepository
{
    public async Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync()
        => await _dbSet.Include(l => l.Manager).Include(l => l.Workers).ToListAsync();
}
