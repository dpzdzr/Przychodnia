using System.Linq.Expressions;

namespace Przychodnia.Repository.Interface;

public interface IBaseRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    void Remove(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void Update(T entity);
    Task SaveChangesAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}
