using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.PatientFeature.Models;

namespace Przychodnia.Features.Entities.PatientFeature.Repositories;

public class PatientRepository(AppDbContext context) 
    : BaseRepository<Patient, AppDbContext>(context), IPatientRepository
{
    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
    {
        var patients = await _dbSet.Include(p => p.PostalCode).ToListAsync();
        return patients;
    }
        

    public async Task<Patient?> GetByPesel(string pesel)
        => await _dbSet.FirstOrDefaultAsync(p => p.Pesel == pesel);
}
