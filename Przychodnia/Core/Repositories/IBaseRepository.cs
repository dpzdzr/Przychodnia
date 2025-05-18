using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Core.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    void Remove(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void Update(T entity);
    Task SaveChangesAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsByIdAsync(int id);
}
