using AutoMapper;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Repositories;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;

namespace Przychodnia.Features.Entities.PatientFeature.Services;

public class PatientService(IPatientRepository patientRepo, IPostalCodeService postalCodeService, IMapper mapper)
    : BaseEntityService<Patient, PatientDTO, IPatientRepository>(patientRepo, mapper), IPatientService
{
    private readonly IPostalCodeService _postalCodeService = postalCodeService;

    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();
    public async Task<Patient?> GetByPeselAsync(string pesel)
    {
        return await _repo.GetByPesel(pesel) ??
            throw new InvalidOperationException($"Nie znaleziono pacjenta z numerem PESEL: {pesel} ");
    }

    public override async Task<Patient> CreateAsync(PatientDTO dto)
    {
        var exists = await _repo.ExistsByPeselAsync(dto.Pesel);
        if (exists)
            throw new InvalidOperationException("Pacjent z danym numerem PESEL jest już w bazie");

        var patient = new Patient();
        await MapDtoAndResolveRelationsAsync(dto, patient);
        await _repo.AddAsync(patient);
        await _repo.SaveChangesAsync();
        return patient;
    }

    public override async Task UpdateAsync(int id, PatientDTO dto)
    {
        var exists = await _repo.ExistsByPeselAsync(dto.Pesel);
        if (exists)
        {
            var existing = GetByPeselAsync(dto.Pesel);
            if (existing.Id != id)
                throw new InvalidOperationException("Pacjent z danym numerem PESEL jest już w bazie");
        }

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
