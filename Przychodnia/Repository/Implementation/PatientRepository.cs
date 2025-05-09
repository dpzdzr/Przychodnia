using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class PatientRepository(DbContext context) : BaseRepository<Patient>(context), IPatientRepository
{
    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _dbSet.Include(p=>p.PostalCode).ToListAsync();

    public void DetachAllAddedEntities()
    {
        var entries = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .ToList();

        foreach (var entry in entries)
            entry.State = EntityState.Detached;
    }
}
