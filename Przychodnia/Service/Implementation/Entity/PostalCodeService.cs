using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

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
