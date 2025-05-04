using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class LaboratoryRepository(DbContext context) : BaseRepository<Laboratory>(context), ILaboratoryRepository
{
    public async Task<Laboratory?> GetLaboratoryByNameAsync(string laboratoryName)
        => await _dbSet.SingleOrDefaultAsync(l => l.Name == laboratoryName);
}
