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

public class PatientRepository(AppDbContext context) 
    : BaseRepository<Patient, AppDbContext>(context), IPatientRepository
{
    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _dbSet.Include(p=>p.PostalCode).ToListAsync();

}
