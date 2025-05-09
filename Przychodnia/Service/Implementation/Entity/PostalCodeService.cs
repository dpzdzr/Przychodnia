using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class PostalCodeService(IPostalCodeRepository repo) : IPostalCodeService
{
    private readonly IPostalCodeRepository _repo = repo;
    public async Task<PostalCode> CreateAsync(PostalCodeDTO dto)
    {
        var entity = await _repo.AddAsync(new PostalCode
        {
            Code = dto.Code,
            City = dto.City
        });
        await _repo.SaveChangesAsync();
        return entity;
    }

    public async Task<List<PostalCode>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task RemoveAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new ArgumentNullException("Nie znaleziono obektu");

        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, PostalCodeDTO dto)
    {
        var existing = await _repo.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException("Nie znaleziono kodu pocztowego");

        existing.Code = dto.Code;
        existing.City = dto.City;

        await _repo.SaveChangesAsync();
    }
}
