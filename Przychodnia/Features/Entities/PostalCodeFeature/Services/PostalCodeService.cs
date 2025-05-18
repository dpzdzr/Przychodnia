using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AutoMapper;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Repositories;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Services;

public class PostalCodeService(IPostalCodeRepository repo, IMapper mapper) 
    : BaseService<PostalCode, PostalCodeDTO, IPostalCodeRepository>(repo, mapper), IPostalCodeService
{
    public override async Task<PostalCode> CreateAsync(PostalCodeDTO dto)
    {   
        var entity = _mapper.Map<PostalCode>(dto);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    public override async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _repo.Remove(entity!);
        await _repo.SaveChangesAsync();
    }

    public override async Task UpdateAsync(int id, PostalCodeDTO dto)
    {
        var existing = await GetByIdAsync(id);
        _mapper.Map(dto, existing);
        await _repo.SaveChangesAsync();
    }
}
