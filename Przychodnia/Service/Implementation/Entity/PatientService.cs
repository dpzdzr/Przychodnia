using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface.Entity;

namespace Przychodnia.Service.Implementation.Entity;

public class PatientService(IPatientRepository patientRepo, IPostalCodeRepository postalCodeRepo, IMapper mapper) : IPatientService
{
    private readonly IPatientRepository _patientRepo = patientRepo;
    private readonly IPostalCodeRepository _postalCodeRepo = postalCodeRepo;
    private readonly IMapper _mapper = mapper;
    public async Task<List<Patient>> GetAllAsync()
        => await _patientRepo.GetAllAsync();

    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _patientRepo.GetAllWithDetailsAsync();

    public async Task RemoveAsync(int id)
    {
        var patient = await _patientRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Nie znaleziono pacjenta");
        _patientRepo.Remove(patient);
        await _patientRepo.SaveChangesAsync();
    }

    public async Task<Patient> CreateAsync(PatientDTO dto)
    {
        var patient = new Patient();
        await MapDtoAndResolveRelationsAsync(dto, patient);   
        await _patientRepo.AddAsync(patient);
        await _patientRepo.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> GetByIdAsync(int id)
         => await _patientRepo.GetByIdAsync(id);

    public async Task SaveChangesAsync()
        => await _patientRepo.SaveChangesAsync();

    public async Task UpdateAsync(int id, PatientDTO dto)
    {
        var patient = await _patientRepo.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException("Nie znaleziono pacjenta");
        await MapDtoAndResolveRelationsAsync(dto, patient);
        await _patientRepo.SaveChangesAsync();
    }

    private async Task MapDtoAndResolveRelationsAsync(PatientDTO dto, Patient targetPatient)
    {
        _mapper.Map(dto, targetPatient);

        PostalCode? postalCode = null;
        if (dto.PostalCodeId is not null)
        {
            postalCode = await _postalCodeRepo.GetByIdAsync(dto.PostalCodeId.Value)
                ?? throw new KeyNotFoundException("Nie znaleziono kodu pocztowego");
        }
        targetPatient.PostalCode = postalCode;
    }
}
