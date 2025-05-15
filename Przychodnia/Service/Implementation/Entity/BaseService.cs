using AutoMapper;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Implementation.Entity;

public abstract class BaseService<TEntity, TDto, TRepository>(TRepository repository, IMapper mapper)
    : IBaseService<TEntity, TDto>
    where TEntity : class, IEntity, new()
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly IMapper _mapper = mapper;
    protected readonly TRepository _repo = repository;

    public async Task EnsureExistsByIdAsync(int id)
    {
        if (!await _repo.ExistsByIdAsync(id))
            throw new KeyNotFoundException($"Not found: {nameof(TEntity)} with id: {id}");
    }
    public async Task<bool> ExistsByIdAsync(int id)
        => await _repo.ExistsByIdAsync(id);
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _repo.GetAllAsync();
    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await _repo.GetByIdAsync(id) ??
        throw new KeyNotFoundException($"{nameof(TEntity)} not found with id: {id}");
    public abstract Task<TEntity> CreateAsync(TDto dto);
    public abstract Task UpdateAsync(int id, TDto dto);
    public abstract Task RemoveAsync(int id);
}
