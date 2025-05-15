using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class PostalCodeService(IPostalCodeRepository repo, IMapper mapper) : IPostalCodeService
{
    private readonly IPostalCodeRepository _repo = repo;
    private readonly IMapper _mapper = mapper;
    public async Task<PostalCode> CreateAsync(PostalCodeDTO dto)
    {
        var entity = _mapper.Map<PostalCode>(dto);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    public async Task<List<PostalCode>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task RemoveAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nie znaleziono kodu pocztowego");

        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, PostalCodeDTO dto)
    {
        var existing = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nie znaleziono kodu pocztowego");

        _mapper.Map(dto, existing);
        await _repo.SaveChangesAsync();
    }
}
