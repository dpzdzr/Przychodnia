using System.Linq.Expressions;

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
