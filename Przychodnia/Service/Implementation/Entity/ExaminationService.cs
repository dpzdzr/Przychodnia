using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Implementation.Entity;

public class ExaminationService(ExaminationRepository exRepo, IMapper mapper, IUserRepository userRepo) : IExaminationService
{
    private readonly IExaminationRepository _exRepository = exRepo;
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IMapper _mapper = mapper;
    public async Task<Examination> AddAsync(ExaminationDTO dto)
    {
        var entity = _mapper.Map<Examination>(dto);
        entity = await _exRepository.AddAsync(entity);
        await _exRepository.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Examination>> GetAllAsync()
        => await _exRepository.GetAllAsync();

    public Task<IEnumerable<Examination>> GetAllWithDetailsAsync()
        => _exRepository.GetAllWithDetailsAsync();

    public async Task RemoveAsync(int id)
    {
        var entity = await _exRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Nie znaleziono badania z podanym identyfikatorem({id})");
        _exRepository.Remove(entity);
        await _exRepository.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(int id, ExaminationDTO dto)
    {
        var entity = await _exRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Nie znaleziono badania z podanym identyfikatorem({id})");
        _mapper.Map(dto, entity);
        await _exRepository.SaveChangesAsync();
    }
}
