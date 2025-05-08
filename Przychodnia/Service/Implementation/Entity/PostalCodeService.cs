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
    public async Task CreateAsync(PostalCodeInputDTO dto)
    {
        await _repo.AddAsync(new PostalCode
        {
            Code = dto.PostalCode,
            City = dto.City
        });
        await _repo.SaveChangesAsync();
    }

    public async Task<List<PostalCode>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task SaveChangesAsync()
        => await _repo.SaveChangesAsync();

    public async Task RemoveAsync(PostalCode postalCode)
    {
        _repo.Remove(postalCode);
        await _repo.SaveChangesAsync();
    }

    public async Task<List<PostalCode>> GetAllMatchingByCode(string fragment)
        => await _repo.Filter(fragment);

    public async Task<List<PostalCode>> GetDistinctCodes(string fragment)
    {
        var filtered = await _repo.Filter(fragment);

        return [.. filtered.GroupBy(x => x.Code).Select(x => x.First())];
    }
}
