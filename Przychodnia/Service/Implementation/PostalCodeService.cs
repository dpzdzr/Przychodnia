using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

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
}
