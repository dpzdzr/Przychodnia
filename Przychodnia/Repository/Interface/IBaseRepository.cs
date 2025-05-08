using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Repository.Interface;

public interface IBaseRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    void Remove(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void Update(T entity);
    Task SaveChangesAsync();
}
