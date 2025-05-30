﻿using AutoMapper;
using Przychodnia.Core.Interfaces;
using Przychodnia.Core.Repositories;

namespace Przychodnia.Core.Services;

public abstract class BaseEntityService<TEntity, TDto, TRepository>(TRepository repository, IMapper mapper)
    : IBaseEntityService<TEntity, TDto>
    where TEntity : class, IEntity, new()
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly IMapper _mapper = mapper;
    protected readonly TRepository _repo = repository;

    public async Task EnsureExistsByIdAsync(int id)
    {
        if (!await _repo.ExistsByIdAsync(id))
            throw new KeyNotFoundException($"Not found: {typeof(TEntity).Name} with id: {id}");
    }

    public async Task<bool> ExistsByIdAsync(int id)
        => await _repo.ExistsByIdAsync(id);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _repo.GetAllAsync();

    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await _repo.GetByIdAsync(id) ??
        throw new KeyNotFoundException($"{typeof(TEntity).Name} not found with id: {id}");

    public abstract Task<TEntity> CreateAsync(TDto dto);
    public abstract Task UpdateAsync(int id, TDto dto);
    public abstract Task RemoveAsync(int id);
}
