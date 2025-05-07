using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

class PostalCodeRepository(DbContext context)
    : BaseRepository<PostalCode>(context), IPostalCodeRepository
{
    public async Task<List<PostalCode>> Filter(string fragment)
    {
        return await _dbSet
            .Where(pc => string.IsNullOrEmpty(fragment) || pc.Code.StartsWith(fragment))
            .OrderBy(k => k.Code)
            .ToListAsync();
    }
}
