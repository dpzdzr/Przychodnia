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

public class UserTypeRepository(AppDbContext context) 
    : BaseRepository<UserType, AppDbContext>(context), IUserTypeRepository
{
    public async Task<List<string>> GetNamesAsync()
        => await _dbSet.Select(t => t.Name).ToListAsync();
}
