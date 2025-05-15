using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Interface.Entity;

public interface IBaseService<TEntity, TDto>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> CreateAsync(TDto dto);
    Task UpdateAsync(int id, TDto dto);
    Task RemoveAsync(int id);
    Task EnsureExistsByIdAsync(int id);
}
