using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Interfaces;
using System.Linq.Expressions;

namespace Przychodnia.Core.Repositories;

public abstract class BaseRepository<T, TContext> : IBaseRepository<T>
    where T : class, IEntity
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<T> AddAsync(T entity)
        => (await _dbSet.AddAsync(entity)).Entity;

    public virtual void Remove(T entity)
        => _dbSet.Remove(entity);

    public virtual async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void Update(T entity)
        => _dbSet.Update(entity);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public async Task<bool> ExistsByIdAsync(int id)
        => await _dbSet.AnyAsync(e => e.Id == id);
}
