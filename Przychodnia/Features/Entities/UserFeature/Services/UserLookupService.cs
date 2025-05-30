﻿using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Repositories;
using System.Security;

namespace Przychodnia.Features.Entities.UserFeature.Services;

public class UserLookupService(IUserRepository repo) : IUserLookupService
{
    private readonly IUserRepository _repo = repo;

    public async Task<bool> ExistsByIdAsync(int id)
        => await _repo.ExistsByIdAsync(id);

    public async Task<User?> GetByLogin(string username)    
        => await _repo.GetByLogin(username);

    public async Task<User?> GetByIdAsync(int id)
        => await _repo.GetByIdAsync(id) ??
        throw new KeyNotFoundException($"{nameof(User)} not found with id: {id}");

    public async Task<User?> GetByIdWithDetailsAsync(int id)
        => await _repo.GetByIdWithDetailsAsync(id) ??
            throw new KeyNotFoundException($"Not found: {typeof(User).Name} with id: {id}");
}
