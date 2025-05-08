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

public class PatientService(IPatientRepository repo) : IPatientService
{
    private readonly IPatientRepository _repo = repo;
    public async Task<List<Patient>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<IEnumerable<Patient>> GetAllWithDetailsAsync()
        => await _repo.GetAllWithDetailsAsync();

    public async Task RemoveAsync(Patient patient)
    {
        _repo.Remove(patient);
        await _repo.SaveChangesAsync();
    }

    public async Task AddAsync(PatientInputDto patientDTO)
    {
        await _repo.AddAsync(new Patient
        {
            FirstName = patientDTO.FirstName,
            LastName = patientDTO.LastName,
            Pesel = patientDTO.Pesel,
            HouseNumber = patientDTO.HouseNumber,
            ApartmentNumber = patientDTO.ApartmentNumber,
            Street = patientDTO.Street,
            PostalCode = patientDTO.PostalCode,
            Sex = patientDTO.Sex
        });
        await _repo.SaveChangesAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
         => await _repo.GetByIdAsync(id);

    public async Task SaveChangesAsync()
        => await _repo.SaveChangesAsync();
}
