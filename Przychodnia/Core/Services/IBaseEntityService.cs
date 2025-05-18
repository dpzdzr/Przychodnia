namespace Przychodnia.Core.Services;

public interface IBaseEntityService<TEntity, TDto>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> CreateAsync(TDto dto);
    Task UpdateAsync(int id, TDto dto);
    Task RemoveAsync(int id);
    Task EnsureExistsByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}
