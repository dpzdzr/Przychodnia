using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class PatientService(IPatientRepository patientRepo, IPostalCodeService postalCodeService, IMapper mapper) 
    : BaseService<Patient, PatientDTO, IPatientRepository>(patientRepo, mapper), IPatientService
{
    private readonly IPostalCodeService _postalCodeService = postalCodeService;

    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();
    public async Task<Patient?> GetByPeselAsync(string pesel)
    {
        return await _repo.GetByPesel(pesel) ??
            throw new InvalidOperationException($"Patient with PESEL: {pesel} not found");
    }

    public override async Task<Patient> CreateAsync(PatientDTO dto)
    {
        var patient = new Patient();
        await MapDtoAndResolveRelationsAsync(dto, patient);
        await _repo.AddAsync(patient);
        await _repo.SaveChangesAsync();
        return patient;
    }
    public override async Task UpdateAsync(int id, PatientDTO dto)
    {
        var patient = await GetByIdAsync(id);
        await MapDtoAndResolveRelationsAsync(dto, patient!);
        await _repo.SaveChangesAsync();
    }
    public override async Task RemoveAsync(int id)
    {
        var patient = await GetByIdAsync(id);
        _repo.Remove(patient!);
        await _repo.SaveChangesAsync();
    }
    private async Task MapDtoAndResolveRelationsAsync(PatientDTO dto, Patient targetPatient)
    {
        _mapper.Map(dto, targetPatient);
        if (dto.PostalCodeId is int id)
            targetPatient.PostalCode = await _postalCodeService.GetByIdAsync(id);
    }
}
