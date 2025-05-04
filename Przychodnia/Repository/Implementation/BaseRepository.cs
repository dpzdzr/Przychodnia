using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public virtual async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public virtual void Remove(T entity)
        => _dbSet.Remove(entity);

    public virtual async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
